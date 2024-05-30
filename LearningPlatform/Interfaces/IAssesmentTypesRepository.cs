using LearningPlatform.Models;

namespace LearningPlatform.Interfaces
{
    public interface IAssesmentTypesRepository
    {
        Task<List<AssessmentTypeEntity>> Get();
    }
}