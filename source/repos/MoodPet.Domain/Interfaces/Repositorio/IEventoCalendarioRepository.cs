using System.Collections.Generic;
using System.Threading.Tasks;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces.Repositorio
{


    public interface IEventoCalendarioRepository
    {
        // 1. Crear un evento
        Task<Eventocalendario> CreateAsync(Eventocalendario evento);

        // 2. Buscar por ID del Evento
        Task<Eventocalendario?> GetByIdAsync(int id);

        // 3. Buscar todos los eventos de una Mascota específica
        Task<IEnumerable<Eventocalendario>> GetByMascotaIdAsync(int mascotaId);
    }
}
