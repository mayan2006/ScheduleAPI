using Microsoft.AspNetCore.Mvc;
using Schedule.Core.Model;
using Schedule.Core.Service;
using Schedule.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Schedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService=studentService;
        }

        // GET: api/<StudentController>
        //שליפה של כל התלמידות
        [HttpGet]
        public ActionResult Get()
        {
           var students= _studentService.GetAll();
            return Ok(students);
        }

        // GET api/<StudentController>/5
        //שליפה של תלמידה לפי id
        [HttpGet("{id}")]

        public ActionResult Get(int id)
        {
           var student = _studentService.GetById(id);
            return Ok(student);

        }


        ////// POST api/<StudentController>
        //////הוספה של תלמידה
        [HttpPost]
        public void Post([FromBody] Student newStudent)
        {
            _studentService.GetAll().Add(newStudent);
        }


        //////PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student updatedStudent)
        {
            var student = _studentService.GetAll().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return; // returns HTTP 404 if the student isn't found
            }

            // Update the student's data
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;
            student.Birthdate = updatedStudent.Birthdate;
            //student.StudentClass = updatedStudent.StudentClass;

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetAll().FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            _studentService.GetAll().Remove(student);
            return NoContent(); // 204
        }
    }
}
