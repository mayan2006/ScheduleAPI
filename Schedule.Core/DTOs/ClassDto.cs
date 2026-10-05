namespace Schedule.Core.DTOs
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ClassStudentDto> Students { get; set; } = new();
        public List<ClassTeacherDto> Teachers { get; set; } = new();
    }

    public class ClassStudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ClassTeacherDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
    }
}
