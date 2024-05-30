using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Department)
                .WithMany(d => d.Groups)
                .HasForeignKey(g => g.DepartmentId);

            builder.HasOne(g => g.Speciality)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SpecialityId);

            builder.HasMany(g => g.Users)
                .WithOne(u => u.Group)
                .HasForeignKey(u => u.GroupId);

            builder.HasMany(g => g.Courses)
                .WithMany(c => c.Groups);

            builder.HasMany(g => g.Classes)
                .WithMany(c => c.Groups);

            builder.HasMany(g => g.Teachers)
                .WithMany(t => t.Groups);

            builder.HasMany(g => g.Journals)
                .WithOne(j => j.Group)
                .HasForeignKey(j => j.GroupId);
        }
    }
}
