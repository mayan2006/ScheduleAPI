using Schedule.Core.Model;

namespace Schedule.Core.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetList();
        Task<Teacher?> GetById(int id);
        void AddTeacher(Teacher teacher);
        void DeleteTeacher(int id);
    }
}
