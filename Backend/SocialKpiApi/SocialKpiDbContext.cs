using Microsoft.EntityFrameworkCore;
using SocialKpiApi.Models;
using System.ComponentModel.DataAnnotations;

public class SocialKpiDbContext : DbContext
{
    public SocialKpiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EventRegistration> EventRegistrations => Set<EventRegistration>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Participants)
            .WithMany(p => p.Events)
            .UsingEntity<EventRegistration>(
                x => x.HasOne(x => x.Employee)
                      .WithMany().HasForeignKey(x => x.EmployeeId),
                x => x.HasOne(x => x.Event)
                      .WithMany().HasForeignKey(x => x.EventId)
            );
    }
}
