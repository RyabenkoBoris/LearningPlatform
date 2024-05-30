using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly LearningDbContext _context;
        private readonly IClassesRepository _classesRepository;
        public TeachersRepository(LearningDbContext context, IClassesRepository classesRepository)
        {
            _context = context;
            _classesRepository = classesRepository;
        }
        public async Task<TeacherEntity?> GetByIdWithLessons(Guid id)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Include(t => t.Lessons)
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TeacherEntity?> GetByIdWithGroups(Guid id)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Include(t => t.Groups)
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TeacherEntity?> GetByUserId(Guid id)
        {
            return await _context.Teachers
                .AsNoTracking()
                .Include(t => t.Lessons)
                .Include(t => t.User)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<Guid> GetUserIdById(Guid id)
        {
            var teacher = await _context.Teachers
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);
            return teacher.UserId;
        }

        public async Task<List<LessonEntity>> GetLessons()
        {
            return await _context.Lessons
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<LessonEntity>> GetNonAvailableLessons(Guid id)
        {
            var teacher = await _context.Teachers
                .AsNoTracking()
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            return await _context.Lessons
                .AsNoTracking()
                .Where(l => !l.Teachers.Any(t => t.Id == id))
                .Where(l => l.Departaments.Any(d => d.Id == teacher.User.DepartmentId))
                .ToListAsync();
        }
        public async Task<List<LessonEntity>> GetAvailableLessons(Guid id)
        {
            return await _context.Lessons
                .AsNoTracking()
                .Where(l => l.Teachers.Any(t => t.Id == id))
                .ToListAsync();
        }

        public async Task<List<GroupEntity>> GetGroups()
        {
            return await _context.Groups
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<GroupEntity>> GetNonAvailableGroups(Guid id)
        {
            var teacher = await _context.Teachers
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Groups
                .AsNoTracking()
                .Where(l => !teacher.Groups.Contains(l))
                .ToListAsync();
        }
        public async Task<List<GroupEntity>> GetAvailableGroups(Guid id)
        {
            var teacher = await _context.Teachers
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Groups
                .AsNoTracking()
                .Where(l => teacher.Groups.Contains(l))
                .ToListAsync();
        }

        public async Task AddLesson(Guid id, int? lessonId)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            teacher.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLesson(Guid id, int? lessonId)
        {
            await _classesRepository.DeleteAllByLessonTeacher(lessonId, id);
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            var teacher = await _context.Teachers.Include(t => t.Lessons).FirstOrDefaultAsync(t => t.Id == id);
            teacher.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task AddGroup(Guid id, int? groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(l => l.Id == groupId);
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            teacher.Groups.Add(group);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGroup(Guid id, int? groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(l => l.Id == groupId);
            var teacher = await _context.Teachers.Include(t => t.Lessons).FirstOrDefaultAsync(t => t.Id == id);
            teacher.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }
}
