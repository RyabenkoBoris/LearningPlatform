using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<GradeEntity>
    {
        public void Configure(EntityTypeBuilder<GradeEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.User)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.UserId);

            builder.HasOne(g => g.Journal)
                .WithMany(j => j.Grades)
                .HasForeignKey(g => g.JournalId);

            builder.HasOne(g => g.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(g => g.TeacherId);

            builder.HasOne(g => g.AssessmentType)
                .WithMany(a => a.Grades)
                .HasForeignKey(g => g.AssessmentTypeId);
        }
    }
}
