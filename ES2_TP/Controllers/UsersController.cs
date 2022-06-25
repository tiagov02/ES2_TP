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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Password")] AplicationUser model)
        {
            if (ModelState.IsValid)
            {
                AplicationUser user = new AplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserType = model.UserType,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                await _userManager.CreateAsync(user, model.Password);
                if (model.UserType == 1)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    if (model.UserType == 2)
                    {
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        if (!await _roleManager.RoleExistsAsync("UserManager"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("UserManager"));
                        }
                        await _userManager.AddToRoleAsync(user, "UserManager");
                    }
                }
            }
            return View(model);
        }
    }
}
