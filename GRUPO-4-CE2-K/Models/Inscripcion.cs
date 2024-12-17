namespace GRUPO_4_CE2_K.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public string UsuarioId { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
