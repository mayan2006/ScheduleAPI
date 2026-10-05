using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.DTOs
{
    public class ClassCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
