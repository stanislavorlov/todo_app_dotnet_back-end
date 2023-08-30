using Microsoft.EntityFrameworkCore;

namespace TodoAppWeb.Models
{
    public class ToDoContext : DbContext
    {
        //declare a parameterless constructor
        public ToDoContext()
        {
        }

        //declare a constructor which accepts DbContextOptions
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            // ensure Database is created
            Database.EnsureCreated();
        }

        public virtual DbSet<ToDo> ToDos { get; set; }
    }
}
