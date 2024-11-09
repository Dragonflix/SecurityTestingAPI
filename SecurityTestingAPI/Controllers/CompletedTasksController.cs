using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedTasksController : ControllerBase
    {
        private readonly CompletedTaskService _service;
        public CompletedTasksController(CompletedTaskService service)
        {
            _service = service;
        }

        // GET: api/CompletedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedTaskDTO>>> GetCompletedTasks()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/CompletedTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompletedTaskDTO>> GetCompletedTask(Guid id)
        {
            return Ok(await _service.GetOneAsync(id));
        }

        // PUT: api/CompletedTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedTask(Guid id, CompletedTaskDTO completedTask)
        {
            if (id != completedTask.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(completedTask);

            return NoContent();
        }

        // POST: api/CompletedTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompletedTaskDTO>> PostCompletedTask(CompletedTaskDTO completedTask)
        {
            await _service.CreateAsync(completedTask);

            return CreatedAtAction("GetTestTask", new { id = completedTask.Id }, completedTask);
        }

        // DELETE: api/CompletedTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompletedTask(Guid id)
        {
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
