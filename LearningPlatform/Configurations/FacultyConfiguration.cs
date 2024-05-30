using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<FacultyEntity>
    {
        public void Configure(EntityTypeBuilder<FacultyEntity> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany(f => f.Departments)
                .WithOne(d => d.Faculty)
                .HasForeignKey(d => d.FacultyId);
        }
    }
}
