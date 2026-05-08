using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class Eventocalendario
    {
        public int Id { get; set; } // Id

        public string titulo { get; set; } 

        public string? Descripcion { get; set; } 

        public DateOnly FechaEvento { get; set; }  // Fecha en donde se espera ejecutar el evento

        public int TipoEventoId { get; set; } 
        public TipoEvento TipoEvento { get; set; } // Relacion con Tipo de evento (Lleva CRUD, pero esto lo delimitara y llenara Ricardo)
        public int MascotaId { get; set; } 
        public Mascota mascota { get; set; } // Relacion con Mascotas (Lleva CRUD, esto lo hare yo)


    }
}
