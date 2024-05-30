using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LearningDbContext _context;
        private readonly ITeachersRepository _teachersRepository;
        private readonly IJournalRepository _journalRepository;
        public DepartmentRepository(LearningDbContext context, ITeachersRepository teachersRepository, IJournalRepository journalRepository)
        {
            _context = context;
            _teachersRepository = teachersRepository;
            _journalRepository = journalRepository;

        }

        public async Task<List<DepartmentEntity>> Get()
        {
            return await _context.Departments
                .AsNoTracking()
                .Include(d => d.Faculty)
                .ToListAsync();
        }

        public async Task<List<FacultyEntity>> GetFaculties()
        {
            return await _context.Faculties
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<DepartmentEntity?> GetById(int id)
        {
            return await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<DepartmentEntity?> GetWithLessonsById(int id)
        {
            return await _context.Departments
                .AsNoTracking()
                .Include(d => d.Lessons)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<LessonEntity>> GetLessons(int id)
        {
            return await _context.Lessons
                .AsNoTracking()
                .Where(l => l.Departaments.Any(d => d.Id == id))
                .ToListAsync();
        }
        public async Task<List<LessonEntity>> GetUnavailableLessons(int id)
        {
            return await _context.Lessons
                .AsNoTracking()
                .Where(l => !l.Departaments.Any(d => d.Id == id))
                .ToListAsync();
        }
        public async Task Add(string name, string abbrevation, int facultyId)
        {
            var departmentEntity = new DepartmentEntity
            {
                Name = name,
                Abbreviation = abbrevation,
                FacultyId = facultyId
            };

            await _context.AddAsync(departmentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string name, string abbrevation, int facultyId)
        {
            await _context.Departments
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(d => d
                    .SetProperty(d => d.Name, name)
                    .SetProperty(d => d.Abbreviation, abbrevation)
                    .SetProperty(d => d.FacultyId, facultyId));
        }

        public async Task Delete(int id)
        {
            await _context.Users
                .Where(u => u.DepartmentId == id)
                .ExecuteDeleteAsync();
            await _context.Departments
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task AddLesson(int departamentId, int? lessonId)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            var departament = await _context.Departments.FirstOrDefaultAsync(t => t.Id == departamentId);
            departament.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            var depatramentGroups = await _context.Departments
                .AsNoTracking()
                .Include(d => d.Groups)
                .FirstOrDefaultAsync(d => d.Id == departamentId);

            foreach (var group in depatramentGroups.Groups)
            {
                await _journalRepository.Add(group.Id, lesson.Id);
            }

        }
        public async Task DeleteLesson(int departamentId, int? lessonId)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
            var departament = await _context.Departments.Include(t => t.Lessons).FirstOrDefaultAsync(t => t.Id == departamentId);
            departament.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            var depatramentUG = await _context.Departments
                .AsNoTracking()
                .Include(d => d.Users)
                .Include(d => d.Groups)
                .FirstOrDefaultAsync(d => d.Id == departamentId);

            foreach (var teacher in depatramentUG.Users)
            {
                await _teachersRepository.DeleteLesson((Guid)teacher.TeacherId, lessonId);
            }

            foreach (var group in depatramentUG.Groups)
            {
                await _journalRepository.Delete(group.Id, lesson.Id);
            }
        }
    }
}
