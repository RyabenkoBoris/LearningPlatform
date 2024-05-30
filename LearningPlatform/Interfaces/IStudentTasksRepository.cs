using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IStudentTasksRepository
    {
        Task Cancel(int id);
        Task Delete(int id);
        Task Estimate(int id, int mark);
        Task<StudentTaskEntity?> GetById(int id);
        Task<List<StudentTaskEntity>> GetStudentTasks(int taskId);
        Task<bool> HasPermission(Guid id, int studentTaskId);
        Task<bool> HasTeacherAccess(Guid id, int studentTaskId);
        Task Submit(int id);
    }
}