using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.DTOs
{
    public class ClassUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
