using System.ComponentModel.DataAnnotations;

namespace lab4Final.Models
{
	public class Prestamo
	{
		public int Id { get; set; }
        [Display(Name = "Socio")]
        public int SocioId { get; set; }
        [Display(Name = "Libro")]
        public int LibroId { get; set; }
        [Display(Name = "Portada")]
        public string? ImagenLibro { get; set; }
        [Display(Name = "Fecha del préstamo")]
        public DateTime FechaPrestamo { get; set; }
        [Display(Name = "Fecha de devolución")]
        public DateTime? FechaDevolucion { get; set; }

        // Navegación
        public Socio? Socio { get; set; }
        public Libro? Libro { get; set; }
    }
}
