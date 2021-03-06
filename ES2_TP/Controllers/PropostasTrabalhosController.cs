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
    public class PropostasTrabalhosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropostasTrabalhosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropostasTrabalhos
        public async Task<IActionResult> Index()
        {
              return _context.PropostasTrabalho != null ? 
                          View(await _context.PropostasTrabalho.Include(e=>e.Categoria).Include(e=>e.Skill).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PropostasTrabalho'  is null.");
        }

        // GET: PropostasTrabalhos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PropostasTrabalho == null)
            {
                return NotFound();
            }

            var propostasTrabalho = await _context.PropostasTrabalho
                .Include(e => e.Categoria).Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propostasTrabalho == null)
            {
                return NotFound();
            }

            return View(propostasTrabalho);
        }
        public async Task<IActionResult> ApresentaPropostas(Guid id)
        {

            bool showPrivateTalents = User.IsInRole("Admin");

            var talentos = from m in _context.Talento
                           select m;
            var propostasTrabalho = await _context.PropostasTrabalho
                .Include(e => e.Categoria).Include(e => e.Skill).
                FirstOrDefaultAsync(m => m.Id == id);
            string searchString = propostasTrabalho.Skill.descricao;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                talentos = talentos.
                    Where(s => s.Skill.descricao!.ToLower().Contains(searchString)).
                    OrderBy(talentos => talentos.nome);
            }

            if (showPrivateTalents is false)
            {
                talentos = talentos.Where(e => e.isPublic == true);
            }

            talentos = talentos.Include(e => e.Categoria).Include(e => e.Skill);

            ViewBag.searchstring = searchString;

            return View(await talentos.ToListAsync());
        }

        // GET: PropostasTrabalhos/Create
        public IActionResult Create()
        {
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "descricao");
            ViewData["Skill"] = new SelectList(_context.Skills, "Id", "descricao");
            return View();
        }

        // POST: PropostasTrabalhos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnosExperiencia,TotalHoras,Nome,Descricao,IdCategoria,IdSkill")] PropostasTrabalho propostasTrabalho)
        {
            if (ModelState.IsValid)
            {
                propostasTrabalho.Id = Guid.NewGuid();
                var cat = await _context.Categoria.FindAsync(propostasTrabalho.IdCategoria);
                propostasTrabalho.Categoria = cat;
                var sk = await _context.Skills.FindAsync(propostasTrabalho.IdSkill);
                propostasTrabalho.Skill = sk;
                _context.Add(propostasTrabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propostasTrabalho);
        }

        // GET: PropostasTrabalhos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PropostasTrabalho == null)
            {
                return NotFound();
            }
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "descricao");
            ViewData["Skill"] = new SelectList(_context.Skills, "Id", "descricao");

            var propostasTrabalho = await _context.PropostasTrabalho.FindAsync(id);
            if (propostasTrabalho == null)
            {
                return NotFound();
            }
            return View(propostasTrabalho);
        }

        // POST: PropostasTrabalhos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AnosExperiencia,TotalHoras,Nome,Descricao,IdCategoria,IdSkill")] PropostasTrabalho propostasTrabalho)
        {
            if (id != propostasTrabalho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cat = await _context.Categoria.FindAsync(propostasTrabalho.IdCategoria);
                propostasTrabalho.Categoria = cat;
                var sk = await _context.Skills.FindAsync(propostasTrabalho.IdSkill);
                propostasTrabalho.Skill = sk;
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

        // GET: PropostasTrabalhos/Delete/5
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

        // POST: PropostasTrabalhos/Delete/5
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
