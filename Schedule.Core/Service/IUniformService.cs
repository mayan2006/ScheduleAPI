using Schedule.Core.DTOs;

namespace Schedule.Core.Service
{
    public interface IUniformService
    {
        Task<List<UniformDto>> GetAll();
        Task<UniformDto?> GetById(int id);
        Task<UniformDto?> AddUniform(UniformCreateDto dto);
        Task<UniformDto?> UpdateUniform(int id, UniformUpdateDto dto);
        Task<bool> DeleteUniform(int id);
    }
}
