using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ILessonsRepository
    {
        Task<List<LessonEntity>> Get();
        Task<LessonEntity?> GetById(int id);
        Task Add(string name);
        Task Update(int id, string name);
        Task Delete(int id);
    }
}