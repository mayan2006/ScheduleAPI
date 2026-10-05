using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using Schedule.Core.Repositories;

namespace Schedule.Data.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetList()
        {
            return await _context.classes
                .Include(schoolClass => schoolClass.Students)
                .Include(schoolClass => schoolClass.Teachers)
                .ToListAsync();
        }

        public async Task<Class?> GetById(int id)
        {
            return await _context.classes
                .Include(schoolClass => schoolClass.Students)
                .Include(schoolClass => schoolClass.Teachers)
                .FirstOrDefaultAsync(schoolClass => schoolClass.Id == id);
        }

        public void AddClass(Class schoolClass)
        {
            _context.classes.Add(schoolClass);
        }

        public void DeleteClass(int id)
        {
            var schoolClass = _context.classes.FirstOrDefault(c => c.Id == id);
            if (schoolClass != null)
                _context.classes.Remove(schoolClass);
        }
    }
}
