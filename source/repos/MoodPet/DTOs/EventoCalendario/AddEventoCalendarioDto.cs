using static MoodPet.Domain.Entities.EventoCalendario;

namespace MoodPetApi.DTOs.EventoCalendario
{
    public class AddEventoCalendarioDto
    {
        public string titulo {  get; set; }

        public string? Descripcion { get; set; }

        public DateOnly FechaEvento { get; set; }

        public int TipoEventoId { get; set; }

        public int MascotaId { get; set; }

        public string? UserId { get; set; }


    }
}
