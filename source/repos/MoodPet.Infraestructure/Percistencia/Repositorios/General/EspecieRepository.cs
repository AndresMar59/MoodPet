using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Interfaces.General;
using MoodPet.Domain.Entities;


namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public class EspecieRepository : GeneralRepository<Especie>, IEspecieRepository
    {
        public EspecieRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
