using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IGradesRepository
    {
        Task Add(int mark, string Note, Guid studentId, int journalId, Guid teacherId, int typeOfAssesment);
        Task Delete(int id);
        Task<List<GradeEntity>> GetUserGrades(Guid id, int journalId);
        Task<bool> IsTeacher(Guid id, int gradeId);
    }
}