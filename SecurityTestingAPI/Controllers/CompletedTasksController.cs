using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityTestingAPI;
using SecurityTestingAPI.DTO;
using SecurityTestingAPI.Models;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedTasksController : ControllerBase
    {
        private readonly TestingDBContext _context;
        private readonly IMapper _mapper;

        public CompletedTasksController(TestingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CompletedTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedTaskDTO>>> GetCompletedTasks()
        {
            return await _context.CompletedTasks.Select(ct => _mapper.Map<CompletedTaskDTO>(ct)).ToListAsync();
        }

        // GET: api/CompletedTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompletedTaskDTO>> GetCompletedTask(Guid id)
        {
            var completedTask = await _context.CompletedTasks.FindAsync(id);

            if (completedTask == null)
            {
                return NotFound();
            }

            return _mapper.Map<CompletedTaskDTO>(completedTask);
        }

        // PUT: api/CompletedTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedTask(Guid id, CompletedTaskDTO completedTask)
        {
            if (id != completedTask.UserId)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<CompletedTask>(completedTask)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompletedTaskExists(id))
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

        // POST: api/CompletedTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompletedTaskDTO>> PostCompletedTask(CompletedTaskDTO completedTask)
        {
            _context.CompletedTasks.Add(_mapper.Map<CompletedTask>(completedTask));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompletedTaskExists(completedTask.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompletedTask", new { id = completedTask.UserId }, completedTask);
        }

        // DELETE: api/CompletedTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompletedTask(Guid id)
        {
            var completedTask = await _context.CompletedTasks.FindAsync(id);
            if (completedTask == null)
            {
                return NotFound();
            }

            _context.CompletedTasks.Remove(completedTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompletedTaskExists(Guid id)
        {
            return _context.CompletedTasks.Any(e => e.UserId == id);
        }
    }
}
