using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class AssessmentTypeConfiguration : IEntityTypeConfiguration<AssessmentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<AssessmentTypeEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.Grades)
                .WithOne(g => g.AssessmentType)
                .HasForeignKey(g => g.AssessmentTypeId);

            builder.HasData(GenerateAssessmentTypes());
        }
        private AssessmentTypeEntity[] GenerateAssessmentTypes()
        {
            AssessmentTypeEntity[] assessmentTypeEntities = [
                new AssessmentTypeEntity {Id = 1, Name = "Лекція"},
                new AssessmentTypeEntity {Id = 2, Name = "Практичне заняття"},
                new AssessmentTypeEntity {Id = 3, Name = "Лабораторне заняття"},
                new AssessmentTypeEntity {Id = 4, Name = "Модульна контрольна робота"},
                new AssessmentTypeEntity {Id = 5, Name = "Розрахунково-графічна робота"},
                new AssessmentTypeEntity {Id = 6, Name = "Домашня контрольна робота"},
                new AssessmentTypeEntity {Id = 7, Name = "Залік"},
                ];
            return assessmentTypeEntities;
        }
    }
}
