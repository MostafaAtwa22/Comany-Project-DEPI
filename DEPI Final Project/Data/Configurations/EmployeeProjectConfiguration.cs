using Microsoft.EntityFrameworkCore;
using DEPI_Final_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_Final_Project.Data.Configurations
{
    public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProjects>
    {
        public void Configure(EntityTypeBuilder<EmployeeProjects> builder)
        {
            builder.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.Property(e => e.StartDate)
                .HasColumnType("DATE")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
