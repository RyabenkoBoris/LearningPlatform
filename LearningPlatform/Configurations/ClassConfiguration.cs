using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<ClassEntity>
    {
        public void Configure(EntityTypeBuilder<ClassEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Lesson)
                .WithMany(l => l.Classes)
                .HasForeignKey(c => c.LessonId);

            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId);

            builder.HasOne(c => c.SheduleSample)
                .WithMany(s => s.Classes)
                .HasForeignKey(c => c.SheduleSampleId);

            builder.HasOne(c => c.ClassType)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.ClassTypeId);

            builder.HasMany(c => c.Groups)
                .WithMany(g => g.Classes);
        }
    }
}
