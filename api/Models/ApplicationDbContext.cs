using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace RiskExposureTracker.Models
{
    public class ApplicationDbContext : IdentityDbContext<OrgModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // public DbSet<Organization> Organizations { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<ExposureSummary> ExposureSummaries { get; set; }
        public DbSet<Mitigation> Mitigations { get; set; }
        public DbSet<RiskReport> RiskReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var regionConverter = new EnumToStringConverter<Region>();
            modelBuilder
                .Entity<OrgModel>()
                .Property(u => u.Region)
                .HasConversion(regionConverter)
                .HasMaxLength(50);

            var riskStatusConverter = new EnumToStringConverter<RiskStatus>();
            modelBuilder
                .Entity<Risk>()
                .Property(r => r.Status)
                .HasConversion(riskStatusConverter)
                .HasMaxLength(20);

            var riskCategoryConverter = new EnumToStringConverter<RiskCategory>();
            modelBuilder
                .Entity<Risk>()
                .Property(r => r.Category)
                .HasConversion(riskCategoryConverter)
                .HasMaxLength(50);

            _ = modelBuilder
                .Entity<Risk>()
                .HasOne(r => r.Organizations)
                .WithMany(o => o.Risks)
                .HasForeignKey(r => r.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder
                .Entity<Risk>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            _ = modelBuilder
                .Entity<ExposureSummary>()
                .HasOne(e => e.Organizations)
                .WithMany(o => o.ExposureSummaries)
                .HasForeignKey(e => e.OrgId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder
                .Entity<Mitigation>()
                .HasOne(m => m.Risk)
                .WithMany(r => r.Mitigations)
                .HasForeignKey(m => m.RiskId)
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder
                .Entity<RiskReport>()
                .HasOne(rp => rp.Organizations)
                .WithMany(o => o.RiskReports)
                .HasForeignKey(rp => rp.OrgId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
