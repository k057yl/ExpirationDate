using ExpirationDate.Data;
using ExpirationDate.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpirationDate.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Print()
        {
            var users = await _context.Users.ToListAsync();

            var userDTOs = users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email
            }).ToList();

            return View(userDTOs);
        }
    }
}