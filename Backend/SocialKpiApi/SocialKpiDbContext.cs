using Microsoft.EntityFrameworkCore;
using SocialKpiApi.Models;
using System.ComponentModel.DataAnnotations;

public class SocialKpiDbContext : DbContext
{
    public SocialKpiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Employee> Employees => Set<Employee>();
}

public class Todo
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
}