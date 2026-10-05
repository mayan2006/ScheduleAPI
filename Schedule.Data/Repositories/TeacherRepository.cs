using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using Schedule.Core.Repositories;

namespace Schedule.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetList()
        {
            return await _context.teachers
                .Include(teacher => teacher.Classes)
                .ToListAsync();
        }

        public async Task<Teacher?> GetById(int id)
        {
            return await _context.teachers
                .Include(teacher => teacher.Classes)
                .FirstOrDefaultAsync(teacher => teacher.Id == id);
        }

        public void AddTeacher(Teacher teacher)
        {
            _context.teachers.Add(teacher);
        }

        public void DeleteTeacher(int id)
        {
            var teacher = _context.teachers.FirstOrDefault(t => t.Id == id);
            if (teacher != null)
                _context.teachers.Remove(teacher);
        }
    }
}
