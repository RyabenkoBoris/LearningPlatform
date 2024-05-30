using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ISpecialitiesRepository
    {
        Task<List<SpecialityEntity>> Get();
        Task<SpecialityEntity?> GetById(int id);
        Task<List<SpecialityEntity>> GetByPage(int page, int pageSize);
        Task Add(int code, string name);
        Task Update(int id, int code, string name);
        Task Delete(int id);

    }
}