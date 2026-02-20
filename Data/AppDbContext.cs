using DecisionMaker.Models;
using Microsoft.EntityFrameworkCore;

namespace DecisionMaker.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<User>               Users               => Set<User>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(u => u.Username).IsUnique();
            e.HasIndex(u => u.Email).IsUnique();
        });

        b.Entity<PasswordResetToken>(e =>
        {
            e.HasOne(t => t.User)
             .WithMany(u => u.PasswordResetTokens)
             .HasForeignKey(t => t.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}