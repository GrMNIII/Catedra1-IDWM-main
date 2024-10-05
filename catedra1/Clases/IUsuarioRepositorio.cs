using GestionUsuarios.Models;

namespace GestionUsuarios.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios(string orden, string genero);
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> ActualizarUsuario(Usuario usuario);
        Task EliminarUsuario(int id);
        Task<bool> ExisteUsuario(int id);
        Task<bool> ExisteRut(string rut);
    }
}
