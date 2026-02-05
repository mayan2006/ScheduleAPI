using Microsoft.EntityFrameworkCore;
using Schedule.Core.Model;
using Schedule.Core.Repositories;
using Schedule.Core.Service;
using Schedule.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.Service
{
    public class StudentService:IStudentService
    {
        public readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public List<Student> GetAll()
        {

            return _studentRepository.GetList();
        }
        public Student? GetById(int id)
        {

            return _studentRepository.GetById(id);

        }
        public void AddStudent(Student student)
        {
            _studentRepository.GetList().Add(student);
        }
        public void UpdateStudent(int id, Student student)
        {

            var updatedStudent = _studentRepository.GetList().FirstOrDefault(s => s.Id == id);
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
            var Student = _studentRepository.GetList().FirstOrDefault(s => s.Id == id);
            if (Student != null)
            {
                _studentRepository.GetList().Remove(Student);   // returns HTTP 404 if the student isn't found
            }

        }

    }
}
