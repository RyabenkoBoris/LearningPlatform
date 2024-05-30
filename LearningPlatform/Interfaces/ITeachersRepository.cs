using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ITeachersRepository
    {
        Task AddLesson(Guid id, int? lessonId);
        Task DeleteLesson(Guid id, int? lessonId);
        Task<TeacherEntity?> GetByIdWithLessons(Guid id);
        Task<TeacherEntity?> GetByUserId(Guid id);
        Task<Guid> GetUserIdById(Guid id);
        Task<List<LessonEntity>> GetLessons();
        Task<List<LessonEntity>> GetNonAvailableLessons(Guid id);
        Task<List<LessonEntity>> GetAvailableLessons(Guid id);
        Task<TeacherEntity?> GetByIdWithGroups(Guid id);
        Task<List<GroupEntity>> GetGroups();
        Task<List<GroupEntity>> GetNonAvailableGroups(Guid id);
        Task<List<GroupEntity>> GetAvailableGroups(Guid id);
        Task AddGroup(Guid id, int? groupId);
        Task DeleteGroup(Guid id, int? groupId);
    }
}