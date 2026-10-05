namespace Schedule.Core.Model
{
    public class Uniform
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }

        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
