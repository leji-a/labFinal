using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4Final.Models;
using lab4Final.Services;

namespace lab4Final.Controllers
{
    public class AutorLibroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutorLibroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AutorLibro
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AutorLibros.Include(a => a.Autor).Include(a => a.Libro);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AutorLibro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutorLibros
                .Include(a => a.Autor)
                .Include(a => a.Libro)
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autorLibro == null)
            {
                return NotFound();
            }

            return View(autorLibro);
        }

        // GET: AutorLibro/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Id");
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id");
            return View();
        }

        // POST: AutorLibro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorId,LibroId")] AutorLibro autorLibro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autorLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Id", autorLibro.AutorId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", autorLibro.LibroId);
            return View(autorLibro);
        }

        // GET: AutorLibro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutorLibros.FindAsync(id);
            if (autorLibro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Id", autorLibro.AutorId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", autorLibro.LibroId);
            return View(autorLibro);
        }

        // POST: AutorLibro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutorId,LibroId")] AutorLibro autorLibro)
        {
            if (id != autorLibro.AutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autorLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorLibroExists(autorLibro.AutorId))
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
            ViewData["AutorId"] = new SelectList(_context.Autores, "Id", "Id", autorLibro.AutorId);
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", autorLibro.LibroId);
            return View(autorLibro);
        }

        // GET: AutorLibro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorLibro = await _context.AutorLibros
                .Include(a => a.Autor)
                .Include(a => a.Libro)
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autorLibro == null)
            {
                return NotFound();
            }

            return View(autorLibro);
        }

        // POST: AutorLibro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorLibro = await _context.AutorLibros.FindAsync(id);
            if (autorLibro != null)
            {
                _context.AutorLibros.Remove(autorLibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorLibroExists(int id)
        {
            return _context.AutorLibros.Any(e => e.AutorId == id);
        }
    }
}
