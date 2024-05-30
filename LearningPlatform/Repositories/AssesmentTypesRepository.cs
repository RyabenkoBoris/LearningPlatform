using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class AssesmentTypesRepository : IAssesmentTypesRepository
    {
        private readonly LearningDbContext _context;

        public AssesmentTypesRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<AssessmentTypeEntity>> Get()
        {
            return await _context.AssessmentTypes
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
