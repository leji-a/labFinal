namespace lab4Final.Models
{
    public class Pais
	{
		public int Id { get; set; }
		public string? Nombre { get; set; }

		// Navegación
		public ICollection<Autor>? Autores { get; set; }
	}
}
