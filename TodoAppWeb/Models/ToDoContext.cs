using Microsoft.EntityFrameworkCore;

namespace TodoAppWeb.Models
{
    public class ToDoContext : DbContext
    {
        //declare a constructor which accepts DbContextOptions
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            // ensure Database is created
            Database.EnsureCreated();
        }

        public DbSet<ToDo> ToDos { get; set; }
    }
}
