using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserEntity?> GetByEmail(string email);
        Task<UserEntity?> GetStudentById(Guid id);
        Task<UserEntity?> GetTeacherById(Guid id);
    }
}