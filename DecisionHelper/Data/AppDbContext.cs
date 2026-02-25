using Microsoft.EntityFrameworkCore;
using DecisionHelper.Models;

namespace DecisionHelper.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Flair> Flairs { get; set; }
        public DbSet<DecisionRecord> DecisionRecords { get; set; }
        public DbSet<DecisionOption> DecisionOptions { get; set; }
    }
}