using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class Mascota : GeneralEntity
    {

        public string Nombre { get; set; }

        public int? Edad
        {
            get
            {
                DateOnly hoy = DateOnly.FromDateTime(DateTime.Now);
                int edad = hoy.Year - FechaNacimiento.Year;

                if (FechaNacimiento > hoy.AddYears(-edad))
                {
                    edad--;
                }

                return edad;
            }
        }

        public float Peso { get; set; }

        public string Sexo { get; set; } 

        public DateOnly FechaNacimiento { get; set; }  

        //FK Usuario
        public string UserId { get; set; }

        //FK Raza
        public Raza Raza { get; set; }

        public int RazaId { get; set; }

        //Navegación
        public ICollection<EventoCalendario> Eventos { get; set; } = new List<EventoCalendario>();
        public ICollection<TareaDiaria> Tareas { get; set; } = new List<TareaDiaria>();
    }
}
