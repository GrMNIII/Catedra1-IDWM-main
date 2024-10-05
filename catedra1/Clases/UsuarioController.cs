using Microsoft.AspNetCore.Mvc;
using GestionUsuarios.Models;
using GestionUsuarios.Repositorios;

namespace GestionUsuarios.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Crear un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (await _usuarioRepositorio.ExisteRut(usuario.Rut))
            {
                return Conflict("El RUT ya existe.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre) || usuario.Nombre.Length < 3 || usuario.Nombre.Length > 100)
            {
                return BadRequest("El nombre debe tener entre 3 y 100 caracteres.");
            }

            if (!new[] { "masculino", "femenino", "otro", "prefiero no decirlo" }.Contains(usuario.Genero))
            {
                return BadRequest("El género no es válido.");
            }

            if (usuario.FechaNacimiento >= DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento debe ser anterior a la fecha actual.");
            }

            var usuarioCreado = await _usuarioRepositorio.CrearUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuarioCreado.Id }, usuarioCreado);
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosUsuarios([FromQuery] string orden, [FromQuery] string genero)
        {
            var usuarios = await _usuarioRepositorio.ObtenerTodosLosUsuarios(orden, genero);
            return Ok(usuarios);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(usuario);
        }

        // Actualizar un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuarioActualizado)
        {
            if (!await _usuarioRepositorio.ExisteUsuario(id))
            {
                return NotFound("Usuario no encontrado.");
            }

            if (await _usuarioRepositorio.ExisteRut(usuarioActualizado.Rut) && usuarioActualizado.Id != id)
            {
                return Conflict("El RUT ya existe.");
            }

            var usuario = await _usuarioRepositorio.ObtenerUsuarioPorId(id);
            usuario.Rut = usuarioActualizado.Rut;
            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.CorreoElectronico = usuarioActualizado.CorreoElectronico;
            usuario.Genero = usuarioActualizado.Genero;
            usuario.FechaNacimiento = usuarioActualizado.FechaNacimiento;

            await _usuarioRepositorio.ActualizarUsuario(usuario);
            return Ok(usuario);
        }

        // Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            if (!await _usuarioRepositorio.ExisteUsuario(id))
            {
                return NotFound("Usuario no encontrado.");
            }

            await _usuarioRepositorio.EliminarUsuario(id);
            return Ok("Usuario eliminado exitosamente.");
        }
    }
}
