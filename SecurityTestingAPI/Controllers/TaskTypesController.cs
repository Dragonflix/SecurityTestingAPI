using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityTestingAPI;
using SecurityTestingAPI.Models;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTypesController : ControllerBase
    {
        private readonly TestingDBContext _context;

        public TaskTypesController(TestingDBContext context)
        {
            _context = context;
        }

        // GET: api/TaskTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskType>>> GetTaskTypes()
        {
            return await _context.TaskTypes.ToListAsync();
        }

        // GET: api/TaskTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskType>> GetTaskType(Guid id)
        {
            var taskType = await _context.TaskTypes.FindAsync(id);

            if (taskType == null)
            {
                return NotFound();
            }

            return taskType;
        }

        // PUT: api/TaskTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskType(Guid id, TaskType taskType)
        {
            if (id != taskType.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskType>> PostTaskType(TaskType taskType)
        {
            _context.TaskTypes.Add(taskType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskType", new { id = taskType.Id }, taskType);
        }

        // DELETE: api/TaskTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskType(Guid id)
        {
            var taskType = await _context.TaskTypes.FindAsync(id);
            if (taskType == null)
            {
                return NotFound();
            }

            _context.TaskTypes.Remove(taskType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTypeExists(Guid id)
        {
            return _context.TaskTypes.Any(e => e.Id == id);
        }
    }
}
