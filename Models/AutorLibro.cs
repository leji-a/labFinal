namespace lab4Final.Models
{
    public class AutorLibro
    {
        public int AutorId { get; set; }
        public int LibroId { get; set; }

        public Autor? Autor { get; set; }
        public Libro? Libro { get; set; }
    }
}