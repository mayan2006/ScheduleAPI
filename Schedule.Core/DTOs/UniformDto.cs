namespace Schedule.Core.DTOs
{
    public class UniformDto
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }

        public int StudentId { get; set; }
        public string? StudentFirstName { get; set; }
        public string? StudentLastName { get; set; }
    }
}
