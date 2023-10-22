using System;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TodoItemController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public TodoItemController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<TodoItem>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoItem todoItem)
        {
            await _mongoDBService.CreateAsync(todoItem);
            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TodoItem todoItem)
        {
            var existingItem = await _mongoDBService.GetAsyncById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await _mongoDBService.UpdateAsync(id, todoItem);
            return Ok(todoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingItem = await _mongoDBService.GetAsyncById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }

}

