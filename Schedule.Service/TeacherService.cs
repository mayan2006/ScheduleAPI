using Schedule.Core.DTOs;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Core.Service;

namespace Schedule.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepositoryManager _manager;

        public TeacherService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<TeacherDto>> GetAll()
        {
            var teachers = await _manager.Teachers.GetList();
            return teachers.Select(MapToDto).ToList();
        }

        public async Task<TeacherDto?> GetById(int id)
        {
            var teacher = await _manager.Teachers.GetById(id);
            return teacher == null ? null : MapToDto(teacher);
        }

        public async Task<TeacherDto> AddTeacher(TeacherCreateDto dto)
        {
            var teacher = new Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Subject = dto.Subject
            };

            _manager.Teachers.AddTeacher(teacher);
            await _manager.SaveAsync();
            return MapToDto(teacher);
        }

        public async Task<TeacherDto?> UpdateTeacher(int id, TeacherUpdateDto dto)
        {
            var teacher = await _manager.Teachers.GetById(id);
            if (teacher == null)
                return null;

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Subject = dto.Subject;
            await _manager.SaveAsync();
            return MapToDto(teacher);
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            if (await _manager.Teachers.GetById(id) == null)
                return false;

            _manager.Teachers.DeleteTeacher(id);
            await _manager.SaveAsync();
            return true;
        }

        private static TeacherDto MapToDto(Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Subject = teacher.Subject,
                Classes = teacher.Classes.Select(schoolClass => new TeacherClassDto
                {
                    Id = schoolClass.Id,
                    Name = schoolClass.Name
                }).ToList()
            };
        }
    }
}
