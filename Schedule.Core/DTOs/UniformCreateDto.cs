using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.DTOs
{
    public class UniformCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Color { get; set; } = string.Empty;

        [Range(1, 60)]
        public int Size { get; set; }

        [Range(1, int.MaxValue)]
        public int StudentId { get; set; }
    }
}
