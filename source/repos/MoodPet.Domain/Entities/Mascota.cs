using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class Mascota
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Edad { get; set; }

        public float Peso { get; set; }

        public string Sexo { get; set; } 

        public DateOnly FechaNacimiento { get; set; }

        //FK Usuario
        public string UserId { get; set; }

        //FK Raza
        public Raza Raza { get; set; }

        public int RazaId { get; set; }

        //Navegación
        public ICollection<Eventocalendario> Eventos { get; set; } = new List<Eventocalendario>();
        public ICollection<TareaDiaria> Tareas { get; set; } = new List<TareaDiaria>();
    }
}
