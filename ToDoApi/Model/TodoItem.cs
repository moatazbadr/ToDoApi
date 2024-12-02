using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Model
{
    public class TodoItem
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, MaxLength(200)]
        public string Description { get; set; }
        public bool isDone { get; set; } = false;
        public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
        
        public int? StudentId { get; set; }
       
     //   public Student? Student { get; set; }


    }
}
