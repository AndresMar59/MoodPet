namespace MoodPetApi.DTOs.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string? Lastname { get; set; }
        public string? Tel { get; set; }
        public string Email { get; set; } = null!;
    }
}