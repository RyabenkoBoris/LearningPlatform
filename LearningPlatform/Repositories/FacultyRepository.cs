using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly LearningDbContext _context;
        public FacultyRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<List<FacultyEntity>> Get()
        {
            return await _context.Faculties
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<FacultyEntity?> GetById(int id)
        {
            return await _context.Faculties
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task Add(string name, string abbrevation)
        {
            var facultyEntity = new FacultyEntity
            {
                Name = name,
                Abbreviation = abbrevation
            };

            await _context.AddAsync(facultyEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string name, string abbrevation)
        {
            await _context.Faculties
                .Where(f => f.Id == id)
                .ExecuteUpdateAsync(f => f
                    .SetProperty(f => f.Name, name)
                    .SetProperty(f => f.Abbreviation, abbrevation));
        }

        public async Task Delete(int id)
        {
            await _context.Faculties
                .Where(f => f.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
