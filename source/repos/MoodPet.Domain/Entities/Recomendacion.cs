using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoodPet.Domain.Entities;

namespace Moodpet.Domain.Entities
{
    public class Recomendacion:GeneralEntity

    {
        public string Tipo { get; set; }
        // Ejemplo: Alimentacion, Salud, Actividad, Higiene

        public string Descripcion { get; set; }

        public int? RazaId { get; set; }
        public Raza? Raza { get; set; }

        public int? EspecieId { get; set; }
        public Especie? Especie { get; set; }
    }
    
    
}
