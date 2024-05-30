using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId);

            builder.HasOne(u => u.Teacher)
                .WithOne(t => t.User)
                .HasForeignKey<UserEntity>(u => u.TeacherId);

            builder.HasOne(u => u.Group)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GroupId);

            builder.HasMany(u => u.Grades)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            builder.HasMany(u => u.StudentTasks)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            builder.HasMany(u => u.Courses)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(u => u.Tasks)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasData(new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "admin@admin",
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("12345"),
                Role = Enum.Role.SuperAdmin,
                Name = "Admin",
            });
        }
    }
}
