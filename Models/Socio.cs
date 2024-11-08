namespace lab4Final.Models
{
	public class Socio
	{
		public int Id { get; set; }
		public string? Nombre { get; set; }
		public string? Apellido { get; set; }
		public string? DNI { get; set; }
		public DateTime? FechaInscripcion { get; set; }
		public string? Telefono { get; set; }

		// Navegación
		public ICollection<Prestamo>? Prestamos { get; set; }
	}
}
