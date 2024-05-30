using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly LearningDbContext _context;

        public ClassesRepository(LearningDbContext context)
        {
            _context = context;
        }
        public async Task<List<ClassEntity>> Get(Guid id)
        {
            return await _context.Classes
                .AsNoTracking()
                .Where(c => c.TeacherId == id)
                .Include(c => c.Lesson)
                .Include(c => c.Groups)
                .ToListAsync();
        }
        public async Task<ClassEntity?> GetById(int id)
        {
            return await _context.Classes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<List<GroupEntity>> GetGroups()
        {
            return await _context.Groups
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<GroupEntity>> GetNonAvailableGroups(Guid TeacherId, int id)
        {
            var teacher = await _context.Teachers
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == TeacherId);

            var classEntity = await _context.Classes
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Groups
                .AsNoTracking()
                .Where(g => !classEntity.Groups.Contains(g) && teacher.Groups.Contains(g))
                .ToListAsync();
        }
        public async Task<List<GroupEntity>> GetAvailableGroups(Guid TeacherId, int id)
        {
            var teacher = await _context.Teachers
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == TeacherId);

            var classEntity = await _context.Classes
                                .AsNoTracking()
                                .Include(t => t.Groups)
                                .FirstOrDefaultAsync(x => x.Id == id);

            return await _context.Groups
                .AsNoTracking()
                .Where(g => classEntity.Groups.Contains(g) && teacher.Groups.Contains(g))
                .ToListAsync();
        }
        public async Task<List<ClassTypeEntity>> GetClassTypes()
        {
            return await _context.ClassTypes
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<string>> GetSheduleSampleList(int id)
        {
            var sheduleSample = await _context.SheduleSamples
                                .AsNoTracking()
                                .FirstOrDefaultAsync(s => s.Id == id);
            return [sheduleSample.LessonTime1, sheduleSample.LessonTime2, sheduleSample.LessonTime3,
                    sheduleSample.LessonTime4, sheduleSample.LessonTime5, sheduleSample.LessonTime6];
        }
        public async Task<Guid> GetTeacherIdById(int id)
        {
            var classEntity = await _context.Classes
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == id);
            return classEntity.TeacherId;
        }
        public async Task Add(short lessonNumber, bool isFirstWeek, DayOfWeek dayOfWeek
                             , int? lessonId, Guid teacherId, int classTypeId)
        {
            var classEntity = new ClassEntity
            {
                LessonNumber = lessonNumber,
                isFirstWeek = isFirstWeek,
                DayOfWeek = dayOfWeek,
                LessonId = (int)lessonId,
                TeacherId = teacherId,
                SheduleSampleId = 1,
                ClassTypeId = classTypeId
            };
            await _context.AddAsync(classEntity);
            await _context.SaveChangesAsync();
        }
        public async Task AddGroup(int classId, int? groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(l => l.Id == groupId);
            var classEntity = await _context.Classes.FirstOrDefaultAsync(t => t.Id == classId);
            classEntity.Groups.Add(group);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGroup(int classId, int? groupId)
        {

            var group = await _context.Groups.FirstOrDefaultAsync(l => l.Id == groupId);
            var classEntity = await _context.Classes.Include(c => c.Groups).FirstOrDefaultAsync(t => t.Id == classId);
            classEntity.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
        public async Task Update(int classId, int? lessonId, int classTypeId)
        {
            await _context.Classes
                .Where(c => c.Id == classId)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(c => c.LessonId, lessonId)
                    .SetProperty(c => c.ClassTypeId, classTypeId));
        }
        public async Task Delete(int id)
        {
            await _context.Classes
                .Where(s => s.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteAllByLessonTeacher(int? lessonId, Guid teacherId)
        {
            await _context.Classes
                .Where(s => s.TeacherId == teacherId && s.LessonId == lessonId)
                .ExecuteDeleteAsync();
        }
    }
}
