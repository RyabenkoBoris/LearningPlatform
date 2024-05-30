using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class GradesRepository : IGradesRepository
    {
        private readonly LearningDbContext _context;

        public GradesRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<GradeEntity>> GetUserGrades(Guid id, int journalId)
        {
            return await _context.Grades
                .AsNoTracking()
                .Where(g => g.UserId == id && g.JournalId == journalId)
                .Include(g => g.Teacher)
                    .ThenInclude(g => g.User)
                .Include(g => g.User)
                .Include(g => g.AssessmentType)
                .OrderBy(g => g.Date)
                .ToListAsync();
        }


        public async Task Add(int mark, string Note, Guid studentId, int journalId, Guid teacherId, int typeOfAssesment)
        {
            var grade = new GradeEntity
            {
                Mark = mark,
                Date = DateTime.UtcNow,
                UserId = studentId,
                Note = Note ?? string.Empty,
                JournalId = journalId,
                TeacherId = teacherId,
                AssessmentTypeId = typeOfAssesment
            };

            await _context.AddAsync(grade);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            await _context.Grades
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> IsTeacher(Guid id, int gradeId)
        {
            var grade = await _context.Grades
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == gradeId && g.Teacher.UserId == id);
            if (grade == null) return false;
            return true;
        }
    }
}
