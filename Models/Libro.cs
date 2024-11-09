using System.ComponentModel.DataAnnotations;

namespace lab4Final.Models
{
	public class Libro
	{
		public int Id { get; set; }
		public string? Titulo { get; set; }
        [Display(Name = "Fecha de publicación")]
        public DateTime FechaPublicacion { get; set; }
        [Display(Name = "Editorial")]
        public int EditorialId { get; set; }


		// Navegación
		public Editorial? Editorial { get; set; }
		public ICollection<AutorLibro>? AutorLibros { get; set; }
		public ICollection<Prestamo>? Prestamos { get; set; }
	}
}
