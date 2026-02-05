using Schedule.Core.Model;
using Schedule.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Data.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context=context;
        }

        public List<Student> GetList()
        {
            return _context.studentsList.ToList();
        }
        public Student? GetById(int id)
        {
            return _context.studentsList.FirstOrDefault(x => x.Id == id);
        }
        public void AddStudent(Student student)
        {
            _context.studentsList.Add(student);
        }
        public void UpdateStudent(int id, Student student)
        {

            var updatedStudent = _context.studentsList.FirstOrDefault(s => s.Id == id);
            if (updatedStudent == null)
            {
                return; // returns HTTP 404 if the student isn't found
            }

            // Update the student's data
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Birthdate = updatedStudent.Birthdate;
            //student.StudentClass = updatedStudent.StudentClass;
        }
        public void DeleteStudent(int id)
        {
            var Student = _context.studentsList.FirstOrDefault(s => s.Id == id);
            if (Student != null)
            {
                _context.studentsList.Remove(Student);   // returns HTTP 404 if the student isn't found
            }

        }
    }
}
