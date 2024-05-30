using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LearningDbContext _context;
        public UsersRepository(LearningDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetTeacherById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Teacher)
                    .ThenInclude(g => g.Classes)
                        .ThenInclude(c => c.Lesson)
                .Include(u => u.Teacher)
                    .ThenInclude(g => g.Classes)
                        .ThenInclude(c => c.Groups)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity?> GetStudentById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Group)
                    .ThenInclude(g => g.Classes)
                        .ThenInclude(c => c.Lesson)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
