using BLL.DTO;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypesController : ControllerBase
    {
        private readonly TaskTypeService _service;

        public TaskTypesController(TaskTypeService service)
        {
            _service = service;
        }

        // GET: api/TaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTypeDto>>> GetTaskTypes()
        {
            return Ok(await _service.GetAllAsync());
        }

        // GET: api/TaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTypeDto>> GetTaskType(Guid id)
        {
            return Ok(await _service.GetOneAsync(id));
        }

        // PUT: api/TaskTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskType(Guid id, TaskTypeDto taskType)
        {
            if (id != taskType.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(taskType);

            return NoContent();
        }

        // POST: api/TaskTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTypeDto>> PostTaskType(TaskTypeDto taskType)
        {
            await _service.CreateAsync(taskType);

            return CreatedAtAction("GetTestTask", new { id = taskType.Id }, taskType);
        }

        // DELETE: api/TaskTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskType(Guid id)
        {
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
