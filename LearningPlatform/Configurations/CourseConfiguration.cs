using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<CourseEntity>
    {
        public void Configure(EntityTypeBuilder<CourseEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.Teachers)
                .WithMany(t => t.Courses);

            builder.HasMany(c => c.Groups)
                .WithMany(g => g.Courses);

            builder.HasMany(c => c.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);
        }
    }
}
