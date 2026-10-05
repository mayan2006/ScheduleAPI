using System.ComponentModel.DataAnnotations;
using Schedule.Core.Validation;

namespace Schedule.Core.DTOs
{
    public class StudentUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DateNotInFuture]
        public DateTime Birthdate { get; set; }

        [Range(1, int.MaxValue)]
        public int? ClassId { get; set; }

        [MaxLength(30)]
        public string? UniformColor { get; set; }

        [Range(1, 60)]
        public int? UniformSize { get; set; }
    }
}
