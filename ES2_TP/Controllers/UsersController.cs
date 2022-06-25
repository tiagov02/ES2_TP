using ES2_TP.Data;
using ES2_TP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES2_TP.Controllers
{
    public class ModelUser
    {
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public int UserType { get; set; } 
        public string Password { get; set; }
    }
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

                if (await _userManager.IsInRoleAsync(item, "Manager"))
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
        /*public FileStreamResult CreateFile(string user, string pwd)
        {
            //todo: add some data from your database into that string:
            var string_with_your_data = "USER: "+user+"\nPassword: "+pwd;

            var byteArray = System.Text.Encoding.ASCII.GetBytes(string_with_your_data);
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", "UTILIZADOR.txt");
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,PhoneNumber,UserType")] AplicationUser model)
        { 
            if (ModelState.IsValid)
            {
                AplicationUser user = new AplicationUser()
                {
                    //Id = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserType = model.UserType,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                await _userManager.CreateAsync(user,"23456qA!");
                model.Id = user.Id;
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
                        if (!await _roleManager.RoleExistsAsync("Manager"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Manager"));
                        }
                        await _userManager.AddToRoleAsync(user, "Manager");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            /*if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                user.UserType = 1;
            }

            if (await _userManager.IsInRoleAsync(user, "User"))
            {
                user.UserType = 2;
            }

            if (await _userManager.IsInRoleAsync(user, "UserManager"))
            {
                user.UserType = 3;
            }
            if (user.UserType == 1)
            {
                ViewBag.userType = "Admin";
            }

            if(user.UserType == 2)
            {
                ViewBag.userType = "User";
            }

            if(user.UserType == 3)
            {
                ViewBag.userType = "User Manager";
            }
            ViewBag.userType = user.UserType;*/
            return View(user);
        }
    }
}
