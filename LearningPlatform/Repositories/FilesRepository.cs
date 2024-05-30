using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private readonly LearningDbContext _context;
        private readonly IConfiguration Configuration;
        public FilesRepository(LearningDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<FilePathEntity?> GetById(int id)
        {
            return await _context.FilePaths
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Add(IFormFile file, int taskId)
        {
            var positionOptions = Configuration.GetSection(PathOptions.Position)
                                                     .Get<PathOptions>();
            var folderPath = positionOptions.Path;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var randomPath = Path.GetRandomFileName();
            var extension = Path.GetExtension(file.FileName);
            var filePath = new FilePathEntity
            {
                Name = file.FileName,
                Path = randomPath + extension,
                TaskId = taskId,
            };

            using (var stream = File.Create(folderPath + randomPath + extension))
            {
                await file.CopyToAsync(stream);
            }

            await _context.AddAsync(filePath);
            await _context.SaveChangesAsync();
        }
        public async Task AddStudentFile(IFormFile file, int studentTaskId)
        {
            var positionOptions = Configuration.GetSection(PathOptions.Position)
                                                     .Get<PathOptions>();
            var folderPath = positionOptions.Path;
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var randomPath = Path.GetRandomFileName();
            var extension = Path.GetExtension(file.FileName);
            var filePath = new FilePathEntity
            {
                Name = file.FileName,
                Path = randomPath + extension,
                StudentTaskId = studentTaskId,
            };

            using (var stream = File.Create(folderPath + randomPath + extension))
            {
                await file.CopyToAsync(stream);
            }

            await _context.AddAsync(filePath);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var positionOptions = Configuration.GetSection(PathOptions.Position)
                                                     .Get<PathOptions>();
            var folderPath = positionOptions.Path;

            var filePath = await _context.FilePaths
                .FirstOrDefaultAsync(f => f.Id == id);

            var fullPath = folderPath + filePath.Path;


            if (File.Exists(fullPath))
                File.Delete(fullPath);

            await _context.FilePaths
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> HasPermissionToDownload(Guid id, int fileId)
        {
            var file = await _context.FilePaths
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null) return false;

            if (file.TaskId != null)
            {
                var course = await _context.Courses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Tasks.Any(t => t.FilePaths.Any(f => f.Id == fileId))) &&
                        (c.UserId == id || c.Teachers.Any(t => t.UserId == id) || c.Groups.Any(g => g.Users.Any(u => u.Id == id))));
                if (course == null) return false;
                return true;
            }
            else if (file.StudentTaskId != null)
            {
                var course = await _context.Courses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Tasks.Any(t => t.StudentTask
                        .Any(s => s.FilePaths.Any(f => f.Id == fileId) &&
                        (s.UserId == id || c.UserId == id || c.Teachers.Any(t => t.UserId == id))))));
                if (course == null) return false;
                return true;
            }
            return false;
        }

        public async Task<bool> HasPermissionToDelete(Guid id, int fileId)
        {
            var file = await _context.FilePaths
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null) return false;

            if (file.TaskId != null)
            {
                var course = await _context.Courses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Tasks.Any(t => t.FilePaths.Any(f => f.Id == fileId))) &&
                        (c.UserId == id || c.Teachers.Any(t => t.UserId == id)));
                if (course == null) return false;
                return true;
            }
            else if (file.StudentTaskId != null)
            {
                var studentTask = await _context.StudentTasks
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.FilePaths.Any(f => f.Id == fileId) && s.UserId == id);
                if (studentTask == null) return false;
                return true;
            }
            return false;
        }
    }
}
