using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using Schedule.Core.Repositories;

namespace Schedule.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetList()
        {
            return await _context.studentsList
                .Include(student => student.Class)
                .Include(student => student.Uniform)
                .ToListAsync();
        }

        public async Task<(List<Student> Items, int TotalCount)> GetPaged(int page, int pageSize)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 1)
                pageSize = 10;

            var totalCount = await _context.studentsList.CountAsync();

            var items = await _context.studentsList
                .Include(student => student.Class)
                .Include(student => student.Uniform)
                .OrderBy(student => student.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Student?> GetById(int id)
        {
            return await _context.studentsList
                .Include(student => student.Class)
                .Include(student => student.Uniform)
                .FirstOrDefaultAsync(student => student.Id == id);
        }

        public void AddStudent(Student student)
        {
            _context.studentsList.Add(student);
        }

        public void DeleteStudent(int id)
        {
            var student = _context.studentsList.FirstOrDefault(s => s.Id == id);
            if (student != null)
                _context.studentsList.Remove(student);
        }
    }
}
