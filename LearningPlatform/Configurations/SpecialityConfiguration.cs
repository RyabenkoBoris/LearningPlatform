using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.Configurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<SpecialityEntity>
    {
        public void Configure(EntityTypeBuilder<SpecialityEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Groups)
                .WithOne(g => g.Speciality)
                .HasForeignKey(g => g.SpecialityId);

            builder.HasData(GenerateSpecialities());
        }
        private SpecialityEntity[] GenerateSpecialities()
        {
            SpecialityEntity[] specialityEntities =
            [
                new SpecialityEntity {Id = 1, Code = 23, Name = "Образотворче мистецтво, декоративне мистецтво, реставрація"},
                new SpecialityEntity {Id = 2, Code = 35, Name = "Філологія"},
                new SpecialityEntity {Id = 3, Code = 51, Name = "Економіка"},
                new SpecialityEntity {Id = 4, Code = 53, Name = "Психологія"},
                new SpecialityEntity {Id = 5, Code = 54, Name = "Соціологія"},
                new SpecialityEntity {Id = 6, Code = 61, Name = "Журналістика"},
                new SpecialityEntity {Id = 7, Code = 72, Name = "Фінанси, банківська справа та страхування"},
                new SpecialityEntity {Id = 8, Code = 73, Name = "Менеджмент"},
                new SpecialityEntity {Id = 9, Code = 75, Name = "Маркетинг"},
                new SpecialityEntity {Id = 10, Code = 81, Name = "Право"},
                new SpecialityEntity {Id = 11, Code = 101, Name = "Екологія"},
                new SpecialityEntity {Id = 12, Code = 104, Name = "Фізика та астрономія"},
                new SpecialityEntity {Id = 13, Code = 105, Name = "Прикладна фізика та наноматеріали"},
                new SpecialityEntity {Id = 14, Code = 111, Name = "Математика"},
                new SpecialityEntity {Id = 15, Code = 113, Name = "Прикладна математика"},
                new SpecialityEntity {Id = 16, Code = 121, Name = "Інженерія програмного забезпечення"},
                new SpecialityEntity {Id = 17, Code = 122, Name = "Комп’ютерні науки та інформаційні технології"},
                new SpecialityEntity {Id = 18, Code = 123, Name = "Комп’ютерна інженерія"},
                new SpecialityEntity {Id = 19, Code = 124, Name = "Системний аналіз"},
                new SpecialityEntity {Id = 20, Code = 125, Name = "Кібербезпека"},
                new SpecialityEntity {Id = 21, Code = 126, Name = "Інформаційні системи та технології"},
                new SpecialityEntity {Id = 22, Code = 131, Name = "Прикладна механіка"},
                new SpecialityEntity {Id = 23, Code = 132, Name = "Матеріалознавство"},
                new SpecialityEntity {Id = 24, Code = 133, Name = "Галузеве машинобудування"},
                new SpecialityEntity {Id = 25, Code = 134, Name = "Авіаційна та ракетно-космічна техніка"},
                new SpecialityEntity {Id = 26, Code = 136, Name = "Металургія"},
                new SpecialityEntity {Id = 27, Code = 141, Name = "Електроенергетика, електротехніка та електромеханіка"},
                new SpecialityEntity {Id = 28, Code = 142, Name = "Енергетичне машинобудування"},
                new SpecialityEntity {Id = 29, Code = 143, Name = "Атомна енергетика"},
                new SpecialityEntity {Id = 30, Code = 144, Name = "Теплоенергетика"},
                new SpecialityEntity {Id = 31, Code = 161, Name = "Хімічні технології та інженерія"},
                new SpecialityEntity {Id = 32, Code = 162, Name = "Біотехнології та біоінженерія"},
                new SpecialityEntity {Id = 33, Code = 163, Name = "Біомедична інженерія"},
                new SpecialityEntity {Id = 34, Code = 171, Name = "Електроніка"},
                new SpecialityEntity {Id = 35, Code = 172, Name = "Телекомунікації та радіотехніка"},
                new SpecialityEntity {Id = 36, Code = 173, Name = "Авіоніка"},
                new SpecialityEntity {Id = 37, Code = 174, Name = "Автоматизація, комп’ютерно-інтегровані технології та робототехніка"},
                new SpecialityEntity {Id = 38, Code = 175, Name = "Інформаційно-вимірювальні технології"},
                new SpecialityEntity {Id = 39, Code = 176, Name = "Мікро- та наносистемна техніка"},
                new SpecialityEntity {Id = 40, Code = 183, Name = "Технології захисту навколишнього середовища"},
                new SpecialityEntity {Id = 41, Code = 184, Name = "Гірництво"},
                new SpecialityEntity {Id = 42, Code = 186, Name = "Видавництво та поліграфія"},
                new SpecialityEntity {Id = 43, Code = 227, Name = "Фізична реабілітація"},
                new SpecialityEntity {Id = 44, Code = 231, Name = "Соціальна робота"},
                new SpecialityEntity {Id = 45, Code = 281, Name = "Публічне управління та адміністрування"},
            ];
            return specialityEntities;
        }
    }
}
