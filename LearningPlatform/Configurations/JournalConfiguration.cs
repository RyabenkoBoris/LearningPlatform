using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class JournalConfiguration : IEntityTypeConfiguration<JournalEntity>
    {
        public void Configure(EntityTypeBuilder<JournalEntity> builder)
        {
            builder.HasKey(j => j.Id);

            builder.HasOne(j => j.Lesson)
                .WithMany(l => l.Journals)
                .HasForeignKey(j => j.LessonId);

            builder.HasOne(j => j.Group)
                .WithMany(g => g.Journals)
                .HasForeignKey(j => j.GroupId);

            builder.HasMany(j => j.Grades)
                .WithOne(g => g.Journal)
                .HasForeignKey(g => g.JournalId);
        }
    }
}
