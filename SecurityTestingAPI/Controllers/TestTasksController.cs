using AutoMapper;
using BLL.DTO;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SecurityTestingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTasksController : ControllerBase
    {
        private readonly TestTaskService _service;

        public TestTasksController(TestTaskService service)
        {
            _service = service;
        }

        // GET: api/TestTasks
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestTaskDto>>> GetTestTasks(FilterModel filterModel)
        {
            return Ok(await _service.GetAllAsync(filterModel));
        }

        // GET: api/TestTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestTaskDto>> GetTestTask(Guid id)
        {
            return Ok(await _service.GetOneAsync(id));
        }

        // PUT: api/TestTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Moderator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestTask(Guid id, TestTaskDto testTask)
        {
            if (id != testTask.Id)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(testTask);

            return NoContent();
        }

        // POST: api/TestTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public async Task<ActionResult<TestTaskDto>> PostTestTask(TestTaskDto testTask)
        {
            await _service.CreateAsync(testTask);

            return CreatedAtAction("GetTestTask", new { id = testTask.Id }, testTask);
        }

        // DELETE: api/TestTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestTask(Guid id)
        {
            await _service.RemoveAsync(id);

            return NoContent();
        }
    }
}
