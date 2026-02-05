using Schedule.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Core.Service
{
    public interface IStudentService
    {
        public List<Student> GetAll();
        public Student? GetById(int id);
        public void AddStudent(Student student);

        public void UpdateStudent(int id, Student student);
        public void DeleteStudent(int id);

    }
}
