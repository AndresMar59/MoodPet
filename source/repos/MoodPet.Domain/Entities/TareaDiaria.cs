using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MoodPet.Domain.Entities
{
    public class TareaDiaria

    {
        public int Id { get; set; } 

        public string Titulo { get; set; } 

        public string? Descripcion { get; set; } 

        public DateOnly Fecha { get; set; } // Fecha donde se asigno que se hara la tarea. 

        public DayOfWeek DayOfWeek { get; set; } // Dia de la semana que se quiere hacer la tarea.

        public bool Recurrente { get; set; } = false; // Si es una tarea recurrente o no (Si si lo es, se crea el historial tarea con la fecha igual a al fecha de la tarea)

        public bool estado { get; set; } = true; // Marcar por default true, hasta que se elimine manualmente. O hasat que la fecha final llegue

        public int? Semanas { get; set; } // Cantidad de semanas que quieres que se repita la tarea, si es recurrente.

        public TimeOnly Hora { get; set; } // Hora en la que se hace la tarea

        public int MascotaId { get; set; }

        public Mascota Mascota { get; set; } 
          
    }

    
}