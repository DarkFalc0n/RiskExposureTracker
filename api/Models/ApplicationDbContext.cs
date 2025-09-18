using Microsoft.EntityFrameworkCore;

namespace RiskExposureTracker.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets for each model
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<ExposureSummary> ExposureSummaries { get; set; }
        public DbSet<Mitigation> Mitigations { get; set; }
        public DbSet<RiskReport> RiskReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           

            modelBuilder.Entity<Risk>()
                .HasOne(r => r.Organizations)
                .WithMany(o => o.Risks)
                .HasForeignKey(r => r.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExposureSummary>()
                .HasOne(e => e.Organizations)
                .WithMany(o => o.ExposureSummaries)
                .HasForeignKey(e => e.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mitigation>()
                .HasOne(m => m.Risk)
                .WithMany(r => r.Mitigations)
                .HasForeignKey(m => m.RiskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RiskReport>()
                .HasOne(rp => rp.Organizations)
                .WithMany(o => o.RiskReports)
                .HasForeignKey(rp => rp.OrgId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

