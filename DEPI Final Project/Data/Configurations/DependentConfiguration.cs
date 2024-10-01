using Microsoft.EntityFrameworkCore;
using DEPI_Final_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DEPI_Final_Project.Models.Enums;

namespace DEPI_Final_Project.Data.Configurations
{
    public class DependentConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.HasKey(d => new { d.Name, d.EmployeeId });

            builder.HasOne(d => d.Employee)
                .WithMany(e => e.Dependents)
                .HasForeignKey(d => d.EmployeeId)
                .IsRequired(true);

            builder.Property(e => e.Gender)
                .HasConversion(
                    e => e.ToString(),
                    e => (Gender)Enum.Parse(typeof(Gender), e)
                );
        }
    }
}
