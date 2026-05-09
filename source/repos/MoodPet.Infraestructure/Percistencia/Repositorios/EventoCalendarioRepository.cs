using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using MoodPet.Infraestructure.Percistencia;
using MoodPet.Infraestructure.Percistencia.Repositorios.General;

public class EventoCalendarioRepository : GeneralRepository<Eventocalendario>, IEventoCalendarioRepository
{
    public EventoCalendarioRepository(ApplicationDbContext context)
        :base(context)
    {
    }

    public async Task<Eventocalendario> CreateAsync(Eventocalendario evento)
    {
        await _context.EventosCalendario.AddAsync(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task<Eventocalendario?> GetByIdAsync(int id)
    {
        return await _context.EventosCalendario
          //  .Include(e => e.Mascota)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Eventocalendario>> GetByMascotaIdAsync(int mascotaId)
    {
        return await _context.EventosCalendario
            .Where(e => e.MascotaId == mascotaId)
            .OrderBy(e => e.FechaEvento) 
            .ToListAsync();
    }
}