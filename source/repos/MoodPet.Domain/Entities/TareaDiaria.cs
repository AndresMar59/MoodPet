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

        public bool Recurrente {
            get => _EsRecurrente;
            set
            {
               _EsRecurrente = value;
                _semanas = _EsRecurrente ? 12 : 1;
            }
        }

        public bool estado { get; set; } = true; // Marcar por default true, hasta que se elimine manualmente. O hasta que la fecha final llegue

        public int? Semanas {get => _semanas; set => _semanas = value; }
        public TimeOnly Hora { get; set; } // Hora en la que se hace la tarea

        //FK Mascota
        public int MascotaId { get; set; }

        public Mascota Mascota { get; set; }



        private bool _EsRecurrente = false;
        private int? _semanas; 

    }

    
}