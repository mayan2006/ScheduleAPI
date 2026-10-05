using Schedule.Core.DTOs;

namespace Schedule.Core.Service
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAll();
        Task<PagedResult<StudentDto>> GetPaged(int page, int pageSize);
        Task<StudentDto?> GetById(int id);
        Task<StudentDto> AddStudent(StudentCreateDto student);
        Task<StudentDto?> UpdateStudent(int id, StudentUpdateDto student);
        Task<bool> DeleteStudent(int id);
    }
}
