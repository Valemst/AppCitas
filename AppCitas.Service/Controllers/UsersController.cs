using AppCitas.Service.Data;
using AppCitas.Service.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppCitas.Service.Controllers;

public class UsersController : BaseApiController
{
	private readonly DataContext _context;

	public UsersController(DataContext context)
	{
		_context = context;
	}

	// GET api/users
	[HttpGet]
	[AllowAnonymous]
	public ActionResult<IEnumerable<AppUser>> GetUsers()
	{
		var users = _context.Users.ToList();

		return users;
	}

	//GET api/users/{id}
    [HttpGet("{id}")]
	[Authorize]
    public async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
