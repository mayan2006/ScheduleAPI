using Schedule.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Data.Repositories
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly DataContext _context;
        public IStudentRepository Students { get; }
        public RepositoryManager(DataContext context, IStudentRepository
        StudentRepository)
        {
            _context = context;
            Students = StudentRepository;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
