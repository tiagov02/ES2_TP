using ES2_TP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ES2_TP.Controllers
{
    public class AplicationUser : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public class AplicationUserController : Controller
        {
            private readonly ApplicationDbContext _context;

            public AplicationUserController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: Clientes
            public async Task<IActionResult> Index()
            {
                return _context.Cliente != null ?
                            View(await _context.Cliente.ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Cliente'  is null.");
            }

            // GET: Clientes/Details/5
            public async Task<IActionResult> Details(Guid? id)
            {
                if (id == null || _context.Cliente == null)
                {
                    return NotFound();
                }

                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }

            // GET: Clientes/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Clientes/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id")] AplicationUser cliente)
            {
                if (ModelState.IsValid)
                {
                    cliente.Id = Guid.NewGuid();
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(cliente);
            }

            // GET: Clientes/Edit/5
            public async Task<IActionResult> Edit(Guid? id)
            {
                if (id == null || _context.AplicationUser == null)
                {
                    return NotFound();
                }

                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);
            }

            // POST: Clientes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(Guid id, [Bind("Id")] AplicationUser cliente)
            {
                if (id != cliente.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClienteExists(cliente.Id))
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
                return View(cliente);
            }

            // GET: Clientes/Delete/5
            public async Task<IActionResult> Delete(Guid? id)
            {
                if (id == null || _context.Cliente == null)
                {
                    return NotFound();
                }

                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }

            // POST: Clientes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                if (_context.Cliente == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Cliente'  is null.");
                }
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    _context.Cliente.Remove(cliente);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ClienteExists(Guid id)
            {
                return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
    }
}
