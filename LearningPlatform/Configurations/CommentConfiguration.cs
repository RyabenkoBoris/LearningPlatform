using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(c => c.StudentTask)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StudentTaskId);

            builder.HasOne(c => c.User)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.UserId);
        }
    }
}
