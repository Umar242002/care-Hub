using CareHub.Models.DataBase;
using CareHub.Models.DbContext;
using CareHub.Models.Vm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CareHubContext _context;

        public AccountController(CareHubContext context)
        {
            _context = context;
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVm request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required.");

            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            return Ok(new { Message = "Login successful", User = user });
        }

        // POST: api/Account/CreateAccount
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] UserAccount request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email and password are required.");

            if (await _context.UserAccounts.AnyAsync(u => u.Email == request.Email))
                return BadRequest("An account with this email already exists.");

            var userAccount = new UserAccount
            {
                Email = request.Email,
                Password = request.Password,
                UserRole = request.UserRole,
                UserType = request.UserType,
                IsApproved = false, // Default to not approved
                ApprovedBy = string.Empty,
                IsUserBlock = false,
                BlockNote = string.Empty,
                AddedBy = "System",
                AddedDate = DateTime.UtcNow,
                InActiveBy = string.Empty,
                InActive = false,
                Name = request.Name
            };

            _context.UserAccounts.Add(userAccount);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Account created successfully", User = userAccount });
        }
    }
}

