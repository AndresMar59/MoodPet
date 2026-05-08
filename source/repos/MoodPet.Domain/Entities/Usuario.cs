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

        public string? role { get; set; }
    }
}
