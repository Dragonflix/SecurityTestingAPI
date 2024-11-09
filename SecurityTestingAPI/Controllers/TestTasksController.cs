using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class TestTasksController : ControllerBase
    {
        private readonly TestingDBContext _context;
        private readonly IMapper _mapper;

        public TestTasksController(TestingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TestTasks
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestTaskDTO>>> GetTestTasks()
        {
            return await _context.TestTasks.Select(tt => _mapper.Map<TestTaskDTO>(tt)).ToListAsync();
        }

        // GET: api/TestTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestTaskDTO>> GetTestTask(Guid id)
        {
            var testTask = await _context.TestTasks.FindAsync(id);

            if (testTask == null)
            {
                return NotFound();
            }

            return _mapper.Map<TestTaskDTO>(testTask);
        }

        // PUT: api/TestTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestTask(Guid id, TestTaskDTO testTask)
        {
            if (id != testTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<TestTask>(testTask)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestTaskExists(id))
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

        // POST: api/TestTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult<TestTaskDTO>> PostTestTask(TestTaskDTO testTask)
        {
            _context.TestTasks.Add(_mapper.Map<TestTask>(testTask));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestTask", new { id = testTask.Id }, testTask);
        }

        // DELETE: api/TestTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestTask(Guid id)
        {
            var testTask = await _context.TestTasks.FindAsync(id);
            if (testTask == null)
            {
                return NotFound();
            }

            _context.TestTasks.Remove(testTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestTaskExists(Guid id)
        {
            return _context.TestTasks.Any(e => e.Id == id);
        }
    }
}
