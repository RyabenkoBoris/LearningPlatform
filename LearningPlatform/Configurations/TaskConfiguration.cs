using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(t => t.Section)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.SectionId);

            builder.HasMany(t => t.StudentTask)
                .WithOne(s => s.Task)
                .HasForeignKey(s => s.TaskId);

            builder.HasMany(t => t.FilePaths)
                .WithOne(f => f.Task)
                .HasForeignKey(f => f.TaskId);
        }
    }
}
