using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class ClassTypeConfiguration : IEntityTypeConfiguration<ClassTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ClassTypeEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(t => t.Classes)
                .WithOne(c => c.ClassType)
                .HasForeignKey(c => c.ClassTypeId);

            builder.HasData(GenerateTypes());
        }
        private ClassTypeEntity[] GenerateTypes()
        {
            ClassTypeEntity[] classTypeEntities =
            [
                new ClassTypeEntity {Id = 1, Name = "Лек on-line"},
                new ClassTypeEntity {Id = 2, Name = "Прак on-line"},
                new ClassTypeEntity {Id = 3, Name = "Лаб on-line"},
            ];
            return classTypeEntities;
        }
    }
}
