using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoApi.Context;
using ToDoApi.Model;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TodoController(ToDoContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll/{studentId}")]
        public async Task <IActionResult>  GetAllItems(int studentId)
        {
            if (_context.todoItems != null)
            {
               
                var ToDoItems = await _context.todoItems.Where(td=>td.StudentId== studentId).ToListAsync();
                return Ok(ToDoItems);
            }

            return BadRequest("No Items are here ");
        }


        [HttpPost("Add-new-todo/{studentId}")]
        public async Task<IActionResult> AddItem(int studentId, [FromBody] TodoItem itemFromUser)
        {
            if (itemFromUser == null)
            {
                return BadRequest("Item couldn't be added");
            }

            if (ModelState.IsValid)
            {
                var student = await _context.students.FindAsync(studentId);
                if (student == null)
                {
                    return NotFound("Student not found");
                }

                TodoItem todoItem = new TodoItem()
                {
                    Title = itemFromUser.Title,
                    Description = itemFromUser.Description,
                    CreationDate = itemFromUser.CreationDate,
                    isDone = itemFromUser.isDone,
                    StudentId = studentId
                };

                _context.todoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                return Ok("Created");
            }

            return BadRequest("Item couldn't be added");
        }


        [HttpDelete("Delete/{ToDoId}/{studentId}")]
        public async Task<IActionResult> DeleteItem(int ToDoId, int studentId)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.students.FindAsync(studentId);
                if (student == null)
                {
                    return BadRequest("Student was not found");
                }

                var todoItem = await _context.todoItems
                    .FirstOrDefaultAsync(td => td.Id == ToDoId && td.StudentId == studentId);

                if (todoItem == null)
                {
                    return NotFound("Todo item not found");
                }

                _context.todoItems.Remove(todoItem);
                await _context.SaveChangesAsync();

                return Ok("Item deleted successfully");
            }

            return BadRequest("Item was not deleted");
        }

        [HttpGet("sort-by-date/{studentId}")]
        public async Task<IActionResult> SortedItems(int studentId)
        {
           
            if (ModelState.IsValid ) { 
           
            var student= await _context.students.Include(s=>s.todoItems)
                   .FirstOrDefaultAsync(st=>st.Id==studentId);
           
                if (student == null)
                {
                    return BadRequest("student not found or wrong todoItem id");

                }
                else
                {
                    var sortedItems = student.todoItems.OrderByDescending(td=>td.CreationDate).ToList();

                return Ok(sortedItems);
                }
            }
            return BadRequest("no items found");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] TodoItem updatedItem)
        {
            if (updatedItem == null)
            {
                return BadRequest("Item cannot be null");
            }

            var todoItem = await _context.todoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound("Item not found");
            }

            todoItem.Title = updatedItem.Title;
            todoItem.Description = updatedItem.Description;
            todoItem.CreationDate = updatedItem.CreationDate;
            todoItem.isDone = updatedItem.isDone;

            _context.todoItems.Update(todoItem);
            await _context.SaveChangesAsync();

            return Ok("Item updated successfully");
        }


    }
}
