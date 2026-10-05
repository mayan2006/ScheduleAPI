using Schedule.Core.DTOs;

namespace Schedule.Core.Service
{
    public interface IClassService
    {
        Task<List<ClassDto>> GetAll();
        Task<ClassDto?> GetById(int id);
        Task<ClassDto> AddClass(ClassCreateDto dto);
        Task<ClassDto?> UpdateClass(int id, ClassUpdateDto dto);
        Task<bool> DeleteClass(int id);
        Task<ClassDto?> AssignTeacher(int classId, int teacherId);
        Task<ClassDto?> RemoveTeacher(int classId, int teacherId);
    }
}
