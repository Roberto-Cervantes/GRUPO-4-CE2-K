namespace GRUPO_4_CE2_K.Services
{
    public interface IFilesService
    {
        Task<string> SubirArchivo(Stream archivo, string nombre);
    }
}
