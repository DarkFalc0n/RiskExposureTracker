using Microsoft.EntityFrameworkCore;
using System;

namespace CapstoneProject_RiskExposureTrackingAndReportingSystem.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets for each model
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<Risks> Risks { get; set; }
        public DbSet<ExposureSummary> ExposureSummaries { get; set; }
        public DbSet<Mitigations> Mitigations { get; set; }
        public DbSet<RiskReports> RiskReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<Risks>()
                .HasOne(r => r.Organizations)
                .WithMany(o => o.Risks)
                .HasForeignKey(r => r.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExposureSummary>()
                .HasOne(e => e.Organizations)
                .WithMany(o => o.ExposureSummaries)
                .HasForeignKey(e => e.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mitigations>()
                .HasOne(m => m.Risk)
                .WithMany(r => r.Mitigations)
                .HasForeignKey(m => m.RiskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RiskReports>()
                .HasOne(rp => rp.Organizations)
                .WithMany(o => o.RiskReports)
                .HasForeignKey(rp => rp.OrgId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

