using Microsoft.EntityFrameworkCore;
using GestionUsuarios.Models;

namespace GestionUsuarios.Data
{
    public class UsuarioDbContext : DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Rut).IsUnique();
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Rut = "11111111-1", Nombre = "Juan Perez", CorreoElectronico = "juan@example.com", Genero = "masculino", FechaNacimiento = new DateTime(1990, 1, 1) },
                new Usuario { Id = 2, Rut = "22222222-2", Nombre = "Maria Gomez", CorreoElectronico = "maria@example.com", Genero = "femenino", FechaNacimiento = new DateTime(1995, 2, 2) },
                new Usuario { Id = 3, Rut = "33333333-3", Nombre = "Carlos Fernandez", CorreoElectronico = "carlos@example.com", Genero = "masculino", FechaNacimiento = new DateTime(1988, 3, 3) },
                new Usuario { Id = 4, Rut = "44444444-4", Nombre = "Ana Martinez", CorreoElectronico = "ana@example.com", Genero = "femenino", FechaNacimiento = new DateTime(1992, 4, 4) },
                new Usuario { Id = 5, Rut = "55555555-5", Nombre = "Luis Ramirez", CorreoElectronico = "luis@example.com", Genero = "masculino", FechaNacimiento = new DateTime(1993, 5, 5) },
                new Usuario { Id = 6, Rut = "66666666-6", Nombre = "Beatriz Soto", CorreoElectronico = "beatriz@example.com", Genero = "femenino", FechaNacimiento = new DateTime(1991, 6, 6) },
                new Usuario { Id = 7, Rut = "77777777-7", Nombre = "Josefa Valenzuela", CorreoElectronico = "josefa@example.com", Genero = "femenino", FechaNacimiento = new DateTime(1989, 7, 7) },
                new Usuario { Id = 8, Rut = "88888888-8", Nombre = "Pedro Ortiz", CorreoElectronico = "pedro@example.com", Genero = "masculino", FechaNacimiento = new DateTime(1994, 8, 8) },
                new Usuario { Id = 9, Rut = "99999999-9", Nombre = "Elena Rojas", CorreoElectronico = "elena@example.com", Genero = "femenino", FechaNacimiento = new DateTime(1987, 9, 9) },
                new Usuario { Id = 10, Rut = "10101010-0", Nombre = "Sebastian Gutierrez", CorreoElectronico = "sebastian@example.com", Genero = "masculino", FechaNacimiento = new DateTime(1996, 10, 10) }
            );
        }
    }
}
