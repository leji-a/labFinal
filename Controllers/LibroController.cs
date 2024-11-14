using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4Final.Models;
using lab4Final.Services;
using Microsoft.AspNetCore.Authorization;

namespace lab4Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LibroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libro
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                .Include(l => l.Editorial)
                .ThenInclude(e => e.Pais)
                .ToListAsync();
            return View(libros);
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Editorial)
                .ThenInclude(e => e.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            ViewData["EditorialId"] = new SelectList(_context.Editoriales.Select(e => new
            {
                e.Id, NombreConPais = e.Nombre + " (" + e.Pais.Nombre + ")"
            }),
            "Id",
            "NombreConPais"
            );

            ViewBag.Autores = _context.Autores
                .Select(a => new
                {
                    a.Id, NombreCompleto = a.Nombre + " " + a.Apellido 
                }).ToList();
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,FechaPublicacion,EditorialId")] Libro libro, int AutorId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();

                var autorLibro = new AutorLibro
                {
                    LibroId = libro.Id, AutorId = AutorId
                };

                _context.Add(autorLibro);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["EditorialId"] = new SelectList(_context.Editoriales.Select(e => new
            {
                e.Id, NombreConPais = e.Nombre + " (" + e.Pais.Nombre + ")"
            }),
            "Id",
            "NombreConPais"
            );
            ViewBag.Autores = _context.Autores.Select(a => new
                {
                    a.Id, NombreCompleto = a.Nombre + " " + a.Apellido
                }).ToList();
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["EditorialId"] = new SelectList(_context.Editoriales.Select(e => new
            {
                e.Id, NombreConPais = e.Nombre + " (" + e.Pais.Nombre + ")"
            }),
            "Id",
            "NombreConPais"
            );
            return View(libro);
        }

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,FechaPublicacion,EditorialId")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            ViewData["EditorialId"] = new SelectList(_context.Editoriales.Select(e => new
            {
                e.Id, NombreConPais = e.Nombre + " (" + e.Pais.Nombre + ")"
            }),
            "Id",
            "NombreConPais"
            );
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
            .Include(l => l.Editorial)
            .ThenInclude(e => e.Pais)
            .FirstOrDefaultAsync(m => m.Id == id);
            
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}
