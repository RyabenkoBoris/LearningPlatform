using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IJournalRepository
    {
        Task Add(int groupId, int lessonId);
        Task AddToGroup(int groupId);
        Task Delete(int groupId, int lessonId);
        Task<List<JournalEntity>> GetByGroupId(int id);
        Task<List<JournalEntity>> GetByUserId(Guid id);
        Task<List<JournalEntity>> GetTeacherJournals(Guid id);
        Task<JournalEntity?> GetWithLessonGroupById(int id);
        Task<JournalEntity?> GetWithUser(int id);
        Task<bool> HasStudentAccess(Guid id, int journalId);
        Task<bool> HasTeacherAccess(Guid id, int journalId);
        Task Hide(int id);
        Task SetSemester(int id, byte semester);
        Task Show(int id);
    }
}