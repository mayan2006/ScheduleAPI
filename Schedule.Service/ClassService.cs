using Schedule.Core.DTOs;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Core.Service;

namespace Schedule.Service
{
    public class ClassService : IClassService
    {
        private readonly IRepositoryManager _manager;

        public ClassService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<ClassDto>> GetAll()
        {
            var classes = await _manager.Classes.GetList();
            return classes.Select(MapToDto).ToList();
        }

        public async Task<ClassDto?> GetById(int id)
        {
            var schoolClass = await _manager.Classes.GetById(id);
            return schoolClass == null ? null : MapToDto(schoolClass);
        }

        public async Task<ClassDto> AddClass(ClassCreateDto dto)
        {
            var schoolClass = new Class { Name = dto.Name };
            _manager.Classes.AddClass(schoolClass);
            await _manager.SaveAsync();
            return MapToDto(schoolClass);
        }

        public async Task<ClassDto?> UpdateClass(int id, ClassUpdateDto dto)
        {
            var schoolClass = await _manager.Classes.GetById(id);
            if (schoolClass == null)
                return null;

            schoolClass.Name = dto.Name;
            await _manager.SaveAsync();
            return MapToDto(schoolClass);
        }

        public async Task<bool> DeleteClass(int id)
        {
            if (await _manager.Classes.GetById(id) == null)
                return false;

            _manager.Classes.DeleteClass(id);
            await _manager.SaveAsync();
            return true;
        }

        public async Task<ClassDto?> AssignTeacher(int classId, int teacherId)
        {
            var schoolClass = await _manager.Classes.GetById(classId);
            var teacher = await _manager.Teachers.GetById(teacherId);
            if (schoolClass == null || teacher == null)
                return null;

            if (!schoolClass.Teachers.Any(t => t.Id == teacherId))
                schoolClass.Teachers.Add(teacher);

            await _manager.SaveAsync();
            return MapToDto(schoolClass);
        }

        public async Task<ClassDto?> RemoveTeacher(int classId, int teacherId)
        {
            var schoolClass = await _manager.Classes.GetById(classId);
            if (schoolClass == null)
                return null;

            var teacher = schoolClass.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
                return null;

            schoolClass.Teachers.Remove(teacher);
            await _manager.SaveAsync();
            return MapToDto(schoolClass);
        }

        private static ClassDto MapToDto(Class schoolClass)
        {
            return new ClassDto
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Students = schoolClass.Students.Select(student => new ClassStudentDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                }).ToList(),
                Teachers = schoolClass.Teachers.Select(teacher => new ClassTeacherDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Subject = teacher.Subject
                }).ToList()
            };
        }
    }
}
