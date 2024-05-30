using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IFilesRepository
    {
        Task Add(IFormFile file, int taskId);
        Task AddStudentFile(IFormFile file, int studentTaskId);
        Task Delete(int id);
        Task<FilePathEntity?> GetById(int id);
        Task<bool> HasPermissionToDelete(Guid id, int fileId);
        Task<bool> HasPermissionToDownload(Guid id, int fileId);
    }
}