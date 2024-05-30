using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class StudentTasksRepository : IStudentTasksRepository
    {
        private readonly LearningDbContext _context;
        private readonly IFilesRepository _filesRepository;
        public StudentTasksRepository(LearningDbContext context, IFilesRepository filesRepository)
        {
            _context = context;
            _filesRepository = filesRepository;
        }
        public async Task<List<StudentTaskEntity>> GetStudentTasks(int taskId)
        {
            return await _context.StudentTasks
                .AsNoTracking()
                .Include(s => s.User)
                .Where(s => s.TaskId == taskId)
                .ToListAsync();
        }
        public async Task<StudentTaskEntity?> GetById(int id)
        {
            return await _context.StudentTasks
                .AsNoTracking()
                .Include(s => s.User)
                .Include(s => s.Task)
                .Include(s => s.Comments)
                    .ThenInclude(c => c.User)
                .Include(s => s.FilePaths)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task Submit(int id)
        {
            await _context.StudentTasks
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.IsSubmitted, true)
                    .SetProperty(s => s.DueDate, DateTime.UtcNow));
        }

        public async Task Estimate(int id, int mark)
        {
            await _context.StudentTasks
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Mark, mark));
        }

        public async Task Cancel(int id)
        {
            await _context.StudentTasks
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.IsSubmitted, false));
        }
        public async Task Delete(int id)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .Include(t => t.FilePaths)
                .FirstOrDefaultAsync(t => t.Id == id);

            foreach (var file in studentTask.FilePaths)
            {
                await _filesRepository.Delete(file.Id);
            }

            await _context.StudentTasks
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> HasPermission(Guid id, int studentTaskId)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentTaskId && s.UserId == id);

            if (studentTask == null) return false;
            return true;
        }

        public async Task<bool> HasTeacherAccess(Guid id, int studentTaskId)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentTaskId &&
                (s.Task.Section.Course.UserId == id || s.Task.Section.Course.Teachers.Any(t => t.UserId == id)));

            if (studentTask == null) return false;
            return true;
        }
    }
}
