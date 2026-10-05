using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using Schedule.Core.Repositories;

namespace Schedule.Data.Repositories
{
    public class UniformRepository : IUniformRepository
    {
        private readonly DataContext _context;

        public UniformRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Uniform>> GetList()
        {
            return await _context.uniforms
                .Include(uniform => uniform.Student)
                .ToListAsync();
        }

        public async Task<Uniform?> GetById(int id)
        {
            return await _context.uniforms
                .Include(uniform => uniform.Student)
                .FirstOrDefaultAsync(uniform => uniform.Id == id);
        }

        public void AddUniform(Uniform uniform)
        {
            _context.uniforms.Add(uniform);
        }

        public void DeleteUniform(int id)
        {
            var uniform = _context.uniforms.FirstOrDefault(u => u.Id == id);
            if (uniform != null)
                _context.uniforms.Remove(uniform);
        }
    }
}
