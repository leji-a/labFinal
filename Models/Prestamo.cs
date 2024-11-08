namespace lab4Final.Models
{
	public class Prestamo
	{
		public int Id { get; set; }
		public int SocioId { get; set; }
		public int LibroId { get; set; }
		public string? ImagenLibro { get; set; }
		public DateTime FechaPrestamo { get; set; }
		public DateTime FechaDevolucion { get; set; }

		// Navegación
		public Socio? Socio { get; set; }
		public Libro? Libro { get; set; }
	}
}
