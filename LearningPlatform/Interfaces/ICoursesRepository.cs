using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface ICoursesRepository
    {
        Task Add(string name, Guid userId);
        Task AddGroup(int? groupId, int? courseId);
        Task AddSection(string name, int courseId);
        Task AddTeacher(Guid? id, int? courseId);
        Task Delete(int id);
        Task DeleteGroup(int? groupId, int? courseId);
        Task DeleteTeacher(Guid? id, int? courseId);
        Task<CourseEntity?> GetById(int id);
        Task<CourseEntity?> GetByIdWithGroups(int id);
        Task<CourseEntity?> GetByIdWithSectionsTasks(int id);
        Task<CourseEntity?> GetByIdWithTeachers(int id);
        Task<string> GetGroupNameById(int id);
        Task<List<GroupEntity>> GetNonAvailableGroups(int courseId);
        Task<List<UserEntity>> GetNonAvailableTeachers(Guid id, int courseId);
        Task<List<CourseEntity>> GetStudentAvailableCourses(Guid id);
        Task<List<CourseEntity>> GetTeacherAvailableCourses(Guid id);
        Task<string> GetTeacherNameById(Guid id);
        Task<bool> IsGroupOwner(Guid id, int groupId);
        Task<bool> IsOneOfTeacher(Guid id, int groupId);
        Task<bool> IsStudentInGroup(Guid id, int groupId);
        Task Update(int id, string name);
    }
}