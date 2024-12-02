using Microsoft.EntityFrameworkCore;
using ToDoApi.Model;

namespace ToDoApi.Context
{
    public class ToDoContext :DbContext
    {
        public ToDoContext (DbContextOptions<ToDoContext> todoContext):base(todoContext)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
           


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> students { get; set; }

        public DbSet<TodoItem> todoItems { get; set; }
    }
}
