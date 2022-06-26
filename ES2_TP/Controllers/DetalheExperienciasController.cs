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
    public class DetalheExperienciasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetalheExperienciasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetalheExperiencias
        public async Task<IActionResult> Index()
        {
              return _context.DetalheExperiencia != null ? 
                          View(await _context.DetalheExperiencia.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DetalheExperiencia'  is null.");
        }

        // GET: DetalheExperiencias/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DetalheExperiencia == null)
            {
                return NotFound();
            }

            var detalheExperiencia = await _context.DetalheExperiencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheExperiencia == null)
            {
                return NotFound();
            }

            return View(detalheExperiencia);
        }

        // GET: DetalheExperiencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalheExperiencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,descricao,dt_ini,dt_fim")] DetalheExperiencia detalheExperiencia)
        {
            if (ModelState.IsValid)
            {
                detalheExperiencia.Id = Guid.NewGuid();
                _context.Add(detalheExperiencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalheExperiencia);
        }

        // GET: DetalheExperiencias/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DetalheExperiencia == null)
            {
                return NotFound();
            }

            var detalheExperiencia = await _context.DetalheExperiencia.FindAsync(id);
            if (detalheExperiencia == null)
            {
                return NotFound();
            }
            return View(detalheExperiencia);
        }

        // POST: DetalheExperiencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,descricao,dt_ini,dt_fim")] DetalheExperiencia detalheExperiencia)
        {
            if (id != detalheExperiencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalheExperiencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalheExperienciaExists(detalheExperiencia.Id))
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
            return View(detalheExperiencia);
        }

        // GET: DetalheExperiencias/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DetalheExperiencia == null)
            {
                return NotFound();
            }

            var detalheExperiencia = await _context.DetalheExperiencia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheExperiencia == null)
            {
                return NotFound();
            }

            return View(detalheExperiencia);
        }

        // POST: DetalheExperiencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DetalheExperiencia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DetalheExperiencia'  is null.");
            }
            var detalheExperiencia = await _context.DetalheExperiencia.FindAsync(id);
            if (detalheExperiencia != null)
            {
                _context.DetalheExperiencia.Remove(detalheExperiencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalheExperienciaExists(Guid id)
        {
          return (_context.DetalheExperiencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
