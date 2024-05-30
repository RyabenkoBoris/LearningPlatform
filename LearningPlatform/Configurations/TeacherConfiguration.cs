using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<TeacherEntity>
    {
        public void Configure(EntityTypeBuilder<TeacherEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithOne(u => u.Teacher)
                .HasForeignKey<TeacherEntity>(t => t.UserId);

            builder.HasMany(t => t.Lessons)
                .WithMany(l => l.Teachers);

            builder.HasMany(t => t.Courses)
                .WithMany(c => c.Teachers);

            builder.HasMany(t => t.Grades)
                .WithOne(g => g.Teacher)
                .HasForeignKey(g => g.TeacherId);

            builder.HasMany(t => t.Classes)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId);

            builder.HasMany(t => t.Groups)
                .WithMany(g => g.Teachers);
        }
    }
}
