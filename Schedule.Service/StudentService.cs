using Schedule.Core.DTOs;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Core.Service;

namespace Schedule.Service
{
    public class StudentService : IStudentService
    {
        private readonly IRepositoryManager _manager;

        public StudentService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<StudentDto>> GetAll()
        {
            var students = await _manager.Students.GetList();
            return students.Select(MapToDto).ToList();
        }

        public async Task<PagedResult<StudentDto>> GetPaged(int page, int pageSize)
        {
            var (items, totalCount) = await _manager.Students.GetPaged(page, pageSize);

            return new PagedResult<StudentDto>
            {
                Items = items.Select(MapToDto).ToList(),
                Page = page < 1 ? 1 : page,
                PageSize = pageSize < 1 ? 10 : pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<StudentDto?> GetById(int id)
        {
            var student = await _manager.Students.GetById(id);
            return student == null ? null : MapToDto(student);
        }

        public async Task<StudentDto> AddStudent(StudentCreateDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birthdate = dto.Birthdate,
                ClassId = NormalizeClassId(dto.ClassId)
            };

            ApplyUniform(student, dto.UniformColor, dto.UniformSize);
            _manager.Students.AddStudent(student);
            await _manager.SaveAsync();

            return MapToDto(student);
        }

        public async Task<StudentDto?> UpdateStudent(int id, StudentUpdateDto dto)
        {
            var existing = await _manager.Students.GetById(id);
            if (existing == null)
                return null;

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Birthdate = dto.Birthdate;
            existing.ClassId = NormalizeClassId(dto.ClassId);
            ApplyUniform(existing, dto.UniformColor, dto.UniformSize);
            await _manager.SaveAsync();

            return MapToDto(existing);
        }

        public async Task<bool> DeleteStudent(int id)
        {
            if (await _manager.Students.GetById(id) == null)
                return false;

            _manager.Students.DeleteStudent(id);
            await _manager.SaveAsync();
            return true;
        }

        private static int? NormalizeClassId(int? classId)
        {
            return classId is null or 0 ? null : classId;
        }

        private static void ApplyUniform(Student student, string? color, int? size)
        {
            if (string.IsNullOrWhiteSpace(color) && !size.HasValue)
                return;

            if (student.Uniform == null)
            {
                student.Uniform = new Uniform
                {
                    Color = color ?? string.Empty,
                    Size = size ?? 0
                };
                return;
            }

            if (!string.IsNullOrWhiteSpace(color))
                student.Uniform.Color = color;
            if (size.HasValue)
                student.Uniform.Size = size.Value;
        }

        private static StudentDto MapToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Birthdate = student.Birthdate,
                ClassId = student.ClassId,
                ClassName = student.Class?.Name,
                UniformSize = student.Uniform?.Size,
                UniformColor = student.Uniform?.Color
            };
        }
    }
}
