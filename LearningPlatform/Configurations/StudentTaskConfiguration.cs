using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class StudentTaskConfiguration : IEntityTypeConfiguration<StudentTaskEntity>
    {
        public void Configure(EntityTypeBuilder<StudentTaskEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.User)
                .WithMany(u => u.StudentTasks)
                .HasForeignKey(s => s.UserId);

            builder.HasOne(s => s.Task)
                .WithMany(t => t.StudentTask)
                .HasForeignKey(s => s.TaskId);

            builder.HasMany(s => s.FilePaths)
                .WithOne(f => f.StudentTask)
                .HasForeignKey(f => f.StudentTaskId);

            builder.HasMany(s => s.Comments)
                .WithOne(c => c.StudentTask)
                .HasForeignKey(c => c.StudentTaskId);
        }
    }
}
