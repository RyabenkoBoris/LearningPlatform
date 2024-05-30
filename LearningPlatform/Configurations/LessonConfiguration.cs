using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
    {
        public void Configure(EntityTypeBuilder<LessonEntity> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasMany(l => l.Teachers)
                .WithMany(t => t.Lessons);

            builder.HasMany(l => l.Journals)
                .WithOne(j => j.Lesson)
                .HasForeignKey(j => j.LessonId);

            builder.HasMany(l => l.Classes)
                .WithOne(c => c.Lesson)
                .HasForeignKey(c => c.LessonId);

            builder.HasMany(l => l.Departaments)
                .WithMany(d => d.Lessons);
        }
    }
}
