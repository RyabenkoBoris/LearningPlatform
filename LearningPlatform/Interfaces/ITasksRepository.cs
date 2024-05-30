using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ITasksRepository
    {
        Task Add(string name, short maxMark, int sectionId, DateTime duedate, string taskText, Guid userId);
        Task Delete(int id);
        Task<TaskEntity?> GetById(int id);
        Task<CourseEntity?> GetCourseById(int id);
        Task Update(int id, string name, DateTime duedate, string taskText);
        Task<StudentTaskEntity?> GetStudentTaskById(int taskId, Guid id);
        Task AddStudentTaskForGroup(int groupId, int taskId);
        Task<bool> HasPermissionToChange(Guid id, int taskId);
        Task<bool> HasAccess(Guid id, int taskId);
    }
}