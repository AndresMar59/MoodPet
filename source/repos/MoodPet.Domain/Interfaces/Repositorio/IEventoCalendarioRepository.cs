using System.Collections.Generic;
using System.Threading.Tasks;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Domain.Interfaces.Repositorio
{
    public interface IEventoCalendarioRepository : IGeneralRepository<EventoCalendario>
    {
        // 1. Crear un evento
        Task<EventoCalendario> CreateAsync(EventoCalendario evento);

        // 2. Buscar por ID del Evento
        Task<EventoCalendario?> GetByIdAsync(int id);

        // 3. Buscar todos los eventos de una Mascota específica
        Task<IEnumerable<EventoCalendario>> GetByMascotaIdAsync(int mascotaId);

        // 4. Buscar todos los eventos de un Usuario específico
        Task<IEnumerable<EventoCalendario>> GetByUserIdAsync(string userId);


    }
}
