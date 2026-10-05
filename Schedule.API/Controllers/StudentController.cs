using Microsoft.AspNetCore.Mvc;
using Schedule.Core.DTOs;
using Schedule.Core.Service;

namespace Schedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            if (page is null && pageSize is null)
                return Ok(await _studentService.GetAll());

            return Ok(await _studentService.GetPaged(page ?? 1, pageSize ?? 10));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> Post([FromBody] StudentCreateDto newStudent)
        {
            var created = await _studentService.AddStudent(newStudent);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Put(int id, [FromBody] StudentUpdateDto updatedStudent)
        {
            var student = await _studentService.UpdateStudent(id, updatedStudent);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _studentService.DeleteStudent(id))
                return NotFound();

            return NoContent();
        }
    }
}
