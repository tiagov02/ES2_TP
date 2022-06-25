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
    /*public class TipoVista
    {
        public bool IsPublic { get; set; }
        public string Tipo_De_Perfil { get; set; }
    }*/
    public class TalentoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TalentoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Talentoes
        /*public async Task<IActionResult> Index()
        {
              return _context.Talento != null ? 
                          View(await _context.Talento.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Talento'  is null.");
        }*/
        //[HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {

            bool showPrivateTalents = User.IsInRole("Admin");

            var talentos = from m in _context.Talento
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                talentos = talentos.
                    Where(s => s.Skill.descricao!.ToLower().Contains(searchString) 
                    || s.nome.ToLower().Contains(searchString)).
                    OrderBy(talentos=>talentos.nome);
            }

            if (showPrivateTalents is false)
            {
                talentos = talentos.Where(e => e.isPublic == true);
            }
            
            talentos = talentos.Include(e => e.Categoria).Include(e => e.Skill);

            ViewBag.searchstring = searchString;

            return View(await talentos.ToListAsync());
        }

        // GET: Talentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Talento == null)
            {
                return NotFound();
            }

            var talento = await _context.Talento.
                Include(e => e.Categoria).Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talento == null)
            {
                return NotFound();
            }

            return View(talento);
        }

        // GET: Talentoes/Create
        public IActionResult Create()
        {
            /*var talentoCategoria = new List<SelectListItem>();
            foreach (Categoria cat in _context.Categoria)
            {
                talentoCategoria.Add(new SelectListItem { Text = cat.descricao, Value = cat.Id.ToString() });
            }*/
            /*List<TipoVista> vista = new List<TipoVista>();
            vista.Add( new TipoVista() { IsPublic = true, Tipo_De_Perfil="Público"  });
            vista.Add(new TipoVista() { IsPublic = false, Tipo_De_Perfil = "Privado" });
            ViewData["Perfil"] = new SelectList(vista, "IsPublic", "Tipo_De_Perfil");*/
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "descricao");
            ViewData["Skill"] = new SelectList(_context.Skills.OrderBy(s => s.descricao), "Id", "descricao");
            return View();
        }

        // POST: Talentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,precoHora,horasExperiencia,nome,pais,email,IdCategoria,IdSkill")] Talento talento)
        { 
            if (ModelState.IsValid)
            {
                talento.Id = Guid.NewGuid();
                var cat = await _context.Categoria.FindAsync(talento.IdCategoria);
                talento.Categoria = cat;
                var sk = await _context.Skills.FindAsync(talento.IdSkill);
                talento.Skill = sk;
                _context.Add(talento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talento);
        }

        // GET: Talentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Talento == null)
            {
                return NotFound();
            }
            ViewData["Categoria"] = new SelectList(_context.Categoria, "Id", "descricao");
            ViewData["Skill"] = new SelectList(_context.Skills, "Id", "descricao");

            var talento = await _context.Talento.FindAsync(id);
            if (talento == null)
            {
                return NotFound();
            }
            return View(talento);
        }

        // POST: Talentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,precoHora,horasExperiencia,nome,pais,email, IdCategoria, IdSkill")] Talento talento)
        {
            if (id != talento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var cat = await _context.Categoria.FindAsync(talento.IdCategoria);
                talento.Categoria = cat;
                var sk = await _context.Skills.FindAsync(talento.IdSkill);
                talento.Skill = sk;
                try
                {
                    _context.Update(talento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalentoExists(talento.Id))
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
            return View(talento);
        }

        // GET: Talentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Talento == null)
            {
                return NotFound();
            }

            var talento = await _context.Talento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talento == null)
            {
                return NotFound();
            }

            return View(talento);
        }

        // POST: Talentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Talento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Talento'  is null.");
            }
            var talento = await _context.Talento.FindAsync(id);
            if (talento != null)
            {
                _context.Talento.Remove(talento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalentoExists(Guid id)
        {
          return (_context.Talento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
