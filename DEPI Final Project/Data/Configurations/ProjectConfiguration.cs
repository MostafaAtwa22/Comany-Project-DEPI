using Microsoft.EntityFrameworkCore;
using DEPI_Final_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_Final_Project.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<EmployeeProjects>(
                    l => l.HasOne(ep => ep.Employee)
                    .WithMany(e => e.EmployeeProjects)
                    .HasForeignKey(ep => ep.EmployeeId),

                    l => l.HasOne(ep => ep.Project)
                    .WithMany(p => p.EmployeeProjects)
                    .HasForeignKey(ep => ep.ProjectId)
                );

            builder.HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey(p => p.DepartmentId)
                .IsRequired();

            builder.Property(e => e.DateOFDelete)
            .HasColumnType("DATE")
            .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
