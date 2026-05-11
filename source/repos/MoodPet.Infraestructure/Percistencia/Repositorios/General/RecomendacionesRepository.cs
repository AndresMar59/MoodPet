using Microsoft.EntityFrameworkCore;
using Moodpet.Domain.Entities;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public class RecomendacionesRepository : IRecomendacionRepository
    {
        private readonly ApplicationDbContext _context;

        public RecomendacionesRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Mascota?> GetMascotaConRazaYEspecie(int mascotaId)
        {
            return await _context.Mascotas
                .Include(m => m.Raza)
                    .ThenInclude(r => r.Especie)
                .FirstOrDefaultAsync(m => m.Id == mascotaId);
        }

        public async Task<List<Recomendacion>> GetPlantillasPorRaza(int razaId)
        {
            return await _context.Recomendaciones
                .Where(p => p.RazaId == razaId && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Recomendacion>> GetPlantillasPorEspecie(int especieId)
        {
            return await _context.Recomendaciones
                .Where(p => p.EspecieId == especieId && !p.IsDeleted && p.RazaId==null)
                .ToListAsync();
        }

        public async Task<HistorialRecomendaciones> CrearRecomendacion(HistorialRecomendaciones recomendacion)
        {
            await _context.HistorialRecomendaciones.AddAsync(recomendacion);
            await _context.SaveChangesAsync();

            return recomendacion;
        }

        public async Task<List<HistorialRecomendaciones>> GetHistorialPorMascota(int mascotaId)
        {
            return await _context.HistorialRecomendaciones
               .Where(r => r.MascotaId == mascotaId && !r.IsDeleted)
               .OrderByDescending(r => r.FechaGeneracion)
               .ToListAsync();
        }
    }
}
