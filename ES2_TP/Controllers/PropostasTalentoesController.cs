using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ES2_TP.Data;
using ES2_TP.Models;
using Microsoft.AspNetCore.Authorization;

namespace ES2_TP.Controllers
{
    [Authorize]
    public class PropostasTalentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropostasTalentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropostasTalentoes
        public async Task<IActionResult> Index()
        {
              return _context.PropostasTalento != null ? 
                          View(await _context.PropostasTalento.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PropostasTalento'  is null.");
        }

        // GET: PropostasTalentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PropostasTalento == null)
            {
                return NotFound();
            }

            var propostasTalento = await _context.PropostasTalento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propostasTalento == null)
            {
                return NotFound();
            }

            return View(propostasTalento);
        }

        // GET: PropostasTalentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropostasTalentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,tempoEstimado,valor")] PropostasTalento propostasTalento)
        {
            if (ModelState.IsValid)
            {
                propostasTalento.Id = Guid.NewGuid();
                _context.Add(propostasTalento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propostasTalento);
        }

        // GET: PropostasTalentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PropostasTalento == null)
            {
                return NotFound();
            }

            var propostasTalento = await _context.PropostasTalento.FindAsync(id);
            if (propostasTalento == null)
            {
                return NotFound();
            }
            return View(propostasTalento);
        }

        // POST: PropostasTalentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,tempoEstimado,valor")] PropostasTalento propostasTalento)
        {
            if (id != propostasTalento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propostasTalento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropostasTalentoExists(propostasTalento.Id))
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
            return View(propostasTalento);
        }

        // GET: PropostasTalentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PropostasTalento == null)
            {
                return NotFound();
            }

            var propostasTalento = await _context.PropostasTalento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propostasTalento == null)
            {
                return NotFound();
            }

            return View(propostasTalento);
        }

        // POST: PropostasTalentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PropostasTalento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PropostasTalento'  is null.");
            }
            var propostasTalento = await _context.PropostasTalento.FindAsync(id);
            if (propostasTalento != null)
            {
                _context.PropostasTalento.Remove(propostasTalento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropostasTalentoExists(Guid id)
        {
          return (_context.PropostasTalento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
