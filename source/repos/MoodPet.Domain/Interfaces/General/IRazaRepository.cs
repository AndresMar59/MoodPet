using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;

namespace MoodPet.Domain.Interfaces.General
{
    public interface IRazaRepository : IGeneralRepository<Raza>
    {

        public Task<List<Raza>> GetRazasByEspecieIdAsync(int especieId);

    }
}
