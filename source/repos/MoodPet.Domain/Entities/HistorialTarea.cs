using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class HistorialTarea
    {
        public int Id { get; set; } 

        public int TareaId { get; set; }

        public TareaDiaria TareaDiaria { get; set; } // Relacion con tarea diaria 

        public DateTime Fecha { get; set; } // Fecha debe coincidir con fecha de TareaDiaria

        public bool Completada { get; set; } = false;


    }

}