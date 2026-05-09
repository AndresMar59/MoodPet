using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class Eventocalendario : GeneralEntity
    {
        public string titulo { get; set; } 

        public string? Descripcion { get; set; } 

        public DateOnly FechaEvento { get; set; }  // Fecha en donde se espera ejecutar el evento

        public EstadoEvento Estado { get; set; } = EstadoEvento.Pendiente;    //default Pendiente, se puede cambiar a Completado o Cancelado

        //Fk TipoEvento
        public int TipoEventoId { get; set; } 
        public TipoEvento TipoEvento { get; set; } // Relacion con Tipo de evento (Lleva CRUD)
        
        //Fk Mascota
        public int MascotaId { get; set; } 
        public Mascota mascota { get; set; } // Relacion con Mascotas (Lleva CRUD)

        //Fk Usuario
        public string UserId { get; set; }

        public enum EstadoEvento
        {
            Pendiente,
            Completado,
            Cancelado
        }
    }
}
