using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoitems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoitem(int id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            var todoitem = await _context.TodoItems.FindAsync(id);

            if (todoitem == null)
            {
                return NotFound();
            }

            return todoitem;
        }

        // PUT: api/ToDo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoitem(int id, TodoItem todoitem)
        {
            if (id != todoitem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoitemExists(id))
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

        // POST: api/ToDo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoitem(TodoItem todoitem)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'ToDoDbContext.Todoitems'  is null.");
          }
            _context.TodoItems.Add(todoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoitem", new { id = todoitem.Id }, todoitem);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoitem(int id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoitem = await _context.TodoItems.FindAsync(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoitemExists(int id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
