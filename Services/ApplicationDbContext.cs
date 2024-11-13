using lab4Final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace lab4Final.Services
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Socio> Socios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorLibro>().HasKey(al => new { al.AutorId, al.LibroId });

            modelBuilder.Entity<AutorLibro>()
                .HasOne(al => al.Autor)
                .WithMany(a => a.AutorLibros)
                .HasForeignKey(al => al.AutorId);

            modelBuilder.Entity<AutorLibro>()
                .HasOne(al => al.Libro)
                .WithMany(l => l.AutorLibros)
                .HasForeignKey(al => al.LibroId);
        }
    }
}
