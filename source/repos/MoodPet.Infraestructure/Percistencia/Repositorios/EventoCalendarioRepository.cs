using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using MoodPet.Infraestructure.Percistencia;
using MoodPet.Infraestructure.Percistencia.Repositorios.General;

public class EventoCalendarioRepository : GeneralRepository<EventoCalendario>, IEventoCalendarioRepository
{
    public EventoCalendarioRepository(ApplicationDbContext context)
        :base(context)
    {
    }

    public async Task<EventoCalendario> CreateAsync(EventoCalendario evento)
    {
        await _context.EventosCalendario.AddAsync(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task<EventoCalendario?> GetByIdAsync(int id)
    {
        return await _context.EventosCalendario
            .Include(e => e.Mascota)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<EventoCalendario>> GetByMascotaIdAsync(int mascotaId)
    {
        return await _context.EventosCalendario
            .Where(e => e.MascotaId == mascotaId)
            .OrderBy(e => e.FechaEvento) 
            .ToListAsync();
    }

    public async Task<IEnumerable<EventoCalendario>> GetByUserIdAsync(string userId)
    {
        return await _context.EventosCalendario
            .Where(e => e.UserId == userId)
            .OrderBy(e => e.FechaEvento)
            .ToListAsync();
    }
}