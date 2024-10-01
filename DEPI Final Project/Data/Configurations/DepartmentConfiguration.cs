using Microsoft.EntityFrameworkCore;
using DEPI_Final_Project.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_Final_Project.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasOne(d => d.Manager)
                .WithOne(e => e.DepartmentManager)
                .HasForeignKey<Department>(d => d.ManagerId)
                .IsRequired(false);
        }
    }
}
