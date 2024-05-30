using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ISectionsRepository
    {
        Task Add(string name, int courseId);
        Task Delete(int id);
        Task<SectionEntity> GetById(int id);
        Task<CourseEntity?> GetCourseById(int id);
        Task<bool> HasPermissionToChange(Guid id, int sectionId);
        Task Update(int id, string name);
    }
}