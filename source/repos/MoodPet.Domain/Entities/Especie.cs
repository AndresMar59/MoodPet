using System.ComponentModel.DataAnnotations;


namespace MoodPet.Domain.Entities
{
     public class Especie : GeneralEntity
    {
        public string Nombre { get; set; } // Nombre de la especie (EL CRUD habra. Pero el llenado dependera de Herberth)
        public ICollection<Raza> Razas { get; set; }  // Relacion uno a muchos con Raza

    }
}
    
