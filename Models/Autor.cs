using System.ComponentModel.DataAnnotations;

namespace lab4Final.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        [Display (Name = "Nacionalidad")]
        public int PaisId { get; set; }

        public Pais? Pais { get; set; }
        public ICollection<AutorLibro>? AutorLibros { get; set; }
    }
}
