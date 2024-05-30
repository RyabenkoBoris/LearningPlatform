using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly LearningDbContext _context;

        public CommentsRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommentEntity>> GetStudentTaskComments(int id)
        {
            return await _context.Comments
                .AsNoTracking()
                .Where(c => c.StudentTaskId == id)
                .Include(c => c.User)
                .OrderBy(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task Add(string text, Guid userId, int studentTaskId)
        {
            var comment = new CommentEntity
            {
                Text = text,
                CreatedDate = DateTime.UtcNow,
                UserId = userId,
                StudentTaskId = studentTaskId,
            };

            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            await _context.Comments
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> StudentWriteAccess(Guid id, int studentTaskId)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentTaskId && s.UserId == id);
            if (studentTask == null) return false;
            return true;
        }

        public async Task<bool> TeacherWriteAccess(Guid id, int studentTaskId)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentTaskId &&
                (s.Task.Section.Course.UserId == id || s.Task.Section.Course.Teachers.Any(t => t.UserId == id)));
            if (studentTask == null) return false;
            return true;
        }

        public async Task<bool> HasPermissionToDelete(Guid id, int commentId)
        {
            var studentTask = await _context.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == commentId && s.UserId == id);
            if (studentTask == null) return false;
            return true;
        }
    }
}
