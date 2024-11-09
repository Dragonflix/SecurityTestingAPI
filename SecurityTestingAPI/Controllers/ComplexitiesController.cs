using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexitiesController : ControllerBase
    {
        private readonly ComplexityService _service;

        public ComplexitiesController(ComplexityService service)
        {
            _service = service;
        }

        // GET: api/Complexities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplexityDTO>>> GetComplexities()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/Complexities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexityDTO>> GetComplexity(Guid id)
        {
            return Ok(await _service.GetOneAsync(id));
        }

        // PUT: api/Complexities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComplexity(Guid id, ComplexityDTO complexity)
        {
            if (id != complexity.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(complexity);

            return NoContent();
        }

        // POST: api/Complexities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComplexityDTO>> PostComplexity(ComplexityDTO complexity)
        {
            await _service.CreateAsync(complexity);

            return CreatedAtAction("GetTestTask", new { id = complexity.Id }, complexity);
        }

        // DELETE: api/Complexities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplexity(Guid id)
        {
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
