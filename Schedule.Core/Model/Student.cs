namespace Schedule.Core.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public int? ClassId { get; set; }
        public Class? Class { get; set; }

        public Uniform? Uniform { get; set; }
    }
}
