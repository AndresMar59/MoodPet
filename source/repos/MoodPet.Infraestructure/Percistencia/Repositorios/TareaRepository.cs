using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;

namespace MoodPet.Infraestructure.Percistencia.Repositorios
{
    public class TareaRepository : ITareaRepository
    {
        public readonly ApplicationDbContext _context;
        public TareaRepository(ApplicationDbContext context) 
        { 
          _context = context;
        }
        public async Task<TareaDiaria> AddAsync(TareaDiaria entity)
        {
            await _context.Set<TareaDiaria>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if (result == 0 || entity.Id == 0)
            {
                return null;
            }
            return entity; ;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TareaDiaria>().FindAsync(id); // Ya trae validacion de quesi existe o no
            if (entity == null)
                return false;

            entity.estado = false;

            var result = await _context.SaveChangesAsync();
            return result >= 1; ;
        }

        public async Task<TareaDiaria> FindAsync(int id)
        {
            var entity = await _context.Set<TareaDiaria>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        } // Devuelve una tarea segun el id de esta, si no existe devuelve null

        public async Task<TareaDiaria> FindAsyncByMascota(int id)
        {
            var entity = await _context.Set<TareaDiaria>().FirstOrDefaultAsync(e => e.MascotaId == id);
            return entity;
        } // Devuelve una tarea segun el id de la mascota, si no existe devuelve null

        public async Task<List<TareaDiaria>> GetAllAsyncbyMascota(int MascotId)
        {
            var entity = await _context.Set<TareaDiaria>().Where(e => e.MascotaId == MascotId).ToListAsync();
            return entity;
        }
    }
}
