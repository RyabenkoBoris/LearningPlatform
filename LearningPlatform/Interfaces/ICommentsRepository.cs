using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ICommentsRepository
    {
        Task Add(string text, Guid userId, int studentTaskId);
        Task Delete(int id);
        Task<List<CommentEntity>> GetStudentTaskComments(int id);
        Task<bool> HasPermissionToDelete(Guid id, int commentId);
        Task<bool> StudentWriteAccess(Guid id, int studentTaskId);
        Task<bool> TeacherWriteAccess(Guid id, int studentTaskId);
    }
}