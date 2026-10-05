using Schedule.Core.DTOs;

namespace Schedule.Core.Service
{
    public interface ITeacherService
    {
        Task<List<TeacherDto>> GetAll();
        Task<TeacherDto?> GetById(int id);
        Task<TeacherDto> AddTeacher(TeacherCreateDto dto);
        Task<TeacherDto?> UpdateTeacher(int id, TeacherUpdateDto dto);
        Task<bool> DeleteTeacher(int id);
    }
}
