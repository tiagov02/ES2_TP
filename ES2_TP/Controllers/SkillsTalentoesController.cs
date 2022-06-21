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
    public class SkillsTalentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkillsTalentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SkillsTalentoes
        public async Task<IActionResult> Index()
        {
              return _context.SkillsTalento != null ? 
                          View(await _context.SkillsTalento.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SkillsTalento'  is null.");
        }

        // GET: SkillsTalentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SkillsTalento == null)
            {
                return NotFound();
            }

            var skillsTalento = await _context.SkillsTalento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillsTalento == null)
            {
                return NotFound();
            }

            return View(skillsTalento);
        }

        // GET: SkillsTalentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SkillsTalentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,preco,numHoras")] SkillsTalento skillsTalento)
        {
            if (ModelState.IsValid)
            {
                skillsTalento.Id = Guid.NewGuid();
                _context.Add(skillsTalento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skillsTalento);
        }

        // GET: SkillsTalentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SkillsTalento == null)
            {
                return NotFound();
            }

            var skillsTalento = await _context.SkillsTalento.FindAsync(id);
            if (skillsTalento == null)
            {
                return NotFound();
            }
            return View(skillsTalento);
        }

        // POST: SkillsTalentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,preco,numHoras")] SkillsTalento skillsTalento)
        {
            if (id != skillsTalento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skillsTalento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillsTalentoExists(skillsTalento.Id))
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
            return View(skillsTalento);
        }

        // GET: SkillsTalentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SkillsTalento == null)
            {
                return NotFound();
            }

            var skillsTalento = await _context.SkillsTalento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skillsTalento == null)
            {
                return NotFound();
            }

            return View(skillsTalento);
        }

        // POST: SkillsTalentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SkillsTalento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SkillsTalento'  is null.");
            }
            var skillsTalento = await _context.SkillsTalento.FindAsync(id);
            if (skillsTalento != null)
            {
                _context.SkillsTalento.Remove(skillsTalento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillsTalentoExists(Guid id)
        {
          return (_context.SkillsTalento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
