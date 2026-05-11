using System;
using System.Collections.Generic;
using System.Text;

namespace MoodPet.Domain.Entities
{
    public class HistorialRecomendaciones : GeneralEntity
    {
        public int MascotaId { get; set; }

        public Mascota Mascota { get; set; }

        public string Tipo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
    }
}
