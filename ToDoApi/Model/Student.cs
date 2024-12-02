namespace ToDoApi.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public ICollection<TodoItem> todoItems { get; set; }=new List<TodoItem>();

    }
}
