using System.ComponentModel.DataAnnotations;

namespace Schedule.Core.DTOs
{
    public class TeacherUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; } = string.Empty;
    }
}
