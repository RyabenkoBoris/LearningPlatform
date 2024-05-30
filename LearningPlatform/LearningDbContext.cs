using LearningPlatform.Configurations;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform
{
    public class LearningDbContext(DbContextOptions<LearningDbContext> options)
        : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<SpecialityEntity> Specialities { get; set; }
        public DbSet<SheduleSampleEntity> SheduleSamples { get; set; }
        public DbSet<SectionEntity> Sections { get; set; }
        public DbSet<LessonEntity> Lessons { get; set; }
        public DbSet<JournalEntity> Journals { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }
        public DbSet<FilePathEntity> FilePaths { get; set; }
        public DbSet<FacultyEntity> Faculties { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<AssessmentTypeEntity> AssessmentTypes { get; set; }
        public DbSet<ClassTypeEntity> ClassTypes { get; set; }
        public DbSet<StudentTaskEntity> StudentTasks { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssessmentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new FacultyConfiguration());
            modelBuilder.ApplyConfiguration(new FilePathConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new JournalConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new SheduleSampleConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialityConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ClassTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentTaskConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
