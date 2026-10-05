namespace Schedule.Core.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public int? ClassId { get; set; }
        public string? ClassName { get; set; }

        public string? UniformColor { get; set; }
        public int? UniformSize { get; set; }
    }
}
