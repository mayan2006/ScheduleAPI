namespace Schedule.Core.DTOs
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }

        public List<TeacherClassDto> Classes { get; set; } = new();
    }

    public class TeacherClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
