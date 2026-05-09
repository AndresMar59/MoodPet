using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPet.Domain.Entities
{
    public class Mascota
    {

        public int Id { get; set; }

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

        public Usuario Usuario { get; set; }

        public Guid UsuarioId { get; set; }

        public Raza Raza { get; set; }

        public int RazaId { get; set; }

    }
}
