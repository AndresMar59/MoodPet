using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;

namespace MoodPet.Infraestructure.Percistencia.Repositorios
{
    public class HistorialTareaRepository : IHistorialRepository
    {
        public readonly ApplicationDbContext _context;
        public HistorialTareaRepository(ApplicationDbContext context) 
        {
            _context = context;
          
        }
        public async Task<HistorialTarea> AddAsync(HistorialTarea entity)
        {
            await _context.Set<HistorialTarea>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if (result == 0 || entity.Id == 0)
            {
                return null;
            }
            return entity; ;
        }

        public async Task<bool> Complete(int id)
        {
            var entity = await _context.Set<HistorialTarea>().FindAsync(id); // Ya trae validacion de quesi existe o no
            if (entity == null)
                return false;

            entity.Completada = true;

            var result = await _context.SaveChangesAsync();
            return result >= 1;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<HistorialTarea>().FindAsync(id); // Ya trae validacion de quesi existe o no
            if (entity == null)
                return false;

            entity.Completada = false;

            var result = await _context.SaveChangesAsync();
            return result >= 1; 
        }

        public async Task<HistorialTarea> FindAsync(int id)
        {
            var entity = await _context.Set<HistorialTarea>().FirstOrDefaultAsync(e => e.Id == id); 
            return entity;
        }

        public async Task<HistorialTarea> FindAsyncByTarea(int id)
        {
           var entity = await _context.Set<HistorialTarea>().FirstOrDefaultAsync(e => e.TareaId == id); 
           return entity;
        }

        public async Task<List<HistorialTarea>> GetAllAsyncbyTarea(int TareaId)
        {
            var entities = await _context.Set<HistorialTarea>().Where(e => e.TareaId == TareaId).ToListAsync();
            return entities;
        }
    }
}
