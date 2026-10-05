using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.DTOs
{
    public class UniformUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public string Color { get; set; } = string.Empty;

        [Range(1, 60)]
        public int Size { get; set; }
    }
}
