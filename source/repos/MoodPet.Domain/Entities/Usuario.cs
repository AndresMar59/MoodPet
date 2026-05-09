using System.ComponentModel.DataAnnotations;
namespace MoodPet.Domain.Entities
{
    public class Usuario

    {
        public string name { get; set; }

        public string? lastname { get; set; }

        public string? fullName { get { return $"{name} {lastname}"; } }

        public Guid id { get; set; }

        public string? tel { get; set; }

        public string email { get; set; }

        public string? password { get; set; } // Es necesario asi para el mapping?

        //Navegación
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
        public ICollection<TareaDiaria> TareaDiarias { get; set; } = new List<TareaDiaria>();
        public ICollection<Eventocalendario> EventosCalendarios { get; set; } = new List<Eventocalendario>();


        //public string? role { get; set; }  creo que no es necesario el atributo role, ya que se maneja a través de Identity, pero lo dejo por si acaso
    }
}
