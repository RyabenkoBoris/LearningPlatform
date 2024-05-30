using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IGroupRepository
    {
        Task Add(string name, int departmentId, int specialityId);
        Task Delete(int id);
        Task<List<GroupEntity>> Get();
        Task<GroupEntity?> GetById(int id);
        Task<List<DepartmentEntity>> GetDepartments();
        Task<List<FacultyEntity>> GetFaculties();
        Task<List<SpecialityEntity>> GetSpecialities();
        Task Update(int id, string name, int specialityId);
    }
}