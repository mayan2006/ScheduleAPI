namespace Schedule.Core.Model
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; } = new();
        public List<Teacher> Teachers { get; set; } = new();
    }
}
