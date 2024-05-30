using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class SpecialitiesRepository : ISpecialitiesRepository
    {
        private readonly LearningDbContext _context;
        public SpecialitiesRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialityEntity>> Get()
        {
            return await _context.Specialities
                .AsNoTracking()
                .OrderBy(s => s.Code)
                .ToListAsync();
        }

        public async Task<SpecialityEntity?> GetById(int id)
        {
            return await _context.Specialities
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SpecialityEntity>> GetByPage(int page, int pageSize)
        {
            return await _context.Specialities
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Add(int code, string name)
        {
            var specialityEntity = new SpecialityEntity
            {
                Name = name,
                Code = code
            };

            await _context.AddAsync(specialityEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, int code, string name)
        {
            await _context.Specialities
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Code, code)
                    .SetProperty(s => s.Name, name));
        }

        public async Task Delete(int id)
        {
            await _context.Specialities
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
