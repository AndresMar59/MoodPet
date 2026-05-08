using System;
using System.Collections.Generic;
using System.Text;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.General;

namespace MoodPet.Infraestructure.Percistencia.Repositorios.General
{
    public class TipoEventoRepository : GeneralRepository<TipoEvento>, ITipoEventoRepository
    {
        public TipoEventoRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
