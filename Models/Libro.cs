namespace lab4Final.Models
{
	public class Libro
	{
		public int Id { get; set; }
		public string? Titulo { get; set; }
		public DateTime FechaPublicacion { get; set; }
		public int EditorialId { get; set; }

		// Navegación
		public Editorial? Editorial { get; set; }
		public ICollection<AutorLibro>? AutorLibros { get; set; }
		public ICollection<Prestamo>? Prestamos { get; set; }
	}
}
