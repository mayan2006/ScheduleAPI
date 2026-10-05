using Schedule.Core.Model;

namespace Schedule.Core.Repositories
{
    public interface IUniformRepository
    {
        Task<List<Uniform>> GetList();
        Task<Uniform?> GetById(int id);
        void AddUniform(Uniform uniform);
        void DeleteUniform(int id);
    }
}
