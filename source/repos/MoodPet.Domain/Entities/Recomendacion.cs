using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoodPet.Domain.Entities;

namespace Moodpet.Domain.Entities
{
    public class Recomendacion

    {  
        public int Id { get; set; }

        public int MascotaId { get; set; }

        public Mascota Mascota { get; set; }

        public string Tipo { get; set; }

        public string? Descripcion { get; set; }

        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
    }
    
    
}
