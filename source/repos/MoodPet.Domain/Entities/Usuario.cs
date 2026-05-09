using System.ComponentModel.DataAnnotations;
namespace MoodPet.Domain.Entities
{
    public class Usuario

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName => $"{FirstName} {LastName}";


        //Navegación
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
        public ICollection<TareaDiaria> TareaDiarias { get; set; } = new List<TareaDiaria>();
        public ICollection<EventoCalendario> Eventos{ get; set; } = new List<EventoCalendario>();

    }
}
