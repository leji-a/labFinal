using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using lab4Final.Models;
using lab4Final.Services;

namespace lab4Final.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ImportarExcelController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ImportarExcelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirExcel(IFormFile excel)
        {
            try
            {
                var workbook = new XLWorkbook(excel.OpenReadStream());
                var hoja = workbook.Worksheet(1);
                var primeraFila = hoja.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                var ultimaFila = hoja.LastRowUsed().RangeAddress.LastAddress.RowNumber;

                List<Socio> socios = [];
                for (int i = primeraFila + 1; i <= ultimaFila; i++)
                {
                    var fila = hoja.Row(i);
                    Socio socio = new()
                    {
                        Nombre = fila.Cell(1).GetString(),
                        Apellido = fila.Cell(2).GetValue<string>(),
                        DNI = fila.Cell(3).GetValue<string>(),
                        Telefono = fila.Cell(4).GetValue<string>()
                    };
                    socios.Add(socio);
                }
                _context.Socios.AddRange(socios);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "Prestamos");
        }
    }
}
