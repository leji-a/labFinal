using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4Final.Models;
using lab4Final.Services;
using Microsoft.AspNetCore.Hosting;

namespace lab4Final.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PrestamoController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Prestamo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prestamos.Include(p => p.Libro).Include(p => p.Socio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prestamo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamo/Create

        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");

            var socios = _context.Socios.Select(s => new 
            { 
                s.Id, 
                NombreCompleto = s.Nombre + " " + s.Apellido 
            });

            ViewData["SocioId"] = new SelectList(socios, "Id", "NombreCompleto");
            return View();
        }
        private async Task<string?> GuardarImagenAsync(IFormFile archivoFoto, string? imagenExistente = null)
        {
            if (archivoFoto == null || archivoFoto.Length == 0) return imagenExistente;

            // Ruta destino
            var pathDestino = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            Directory.CreateDirectory(pathDestino); // Crear el directorio si no existe

            // Nombre unico para imagen
            var archivoDestino = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivoFoto.FileName);

            // Ruta a guardar
            var rutaCompletaDestino = Path.Combine(pathDestino, archivoDestino);

            // Guardar la imagen
            using (var filestream = new FileStream(rutaCompletaDestino, FileMode.Create))
            {
                await archivoFoto.CopyToAsync(filestream);
                if (!string.IsNullOrEmpty(imagenExistente))
                {
                    var rutaArchivoViejo = Path.Combine(pathDestino, imagenExistente);
                    if (System.IO.File.Exists(rutaArchivoViejo))
                    {
                        System.IO.File.Delete(rutaArchivoViejo);
                    }
                }
            }
            return archivoDestino;
        }

        // POST: Prestamo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SocioId,LibroId,ImagenLibro,FechaPrestamo,FechaDevolucion")] Prestamo prestamo, IFormFile imagenLibro)
        {
            if (ModelState.IsValid)
            {
                prestamo.ImagenLibro = await GuardarImagenAsync(imagenLibro);
                prestamo.FechaPrestamo = DateTime.Now;
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");

            var socios = _context.Socios.Select(s => new
            {
                s.Id,
                NombreCompleto = s.Nombre + " " + s.Apellido
            });

            ViewData["SocioId"] = new SelectList(socios, "Id", "NombreCompleto");
            return View(prestamo);
        }

        // GET: Prestamo/Edit/5
        public async Task<IActionResult> Edit(int? id, [Bind("Id,SocioId,LibroId,FechaPrestamo,FechaDevolucion")] Prestamo prestamo, IFormFile imagenLibro)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid && imagenLibro != null && imagenLibro.Length > 0)
            {
                try
                {
                    if (imagenLibro != null)
                    {
                        prestamo.ImagenLibro = await GuardarImagenAsync(imagenLibro);
                    }

                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");

            var socios = _context.Socios.Select(s => new
            {
                s.Id,
                NombreCompleto = s.Nombre + " " + s.Apellido
            });

            ViewData["SocioId"] = new SelectList(socios, "Id", "NombreCompleto");
            return View(prestamo);
        }

        // POST: Prestamo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SocioId,LibroId,ImagenLibro,FechaPrestamo,FechaDevolucion")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Titulo");

            var socios = _context.Socios.Select(s => new
            {
                s.Id,
                NombreCompleto = s.Nombre + " " + s.Apellido
            });

            ViewData["SocioId"] = new SelectList(socios, "Id", "NombreCompleto");
            return View(prestamo);
        }

        // GET: Prestamo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .Include(p => p.Socio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }
    }
}
