using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;
using MoodPet.Domain.Interfaces.Repositorio;

namespace MoodPet.Application.Service
{
    public class EventoCalendarioService
    {
        private readonly IEventoCalendarioRepository _eventoCalendarioRepository;
        private readonly IMascotaRepository _mascotaRepository;
        private readonly ITipoEventoRepository _tipoEventoRepository;

        public EventoCalendarioService(IEventoCalendarioRepository eventoCalendarioRepository, IMascotaRepository mascotaRepository, ITipoEventoRepository tipoEventoRepository)
        {
            _eventoCalendarioRepository = eventoCalendarioRepository;
            _mascotaRepository = mascotaRepository;
            _tipoEventoRepository = tipoEventoRepository;
        }

        //Crea un nuevo evento de calendario para una mascota específica. Verifica propiedades de crear el evento.
        public async Task<string> CreateEvento(EventoCalendario evento)
        {
            var mascota = await _mascotaRepository.FindAsync(evento.MascotaId);

            // Validaciones
            if (mascota == null)
            {
                throw new ArgumentException($"No se encontró la mascota con el id {evento.MascotaId}");
            }

            if (mascota.UserId != evento.UserId)
            {
                throw new ArgumentException("El usuario no es el dueño de la mascota");
            }

            var tipo = await _tipoEventoRepository.FindAsync(evento.TipoEventoId);
            if (tipo == null)
            {
                throw new ArgumentException($"No se encontró el tipo de evento con el id {evento.TipoEventoId}");
            }

            var existingEvento = await _eventoCalendarioRepository.GetByIdAsync(evento.Id);
            if (existingEvento != null)
            {
                throw new ArgumentException($"Ya existe un evento con el id {evento.Id}");
            }

            if (evento.FechaEvento < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha del evento no puede ser pasada");
            }

            if (string.IsNullOrWhiteSpace(evento.titulo))
            {
                throw new ArgumentException("El nombre del evento no puede estar vacío");
            }

            //Creando el evento
            var createdEvento = await _eventoCalendarioRepository.CreateEventoAsync(evento);

            return $"Creación del evento con id {createdEvento.Id} completada";
        }

        //Obtiene un evento de calendario por su id. Verifica que el evento exista antes de devolverlo.
        public async Task<EventoCalendario> GetByID(int id)
        {
            var evento = await _eventoCalendarioRepository.GetByIdAsync(id);

            if (evento == null)
            {
                throw new ArgumentException($"No se encontró el evento con el id {id}");
            }

            return evento;

        }

        //Elimina un evento de calendario por su id. Verifica que el evento exista antes de eliminarlo. Cambia su estado a cancelado antes de eliminar.
        public async Task<string> DeleteEventoById(int id)
        {
            var evento = await _eventoCalendarioRepository.GetByIdAsync(id);
            if (evento == null)
            {
                throw new ArgumentException($"No se encontró el evento con el id {id}");
            }

            evento.Estado = EventoCalendario.EstadoEvento.Cancelado;

            var deleted = await _eventoCalendarioRepository.Delete(id);

            if (!deleted)
            {
                throw new ArgumentException($"Error al eliminar el evento con el id {id}");
            }
            return $"Eliminación del evento con id {id} completada";
        }

        //Obtiene todos los eventos de calendario asociados a una mascota específica. Verifica que la mascota exista antes de obtener los eventos.
        public async Task<IEnumerable<EventoCalendario>> GetEventosByMascotaId(int mascotaId)
        {

            return await _eventoCalendarioRepository.GetByMascotaIdAsync(mascotaId);

        }

        //Obtiene todos los eventos de calendario asociados a un usuario específico. Verifica que el usuario exista antes de obtener los eventos.
        public async Task<IEnumerable<EventoCalendario>> GetEventosByUserId(string userId)
        {
            return await _eventoCalendarioRepository.GetByUserIdAsync(userId);
        }

        //Actualiza un evento de calendario existente.Cambia el estado del evento a completado si la fecha del evento es anterior a la fecha actual.
        public async Task<string> UpdateEvento(EventoCalendario evento)
        {
            var existingEvento = await _eventoCalendarioRepository.GetByIdAsync(evento.Id);
            if (existingEvento == null)
            {
                throw new ArgumentException($"No se encontró el evento con el id {evento.Id}");
            }

            existingEvento.Descripcion = evento.Descripcion;
            if (evento.FechaEvento < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha del evento no puede ser en el pasado");
            }
            existingEvento.FechaEvento = evento.FechaEvento;
            existingEvento.TipoEventoId = evento.TipoEventoId;
            if (evento.FechaEvento > DateOnly.FromDateTime(DateTime.Now) && evento.Estado == EventoCalendario.EstadoEvento.Completado)
            {
                throw new ArgumentException("No se puede marcar este evento como completado todavía");
            }
            existingEvento.Estado = evento.Estado;

            await _eventoCalendarioRepository.UpdateAsync(existingEvento);

            return $"Actualización del evento con id {evento.Id} completada";
        }

        //Marca un evento de calendario como completado. Verifica que la fecha del evento sea anterior a la fecha actual.
        public async Task<string> MarkEventoAsCompleted(int id)
        {
            var evento = await _eventoCalendarioRepository.GetByIdAsync(id);
            if (evento == null)
            {
                throw new ArgumentException($"No se encontró el evento con el id {id}");
            }

            if (evento.FechaEvento > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("No se puede marcar este evento como completado todavía");
            }

            evento.Estado = EventoCalendario.EstadoEvento.Completado;

            await _eventoCalendarioRepository.UpdateAsync(evento);

            return $"El evento con id {id} ha sido marcado como completado";
        }

        public async Task<string> MarkEventoAsCancelled(int id)
        {
            var evento = await _eventoCalendarioRepository.GetByIdAsync(id);

            if (evento == null)
            {
                throw new ArgumentException($"No se encontró el evento con el id {id}");
            }
            evento.Estado = EventoCalendario.EstadoEvento.Cancelado;

            await _eventoCalendarioRepository.Delete(evento.Id);

            return $"El evento con id {id} ha sido marcado como cancelado";
        }
    }
}
