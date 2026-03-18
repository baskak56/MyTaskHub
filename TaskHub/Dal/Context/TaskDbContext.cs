using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

public class TaskDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Title).HasMaxLength(500);
            entity.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
        });

        base.OnModelCreating(modelBuilder);
    }
}