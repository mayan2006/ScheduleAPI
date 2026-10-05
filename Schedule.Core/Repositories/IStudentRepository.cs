using Schedule.Core.Model;

namespace Schedule.Core.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetList();
        Task<(List<Student> Items, int TotalCount)> GetPaged(int page, int pageSize);
        Task<Student?> GetById(int id);
        void AddStudent(Student student);
        void DeleteStudent(int id);
    }
}
