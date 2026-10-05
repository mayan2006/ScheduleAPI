using Schedule.Core.DTOs;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Core.Service;

namespace Schedule.Service
{
    public class UniformService : IUniformService
    {
        private readonly IRepositoryManager _manager;

        public UniformService(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<UniformDto>> GetAll()
        {
            var uniforms = await _manager.Uniforms.GetList();
            return uniforms.Select(MapToDto).ToList();
        }

        public async Task<UniformDto?> GetById(int id)
        {
            var uniform = await _manager.Uniforms.GetById(id);
            return uniform == null ? null : MapToDto(uniform);
        }

        public async Task<UniformDto?> AddUniform(UniformCreateDto dto)
        {
            var student = await _manager.Students.GetById(dto.StudentId);
            if (student == null || student.Uniform != null)
                return null;

            var uniform = new Uniform
            {
                Color = dto.Color,
                Size = dto.Size,
                StudentId = dto.StudentId
            };

            _manager.Uniforms.AddUniform(uniform);
            await _manager.SaveAsync();

            uniform.Student = student;
            return MapToDto(uniform);
        }

        public async Task<UniformDto?> UpdateUniform(int id, UniformUpdateDto dto)
        {
            var uniform = await _manager.Uniforms.GetById(id);
            if (uniform == null)
                return null;

            uniform.Color = dto.Color;
            uniform.Size = dto.Size;
            await _manager.SaveAsync();
            return MapToDto(uniform);
        }

        public async Task<bool> DeleteUniform(int id)
        {
            if (await _manager.Uniforms.GetById(id) == null)
                return false;

            _manager.Uniforms.DeleteUniform(id);
            await _manager.SaveAsync();
            return true;
        }

        private static UniformDto MapToDto(Uniform uniform)
        {
            return new UniformDto
            {
                Id = uniform.Id,
                Color = uniform.Color,
                Size = uniform.Size,
                StudentId = uniform.StudentId,
                StudentFirstName = uniform.Student?.FirstName,
                StudentLastName = uniform.Student?.LastName
            };
        }
    }
}
