using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class FilePathConfiguration : IEntityTypeConfiguration<FilePathEntity>
    {
        public void Configure(EntityTypeBuilder<FilePathEntity> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Task)
                .WithMany(t => t.FilePaths)
                .HasForeignKey(f => f.TaskId);

            builder.HasOne(f => f.StudentTask)
                .WithMany(s => s.FilePaths)
                .HasForeignKey(f => f.StudentTaskId);
        }
    }
}
