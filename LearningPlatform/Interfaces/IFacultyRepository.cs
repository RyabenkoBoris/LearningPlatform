using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IFacultyRepository
    {
        Task Add(string name, string abbrevation);
        Task Delete(int id);
        Task<List<FacultyEntity>> Get();
        Task<FacultyEntity?> GetById(int id);
        Task Update(int id, string name, string abbrevation);
    }
}