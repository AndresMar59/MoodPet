using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Interfaces.General;
using MoodPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public class EspecieRepository : GeneralRepository<Especie>, IEspecieRepository
    {
        public EspecieRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Especie> FindAsync(int speciesId)
        {
            return await _context.Especies
                .Include(e => e.Razas)      // Incluye las razas relacionadas
                .Where(e => !e.IsDeleted && e.Id == speciesId)
                .FirstOrDefaultAsync();
        }
    }
}

               