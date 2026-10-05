using Microsoft.AspNetCore.Mvc;
using Schedule.Core.DTOs;
using Schedule.Core.Service;

namespace Schedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniformController : ControllerBase
    {
        private readonly IUniformService _uniformService;

        public UniformController(IUniformService uniformService)
        {
            _uniformService = uniformService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniformDto>>> Get()
        {
            return Ok(await _uniformService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniformDto>> Get(int id)
        {
            var uniform = await _uniformService.GetById(id);
            if (uniform == null)
                return NotFound();

            return Ok(uniform);
        }

        [HttpPost]
        public async Task<ActionResult<UniformDto>> Post([FromBody] UniformCreateDto newUniform)
        {
            var created = await _uniformService.AddUniform(newUniform);
            if (created == null)
                return NotFound();

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UniformDto>> Put(int id, [FromBody] UniformUpdateDto updatedUniform)
        {
            var uniform = await _uniformService.UpdateUniform(id, updatedUniform);
            if (uniform == null)
                return NotFound();

            return Ok(uniform);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _uniformService.DeleteUniform(id))
                return NotFound();

            return NoContent();
        }
    }
}
