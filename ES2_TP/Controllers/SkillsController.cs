﻿using System;
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
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
              return _context.Skills != null ? 
                          View(await _context.Skills.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Skills'  is null.");
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,descricao")] Skills skills)
        {
            if (ModelState.IsValid)
            {
                skills.Id = Guid.NewGuid();
                _context.Add(skills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skills);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills.FindAsync(id);
            if (skills == null)
            {
                return NotFound();
            }
            return View(skills);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,descricao")] Skills skills)
        {
            if (id != skills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillsExists(skills.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skills);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Skills == null)
            {
                return NotFound();
            }

            var skills = await _context.Skills
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Skills == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Skills'  is null.");
            }
            var skills = await _context.Skills.FindAsync(id);
            if (skills != null)
            {
                _context.Skills.Remove(skills);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillsExists(Guid id)
        {
          return (_context.Skills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
