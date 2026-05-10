using MoodPet.Domain.Entities;
using static MoodPet.Domain.Entities.EventoCalendario;

namespace MoodPetApi.DTOs.EventoCalendario
{
    public class UpdateEventoCalendarioDto
    {
        public string? Descripcion { get; set; }

        public DateOnly? FechaEvento { get; set; }  // Fecha en donde se espera ejecutar el evento

        public EstadoEvento? Estado { get; set; } = EstadoEvento.Pendiente;    //default Pendiente, se puede cambiar a Completado o Cancelado

        public int? TipoEventoId { get; set; }
    }
}
