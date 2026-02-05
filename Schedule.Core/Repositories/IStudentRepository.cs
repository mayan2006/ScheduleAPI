using Schedule.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Core.Repositories
{
     public interface IStudentRepository
    {
        List<Student> GetList();
        public Student? GetById(int id);
        public void AddStudent(Student student);
        public void UpdateStudent(int id, Student student);
        public void DeleteStudent(int id);

    }
}
