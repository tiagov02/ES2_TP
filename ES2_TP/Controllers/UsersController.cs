using ES2_TP.Data;
using ES2_TP.Models;
using Microsoft.AspNetCore.Authorization;
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
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Talentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserName,Email,PhoneNumber,UserType")] AplicationUser model)
        {
            var us = await _userManager.FindByIdAsync(id.ToString());
            /*if (id.ToString() != model.Id)
            {
                return NotFound();
            }*/
            AplicationUser user = new AplicationUser()
            {
                //Id = id.ToString(),
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserType = model.UserType,
                SecurityStamp = us.SecurityStamp,
                PasswordHash = us.PasswordHash,
            };
            

            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(user);
                //await _userManager.SetEmailAsync(us, model.Email);
                //await _userManager.SetUserNameAsync(us, model.Email);
                if (us.UserType == 1)
                {
                    await _userManager.RemoveFromRoleAsync(us,"Admin");
                }
                else
                {
                    if (us.UserType == 2)
                    {
                        await _userManager.RemoveFromRoleAsync(us, "User");
                    }
                    else
                    {
                        if(us.UserType == 3)
                        {
                            await _userManager.RemoveFromRoleAsync(us, "Manager");
                        }
                    }
                }
                    
                if (model.UserType == 1)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    await _userManager.AddToRoleAsync(us, "Admin");
                }
               
                    if (model.UserType == 2)
                    {
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }
                        await _userManager.AddToRoleAsync(us, "User");
                    }
                        if(model.UserType == 3)
                        {
                            if (!await _roleManager.RoleExistsAsync("Manager"))
                            {
                                await _roleManager.CreateAsync(new IdentityRole("Manager"));
                            }
                            await _userManager.AddToRoleAsync(us, "Manager");
                        }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
            if (await _userManager.IsInRoleAsync(user, "Admin"))
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
            return View(user);
        }
    }
}
