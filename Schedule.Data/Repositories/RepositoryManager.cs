using Schedule.Core.Repositories;

namespace Schedule.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;

        public IStudentRepository Students { get; }
        public IClassRepository Classes { get; }
        public ITeacherRepository Teachers { get; }
        public IUniformRepository Uniforms { get; }

        public RepositoryManager(
            DataContext context,
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            ITeacherRepository teacherRepository,
            IUniformRepository uniformRepository)
        {
            _context = context;
            Students = studentRepository;
            Classes = classRepository;
            Teachers = teacherRepository;
            Uniforms = uniformRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
