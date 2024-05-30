using LearningPlatform.Enum;
using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IAdminUsersRepository
    {
        Task DeleteStudent(Guid id);
        Task DeleteTeacher(Guid id);
        Task<List<UserEntity>> GetAdmins();
        Task<UserEntity?> GetById(Guid id);
        Task<List<UserEntity>> GetByNameOrFacultyOrGroup(string search);
        Task<List<DepartmentEntity>> GetDepartments();
        Task<List<FacultyEntity>> GetFaculties();
        Task<List<GroupEntity>> GetGroups();
        Task<List<UserEntity>> GetStudents();
        Task<List<UserEntity>> GetTeachers();
        Task Register(string email, string password, string name, int? departmentId, Role role);
        Task RegisterStudent(string email, string password, string name, int? groupId);
        Task Update(Guid id, string email, string password, string name, int? departmentId);
        Task UpdateStudent(Guid id, string email, string password, string name, int? groupId);
    }
}