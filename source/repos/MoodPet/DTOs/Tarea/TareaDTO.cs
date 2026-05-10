namespace MoodPetApi.DTOs.Tarea
{
    public class TareaDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateOnly Fecha { get; set; } // Fecha donde se asigno que se hara la tarea. 

        public bool Recurrente { get; set; } = false; // Si es una tarea recurrente o no (Si si lo es, se crea el historial tarea con la fecha igual a al fecha de la tarea)

        public int? Semanas { get; set; } = 1;

        public TimeOnly Hora { get; set; } // Hora en la que se hace la tarea

        public int MascotaId { get; set; }

    }
}
