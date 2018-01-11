using Microsoft.EntityFrameworkCore;

namespace webapi_with_test.webapi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) 
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}