using DecisionHelper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DecisionHelper.Data
{
    //Database for application
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        //Flairs
        public DbSet<Flair> Flairs { get; set; }
        //Completed decision and their winning option
        public DbSet<DecisionRecord> DecisionRecords { get; set; }
        //Options submitted by all users
        public DbSet<DecisionOption> DecisionOptions { get; set; }
        //Options submitted by individual users
        public DbSet<UserDecisionOption> UserDecisionOptions { get; set; }
        //Criteria by individual userss
        public DbSet<UserCriterion> UserCriteria { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
