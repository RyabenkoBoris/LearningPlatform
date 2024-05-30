
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Faculty)
                .WithMany(f => f.Departments)
                .HasForeignKey(d => d.FacultyId);

            builder.HasMany(d => d.Groups)
                .WithOne(g => g.Department)
                .HasForeignKey(g => g.DepartmentId);

            builder.HasMany(d => d.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.DepartmentId);

            builder.HasMany(d => d.Lessons)
                .WithMany(l => l.Departaments);
        }
    }
}
