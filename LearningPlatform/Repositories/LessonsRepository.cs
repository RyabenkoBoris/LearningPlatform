using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private readonly LearningDbContext _context;
        public LessonsRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<LessonEntity>> Get()
        {
            return await _context.Lessons
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LessonEntity?> GetById(int id)
        {
            return await _context.Lessons
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Add(string name)
        {
            var lessonEntity = new LessonEntity
            {
                Name = name,
            };

            await _context.AddAsync(lessonEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string name)
        {
            await _context.Lessons
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Name, name));
        }

        public async Task Delete(int id)
        {
            await _context.Lessons
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
