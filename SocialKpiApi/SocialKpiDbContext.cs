using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class SocialKpiDbContext : DbContext
{
    public SocialKpiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}

public class Todo
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
}