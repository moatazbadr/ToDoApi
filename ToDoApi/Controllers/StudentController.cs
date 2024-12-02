using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Context;
using ToDoApi.Model;
using ToDoApi.DTO;
using System.Linq;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ToDoContext context;

        public StudentController(ToDoContext context)
        {
            this.context = context;
        }

        [HttpGet("get-all-student")]
        public IActionResult DisplayAll()
        {
            var students = context.students
                .Include(s => s.todoItems)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    TodoItems = s.todoItems.Select(t => new TodoItemDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        IsDone = t.isDone,
                        CreationDate = t.CreationDate
                    }).ToList()
                }).ToList();
            return Ok(students);
        }

        [HttpPost("add-student")]
        public IActionResult AddStudent([FromBody] StudentRegisterDto studentDto)
        {
            if (studentDto == null || string.IsNullOrEmpty(studentDto.name))
            {
                return BadRequest("Invalid student data.");
            }

            var student = new Student()
            {
                Name = studentDto.name,
            };
            context.students.Add(student);
            context.SaveChanges();
            return Ok(student);
        }
    } 
}

        //[HttpPost("add-todo/{studentId}")]
        //public IActionResult AddTodoItem(int studentId, [FromBody] TodoItemDto todoItemDto)
        //{
        //    var student = context.students.Find(studentId);
        //    if (student == null)
        //    {
        //        return NotFound("Student not found.");
        //    }

        //    var newTodoItem = new TodoItem()
        //    {
        //        Title = todoItemDto.Title,
        //        Description = todoItemDto.Description,
        //        isDone = todoItemDto.IsDone,
        //        CreationDate = todoItemDto.CreationDate,
        //        StudentId = studentId,
                
        //    };

        //    context.todoItems.Add(newTodoItem);
        //    context.SaveChanges();
        //    return Ok(newTodoItem);
        //}
