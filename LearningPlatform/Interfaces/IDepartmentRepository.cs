using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IDepartmentRepository
    {
        Task Add(string name, string abbrevation, int facultyId);
        Task Delete(int id);
        Task Update(int id, string name, string abbrevation, int facultyId);
        Task<List<DepartmentEntity>> Get();
        Task<List<FacultyEntity>> GetFaculties();
        Task<DepartmentEntity?> GetById(int id);
        Task<List<LessonEntity>> GetLessons(int id);
        Task<List<LessonEntity>> GetUnavailableLessons(int id);
        Task<DepartmentEntity?> GetWithLessonsById(int id);
        Task AddLesson(int departamentId, int? lessonId);
        Task DeleteLesson(int departamentId, int? lessonId);
    }
}