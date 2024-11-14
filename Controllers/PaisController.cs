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
    public class PaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaisController(ApplicationDbContext context)
        {
            _context = context;
            AddCountriesIfNotExists();
        }
        private void AddCountriesIfNotExists()
        {
            // Lista de países
            var countries = new List<string>
            {
                "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita", "Argentina", "Armenia", "Australia",
                "Austria", "Azerbaiyán", "Bahamas", "Baréin", "Bangladesh", "Barbados", "Bélgica", "Belice", "Benín", "Bielorrusia",
                "Birmania", "Bolivia", "Bosnia y Herzegovina", "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi", "Cabo Verde",
                "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo", "Corea del Norte",
                "Corea del Sur", "Costa Rica", "Costa de Marfil", "Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador",
                "Emiratos Árabes Unidos", "Eritrea", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Esuatini", "Etiopía",
                "Fiji", "Filipinas", "Finlandia", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Granada", "Guatemala", "Guinea",
                "Guinea Ecuatorial", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "India", "Indonesia", "Irak", "Irán",
                "Irlanda", "Islas Marshall", "Islas Salomón", "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia",
                "Kirguistán", "Kiribati", "Kuwait", "Laos", "Lesoto", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania",
                "Luxemburgo", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Marianas del Norte", "Mauricio",
                "Mauritania", "México", "Micronesia", "Mónaco", "Mongolia", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger",
                "Nigeria", "Noruega", "Nueva Zelanda", "Níger", "Omán", "Países Bajos", "Pakistán", "Palaos", "Panamá", "Papúa Nueva Guinea",
                "Paraguay", "Perú", "Polonia", "Portugal", "Reino Unido", "República Checa", "República Dominicana", "Reunión", "Rumania",
                "Rusia", "Ruanda", "Samoa", "San Cristóbal y Nieves", "San Marino", "San Vicente y las Granadinas", "Santo Tomé y Príncipe", "Senegal",
                "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sri Lanka", "Sudáfrica", "Sudán", "Sudán del Sur",
                "Suecia", "Suiza", "Surinam", "Svalbard", "Siria", "Tailandia", "Tanzania", "Tayikistán", "Timor Oriental", "Togo", "Tonga",
                "Trinidad y Tobago", "Tunez", "Turkmenistán", "Turquía", "Tuvalu", "Uganda", "Ukrania", "Uruguay", "Uzbekistán", "Vanuatu",
                "Vaticano", "Venezuela", "Vietnam", "Yemen", "Yibuti", "Zambia", "Zimbabue"
            };

            foreach (var countryName in countries)
            {
                // Verifica si el país ya existe en la base de datos
                if (!_context.Paises.Any(p => p.Nombre == countryName))
                {
                    // Si no existe, lo agrega
                    _context.Paises.Add(new Pais { Nombre = countryName });
                }
            }

            // Guarda los cambios en la base de datos
            _context.SaveChanges();
        }



        // GET: Pais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paises.ToListAsync());
        }

        // GET: Pais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: Pais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Pais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Pais pais)
        {
            if (id != pais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisExists(pais.Id))
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
            return View(pais);
        }

        // GET: Pais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pais = await _context.Paises
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pais == null)
            {
                return NotFound();
            }

            return View(pais);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais != null)
            {
                _context.Paises.Remove(pais);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
