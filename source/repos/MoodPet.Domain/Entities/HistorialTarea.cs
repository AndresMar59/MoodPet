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

        public DateOnly Fecha { get; set; } // Fecha debe coincidir con fecha de TareaDiaria

        public TimeOnly Hora { get; set; } // Hora de realización de la tarea

        public bool Completada { get; set; } = false;

        public bool Eliminada { get; set; } = false; // Marcar por default false, hasta que se elimine manualmente. O hasta que la fecha final llegue
    }

}