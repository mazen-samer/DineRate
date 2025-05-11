using DineRate.Models;
using Microsoft.EntityFrameworkCore;

namespace DineRate.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewReaction> ReviewReactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ReviewReaction>()
            .HasIndex(r => new { r.UserId, r.ReviewId })
            .IsUnique();

        modelBuilder.Entity<ReviewReaction>()
            .HasOne(r => r.User)
            .WithMany(u => u.ReviewReactions)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
