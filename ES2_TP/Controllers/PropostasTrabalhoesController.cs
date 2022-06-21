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
    public class PropostasTrabalhoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropostasTrabalhoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropostasTrabalhoes
        public async Task<IActionResult> Index()
        {
              return _context.PropostasTrabalho != null ? 
                          View(await _context.PropostasTrabalho.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PropostasTrabalho'  is null.");
        }

        // GET: PropostasTrabalhoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PropostasTrabalho == null)
            {
                return NotFound();
            }

            var propostasTrabalho = await _context.PropostasTrabalho
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propostasTrabalho == null)
            {
                return NotFound();
            }

            return View(propostasTrabalho);
        }

        // GET: PropostasTrabalhoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropostasTrabalhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nome,descricao")] PropostasTrabalho propostasTrabalho)
        {
            if (ModelState.IsValid)
            {
                propostasTrabalho.Id = Guid.NewGuid();
                _context.Add(propostasTrabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propostasTrabalho);
        }

        // GET: PropostasTrabalhoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PropostasTrabalho == null)
            {
                return NotFound();
            }

            var propostasTrabalho = await _context.PropostasTrabalho.FindAsync(id);
            if (propostasTrabalho == null)
            {
                return NotFound();
            }
            return View(propostasTrabalho);
        }

        // POST: PropostasTrabalhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,nome,descricao")] PropostasTrabalho propostasTrabalho)
        {
            if (id != propostasTrabalho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propostasTrabalho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropostasTrabalhoExists(propostasTrabalho.Id))
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
            return View(propostasTrabalho);
        }

        // GET: PropostasTrabalhoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PropostasTrabalho == null)
            {
                return NotFound();
            }

            var propostasTrabalho = await _context.PropostasTrabalho
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propostasTrabalho == null)
            {
                return NotFound();
            }

            return View(propostasTrabalho);
        }

        // POST: PropostasTrabalhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PropostasTrabalho == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PropostasTrabalho'  is null.");
            }
            var propostasTrabalho = await _context.PropostasTrabalho.FindAsync(id);
            if (propostasTrabalho != null)
            {
                _context.PropostasTrabalho.Remove(propostasTrabalho);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropostasTrabalhoExists(Guid id)
        {
          return (_context.PropostasTrabalho?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
