namespace MoodPetApi.DTOs.Mascota
{
    public class AddMascotaDto
    {
        public string nombre { get; set; }
        public float peso { get; set; }
        public string sexo { get; set; }

        public DateOnly fechaNacimiento { get; set; }
        public int razaId { get; set; }

        public string? userId { get; set; }


    }
}
