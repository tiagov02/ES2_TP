using ES2_TP.Data;
using ES2_TP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES2_TP.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (var item in users)
            {
                if (await _userManager.IsInRoleAsync(item, "Admin"))
                {
                    item.UserType = 1;
                }

                if (await _userManager.IsInRoleAsync(item, "User"))
                {
                    item.UserType = 2;
                }

                if (await _userManager.IsInRoleAsync(item, "UserManager"))
                {
                    item.UserType = 3;
                }
            }

            return View(users);
        }
    }
}
