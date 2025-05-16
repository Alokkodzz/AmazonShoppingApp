using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserDbContext _dbContext;

    public UsersController(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto dto)
    {
        // Basic validation omitted for brevity

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password)
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    private string HashPassword(string password)
    {
        // Implement a secure hashing function here
        return password; // placeholder - never store plain text passwords in prod!
    }
}

