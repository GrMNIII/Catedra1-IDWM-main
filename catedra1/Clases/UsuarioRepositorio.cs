using Microsoft.EntityFrameworkCore;
using GestionUsuarios.Data;
using GestionUsuarios.Models;

namespace GestionUsuarios.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly UsuarioDbContext _context;

        public UsuarioRepositorio(UsuarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios(string orden, string genero)
        {
            var consulta = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(genero))
            {
                consulta = consulta.Where(u => u.Genero == genero);
            }

            if (orden == "asc")
            {
                consulta = consulta.OrderBy(u => u.Nombre);
            }
            else if (orden == "desc")
            {
                consulta = consulta.OrderByDescending(u => u.Nombre);
            }

            return await consulta.ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ActualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteUsuario(int id)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> ExisteRut(string rut)
        {
            return await _context.Usuarios.AnyAsync(u => u.Rut == rut);
        }
    }
}
