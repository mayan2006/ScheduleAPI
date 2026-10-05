using Microsoft.AspNetCore.Mvc;
using Schedule.Core.DTOs;
using Schedule.Core.Service;

namespace Schedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClassDto>>> Get()
        {
            return Ok(await _classService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> Get(int id)
        {
            var schoolClass = await _classService.GetById(id);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass);
        }

        [HttpGet("{id}/students")]
        public async Task<ActionResult<List<ClassStudentDto>>> GetStudents(int id)
        {
            var schoolClass = await _classService.GetById(id);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass.Students);
        }

        [HttpGet("{id}/teachers")]
        public async Task<ActionResult<List<ClassTeacherDto>>> GetTeachers(int id)
        {
            var schoolClass = await _classService.GetById(id);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass.Teachers);
        }

        [HttpPost("{id}/teachers/{teacherId}")]
        public async Task<ActionResult<ClassDto>> AssignTeacher(int id, int teacherId)
        {
            var schoolClass = await _classService.AssignTeacher(id, teacherId);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass);
        }

        [HttpDelete("{id}/teachers/{teacherId}")]
        public async Task<ActionResult<ClassDto>> RemoveTeacher(int id, int teacherId)
        {
            var schoolClass = await _classService.RemoveTeacher(id, teacherId);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass);
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> Post([FromBody] ClassCreateDto newClass)
        {
            var created = await _classService.AddClass(newClass);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClassDto>> Put(int id, [FromBody] ClassUpdateDto updatedClass)
        {
            var schoolClass = await _classService.UpdateClass(id, updatedClass);
            if (schoolClass == null)
                return NotFound();

            return Ok(schoolClass);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _classService.DeleteClass(id))
                return NotFound();

            return NoContent();
        }
    }
}
