using Microsoft.AspNetCore.Mvc;
using Schedule.Core.DTOs;
using Schedule.Core.Service;

namespace Schedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeacherDto>>> Get()
        {
            return Ok(await _teacherService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> Get(int id)
        {
            var teacher = await _teacherService.GetById(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        [HttpGet("{id}/classes")]
        public async Task<ActionResult<List<TeacherClassDto>>> GetClasses(int id)
        {
            var teacher = await _teacherService.GetById(id);
            if (teacher == null)
                return NotFound();

            return Ok(teacher.Classes);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> Post([FromBody] TeacherCreateDto newTeacher)
        {
            var created = await _teacherService.AddTeacher(newTeacher);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherDto>> Put(int id, [FromBody] TeacherUpdateDto updatedTeacher)
        {
            var teacher = await _teacherService.UpdateTeacher(id, updatedTeacher);
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _teacherService.DeleteTeacher(id))
                return NotFound();

            return NoContent();
        }
    }
}
