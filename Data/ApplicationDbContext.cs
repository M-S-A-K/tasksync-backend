using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
}