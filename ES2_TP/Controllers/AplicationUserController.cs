using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ES2_TP.Data;
using ES2_TP.Models;

namespace ES2_TP.Controllers
{
    public class AplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AplicationUserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,PhoneNumber,UserType,Password")] AplicationUser user)
        {
            if (ModelState.IsValid)
            {
                AplicationUser u= new AplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserType = user.UserType,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                //await _context.AspNetUsers.CreateAsync(user, user.Password);
                //Duvida
            }
            return View();
        }
    }
}
