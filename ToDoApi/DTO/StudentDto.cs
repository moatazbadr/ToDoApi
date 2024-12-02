namespace ToDoApi.DTO
{
    public class StudentDto { 
        public int Id { get; set; } 
        public string? Name { get; set; } 
        public List<TodoItemDto> TodoItems { get; set; } = new List<TodoItemDto>();
    
    }
}
