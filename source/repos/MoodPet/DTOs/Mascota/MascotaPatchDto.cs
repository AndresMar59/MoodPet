namespace MoodPetApi.DTOs.Mascota
{
    public class MascotaPatchDto
    {
        public string? Nombre { get; set; }

        public float? Peso { get; set; }

        public string? Sexo { get; set; }

        public DateOnly? FechaNacimiento { get; set; } 

        public int? RazaId { get; set; }
    }
}
