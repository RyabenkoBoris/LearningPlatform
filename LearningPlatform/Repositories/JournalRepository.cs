using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly LearningDbContext _context;

        public JournalRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<JournalEntity?> GetWithUser(int id)
        {
            return await _context.Journals
                .AsNoTracking()
                .Include(j => j.Group)
                    .ThenInclude(g => g.Users)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<JournalEntity?> GetWithLessonGroupById(int id)
        {
            return await _context.Journals
                .AsNoTracking()
                .Include(j => j.Lesson)
                .Include(j => j.Group)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<List<JournalEntity>> GetByGroupId(int id)
        {
            return await _context.Journals
                .AsNoTracking()
                .Where(j => j.GroupId == id)
                .Include(j => j.Group)
                .Include(j => j.Lesson)
                .ToListAsync();
        }

        public async Task<List<JournalEntity>> GetByUserId(Guid id)
        {
            return await _context.Journals
                .AsNoTracking()
                .Where(j => j.Group.Users.Any(u => u.Id == id))
                .Where(j => j.Visible)
                .Include(j => j.Lesson)
                .ToListAsync();
        }

        public async Task<List<JournalEntity>> GetTeacherJournals(Guid id)
        {
            return await _context.Journals
                .AsNoTracking()
                .Where(j => j.Group.Teachers.Any(t => t.UserId == id) &&
                    j.Group.Teachers.Any(t => t.Lessons.Any(l => l.Id == j.LessonId)))
                .Where(j => j.Visible)
                .Include(j => j.Lesson)
                .Include(j => j.Group)
                .ToListAsync();
        }

        public async Task Add(int groupId, int lessonId)
        {
            var journal = new JournalEntity
            {
                GroupId = groupId,
                LessonId = lessonId
            };

            await _context.AddAsync(journal);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int groupId, int lessonId)
        {
            await _context.Journals
                .Where(j => j.GroupId == groupId && j.LessonId == lessonId)
                .ExecuteDeleteAsync();
        }

        public async Task AddToGroup(int groupId)
        {
            var group = await _context.Groups
                .AsNoTracking()
                .Include(g => g.Department)
                    .ThenInclude(d => d.Lessons)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            foreach (var lesson in group.Department.Lessons)
            {
                await Add(groupId, lesson.Id);
            }
        }
        public async Task SetSemester(int id, byte semester)
        {
            await _context.Journals
                .Where(j => j.Id == id)
                .ExecuteUpdateAsync(j => j
                .SetProperty(j => j.Semester, semester));
        }
        public async Task Hide(int id)
        {
            await _context.Journals
                .Where(j => j.Id == id)
                .ExecuteUpdateAsync(j => j
                .SetProperty(j => j.Visible, false));
        }
        public async Task Show(int id)
        {
            await _context.Journals
                .Where(j => j.Id == id)
                .ExecuteUpdateAsync(j => j
                .SetProperty(j => j.Visible, true));
        }

        public async Task<bool> HasTeacherAccess(Guid id, int journalId)
        {
            var journal = await _context.Journals
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == journalId && j.Group.Teachers.Any(t => t.UserId == id)
                && j.Lesson.Teachers.Any(t => t.UserId == id));
            if (journal == null) return false;
            return true;
        }

        public async Task<bool> HasStudentAccess(Guid id, int journalId)
        {
            var journal = await _context.Journals
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == journalId && j.Group.Users.Any(u => u.Id == id));
            if (journal == null) return false;
            return true;
        }
    }
}
