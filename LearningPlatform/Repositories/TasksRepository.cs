using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly LearningDbContext _context;
        private readonly IFilesRepository _filesRepository;
        private readonly IStudentTasksRepository _studentTasksRepository;

        public TasksRepository(LearningDbContext context, IFilesRepository filesRepository
                             , IStudentTasksRepository studentTasksRepository)
        {
            _context = context;
            _filesRepository = filesRepository;
            _studentTasksRepository = studentTasksRepository;
        }
        public async Task<TaskEntity?> GetById(int id)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Include(t => t.User)
                .Include(t => t.FilePaths)
                .SingleOrDefaultAsync(t => t.Id == id);
        }
        public async Task<StudentTaskEntity?> GetStudentTaskById(int taskId, Guid id)
        {
            var studentTask = await _context.StudentTasks
                .AsNoTracking()
                .Include(s => s.FilePaths)
                .FirstOrDefaultAsync(s => s.TaskId == taskId && s.UserId == id);

            if (studentTask is null)
            {
                studentTask = new StudentTaskEntity
                {
                    TaskId = taskId,
                    UserId = id
                };
                await _context.AddAsync(studentTask);
                await _context.SaveChangesAsync();
            }

            return studentTask;
        }

        public async Task<CourseEntity?> GetCourseById(int id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            var section = await _context.Sections
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == task.SectionId);

            return await _context.Courses
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == section.CourseId);
        }
        public async Task Add(string name, short maxMark, int sectionId, DateTime duedate, string taskText, Guid userId)
        {

            var taskEntity = new TaskEntity
            {
                Name = name,
                MaxMark = maxMark,
                SectionId = sectionId,
                CreateDate = DateTime.UtcNow,
                TaskText = taskText,
                DueDate = duedate.ToUniversalTime(),
                UserId = userId
            };

            await _context.AddAsync(taskEntity);
            await _context.SaveChangesAsync();

            var section = await _context.Sections
                .AsNoTracking()
                .Include(s => s.Course)
                    .ThenInclude(c => c.Groups)
                .FirstOrDefaultAsync(s => s.Id == sectionId);

            foreach (var group in section.Course.Groups)
            {
                await AddStudentTaskForGroup(group.Id, taskEntity.Id);
            }
        }

        public async Task AddStudentTaskForGroup(int groupId, int taskId)
        {
            var group = await _context.Groups
                .AsNoTracking()
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            StudentTaskEntity studentTaskEntity;
            foreach (var user in group.Users)
            {
                studentTaskEntity = new StudentTaskEntity
                {
                    UserId = user.Id,
                    TaskId = taskId,
                };
                await _context.AddAsync(studentTaskEntity);
            }
            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, string name, DateTime duedate, string taskText)
        {
            await _context.Tasks
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(t => t.Name, name)
                    .SetProperty(t => t.TaskText, taskText)
                    .SetProperty(t => t.DueDate, duedate.ToUniversalTime()));
        }

        public async Task Delete(int id)
        {
            var task = await _context.Tasks
                .AsNoTracking()
                .Include(t => t.FilePaths)
                .Include(t => t.StudentTask)
                .FirstOrDefaultAsync(t => t.Id == id);

            foreach (var file in task.FilePaths)
            {
                await _filesRepository.Delete(file.Id);
            }
            foreach (var studentTask in task.StudentTask)
            {
                await _studentTasksRepository.Delete(studentTask.Id);
            }
            await _context.Tasks
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> HasPermissionToChange(Guid id, int taskId)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Tasks.Any(t => t.Id == taskId)) &&
                    (c.UserId == id || c.Teachers.Any(t => t.UserId == id)));
            if (course == null) return false;
            return true;
        }

        public async Task<bool> HasAccess(Guid id, int taskId)
        {
            if (await HasPermissionToChange(id, taskId)) return true;
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Tasks.Any(t => t.Id == taskId)) &&
                    (c.Groups.Any(g => g.Users.Any(u => u.Id == id))));
            if (course == null) return false;
            return true;
        }
    }
}
