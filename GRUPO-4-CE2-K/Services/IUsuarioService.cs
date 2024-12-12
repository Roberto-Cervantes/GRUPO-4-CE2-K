using GRUPO_4_CE2_K.Models;

namespace GRUPO_4_CE2_K.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario usuario);
    }
}
