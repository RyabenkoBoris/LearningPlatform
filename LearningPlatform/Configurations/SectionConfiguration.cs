using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
    {
        public void Configure(EntityTypeBuilder<SectionEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Course)
                .WithMany(c => c.Sections)
                .HasForeignKey(s => s.CourseId);

            builder.HasMany(s => s.Tasks)
                .WithOne(t => t.Section)
                .HasForeignKey(t => t.SectionId);
        }
    }
}
