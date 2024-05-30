using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IClassesRepository
    {
        Task Add(short lessonNumber, bool isFirstWeek, DayOfWeek dayOfWeek, int? lessonId, Guid teacherId, int classTypeId);
        Task AddGroup(int classId, int? groupId);
        Task Delete(int id);
        Task DeleteGroup(int classId, int? groupId);
        Task<List<ClassEntity>> Get(Guid id);
        Task<List<GroupEntity>> GetNonAvailableGroups(Guid TeacherId, int id);
        Task<List<GroupEntity>> GetGroups();
        Task<Guid> GetTeacherIdById(int id);
        Task<List<GroupEntity>> GetAvailableGroups(Guid TeacherId, int id);
        Task<List<ClassTypeEntity>> GetClassTypes();
        Task Update(int classId, int? lessonId, int classTypeId);
        Task<ClassEntity?> GetById(int id);
        Task<List<string>> GetSheduleSampleList(int id);
        Task DeleteAllByLessonTeacher(int? lessonId, Guid teacherId);
    }
}