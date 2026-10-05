using Schedule.Core.Model;

namespace Schedule.Core.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetList();
        Task<Class?> GetById(int id);
        void AddClass(Class schoolClass);
        void DeleteClass(int id);
    }
}
