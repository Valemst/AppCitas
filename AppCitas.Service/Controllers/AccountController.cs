using AppCitas.Service.Data;
using AppCitas.Service.DTOs;
using AppCitas.Service.Entities;
using AppCitas.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AppCitas.Service.Controllers;

public class AccountController : BaseApiController
{
	private readonly DataContext _context;
	private readonly ITokenService _tokenService;

	public AccountController(DataContext context, ITokenService tokenService)
	{
		_context = context;
		_tokenService = tokenService;
	}

	[HttpPost("register")]
	public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
	{
		if (await UserExists(registerDto.Username))
			return BadRequest("Username is already taken!");

		using var hmac = new HMACSHA512();

		var user = new AppUser
		{
			UserName = registerDto.Username.ToLower(),
			PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
			PasswordSalt = hmac.Key
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return new UserDto
		{
			Username = user.UserName,
			Token = _tokenService.CreateToken(user)
		};
	}

	[HttpPost("login")]
	public async Task<ActionResult<UserDto>> Login(LoginDto logintDto)
	{
		var user = await _context.Users
			.SingleOrDefaultAsync(x => x.UserName == logintDto.Username);

		if (user == null) return Unauthorized("Invalid username or password");

		using var hmac = new HMACSHA512(user.PasswordSalt);

		var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logintDto.Password));

		for(int i = 0; i < computerHash.Length; i++)
		{
			if (computerHash[i] != user.PasswordHash[i])
				return Unauthorized("Invalid username or password");
		}

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

	private async Task<bool> UserExists(string username)
	{
		return await
			_context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
	}
}
