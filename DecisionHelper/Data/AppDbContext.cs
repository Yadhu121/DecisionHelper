using DecisionHelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DecisionHelper.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Flair> Flairs { get; set; }
        public DbSet<DecisionRecord> DecisionRecords { get; set; }
        public DbSet<DecisionOption> DecisionOptions { get; set; }
        public DbSet<UserDecisionOption> UserDecisionOptions { get; set; }
        public DbSet<UserCriterion> UserCriteria { get; set; }
    }
}
