using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public class RazaRepository : GeneralRepository<Raza>, IRazaRepository
    {
        public RazaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Raza>> GetRazasByEspecieIdAsync(int especieId)
        {
            return _context.Razas.Where(r => r.EspecieId == especieId).ToListAsync();
        }

    }
}
