using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class SectionsRepository : ISectionsRepository
    {
        private readonly LearningDbContext _context;
        private readonly ITasksRepository _tasksRepository;

        public SectionsRepository(LearningDbContext context, ITasksRepository tasksRepository)
        {
            _context = context;
            _tasksRepository = tasksRepository;
        }

        public async Task<SectionEntity?> GetById(int id)
        {
            return await _context.Sections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<CourseEntity?> GetCourseById(int id)
        {
            var section = await _context.Sections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == section.CourseId);
        }
        public async Task Add(string name, int courseId)
        {
            var sectionEntity = new SectionEntity
            {
                Name = name,
                CourseId = courseId
            };

            await _context.AddAsync(sectionEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            await _context.Sections
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Name, name));
        }

        public async Task Delete(int id)
        {
            var section = await _context.Sections
                .AsNoTracking()
                .Include(s => s.Tasks)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach (var task in section.Tasks)
            {
                await _tasksRepository.Delete(task.Id);
            }

            await _context.Sections
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> HasPermissionToChange(Guid id, int sectionId)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Sections.Any(s => s.Id == sectionId) &&
                    (c.UserId == id || c.Teachers.Any(t => t.UserId == id)));
            if (course == null) return false;
            return true;
        }
    }
}
