using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class SheduleSampleConfiguration : IEntityTypeConfiguration<SheduleSampleEntity>
    {
        public void Configure(EntityTypeBuilder<SheduleSampleEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Classes)
                .WithOne(c => c.SheduleSample)
                .HasForeignKey(c => c.SheduleSampleId);

            builder.HasData(new SheduleSampleEntity
            {
                Id = 1,
                LessonTime1 = "8:30",
                LessonTime2 = "10:25",
                LessonTime3 = "12:20",
                LessonTime4 = "14:15",
                LessonTime5 = "16:10",
                LessonTime6 = "18:30",
            });
        }
    }
}
