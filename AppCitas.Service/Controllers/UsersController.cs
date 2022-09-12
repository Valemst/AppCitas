using AppCitas.Service.Data;
using AppCitas.Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppCitas.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly DataContext _context;

	public UsersController(DataContext context)
	{
		_context = context;
	}

	// GET api/users
	[HttpGet]
	public ActionResult<IEnumerable<AppUser>> GetUsers()
	{
		var users = _context.Users.ToList();

		return users;
	}

	//GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
