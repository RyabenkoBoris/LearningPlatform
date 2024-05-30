using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly LearningDbContext _context;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly ITasksRepository _tasksRepository;
        private readonly IStudentTasksRepository _studentTasksRepository;

        public CoursesRepository(LearningDbContext context, ISectionsRepository sectionsRepository
                                , ITasksRepository tasksRepository, IStudentTasksRepository studentTasksRepository)
        {
            _context = context;
            _sectionsRepository = sectionsRepository;
            _tasksRepository = tasksRepository;
            _studentTasksRepository = studentTasksRepository;
        }
        public async Task<string> GetTeacherNameById(Guid id)
        {
            var teacher = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.TeacherId == id);
            return teacher.Name;
        }
        public async Task<string> GetGroupNameById(int id)
        {
            var group = await _context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
            return group.Name;
        }
        public async Task<List<CourseEntity>> GetTeacherAvailableCourses(Guid id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.UserId == id || c.Teachers.Any(t => t.UserId == id))
                .ToListAsync();
        }

        public async Task<List<CourseEntity>> GetStudentAvailableCourses(Guid id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.User)
                .Where(c => c.Groups.Any(g => g.Users.Any(u => u.Id == id)))
                .ToListAsync();
        }
        public async Task<CourseEntity?> GetById(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<CourseEntity?> GetByIdWithSectionsTasks(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Tasks)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<CourseEntity?> GetByIdWithTeachers(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Teachers)
                    .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<CourseEntity?> GetByIdWithGroups(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .Include(c => c.User)
                .Include(c => c.Groups)
                    .ThenInclude(g => g.Users)
                .AsSplitQuery()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<UserEntity>> GetNonAvailableTeachers(Guid id, int courseId)
        {
            var courseEntity = await _context.Courses
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == courseId);

            var teachers = await _context.Teachers
                                .AsNoTracking()
                                .Where(t => !t.Courses.Contains(courseEntity))
                                .Where(t => t.UserId != id)
                                .ToListAsync();

            return await _context.Users
                .AsNoTracking()
                .Where(u => teachers.Contains(u.Teacher))
                .ToListAsync();
        }

        public async Task<List<GroupEntity>> GetNonAvailableGroups(int courseId)
        {
            var courseEntity = await _context.Courses
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == courseId);
            return await _context.Groups
                .Where(g => !g.Courses.Contains(courseEntity))
                    .Include(g => g.Users)
                .ToListAsync();
        }
        public async Task Add(string name, Guid userId)
        {
            var courseEntity = new CourseEntity
            {
                Name = name,
                UserId = userId
            };

            await _context.AddAsync(courseEntity);
            await _context.SaveChangesAsync();
            await AddSection(string.Empty, courseEntity.Id);
        }
        public async Task Update(int id, string name)
        {
            await _context.Courses
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(c => c
                    .SetProperty(c => c.Name, name));
        }
        public async Task Delete(int id)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .Include(c => c.Sections)
                .FirstOrDefaultAsync(c => c.Id == id);

            foreach (var section in course.Sections)
            {
                await _sectionsRepository.Delete(section.Id);
            }

            await _context.Courses
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }
        public async Task AddSection(string name, int courseId)
        {
            var sectionEntity = new SectionEntity
            {
                Name = name,
                CourseId = courseId
            };
            await _context.AddAsync(sectionEntity);
            await _context.SaveChangesAsync();
        }
        public async Task AddTeacher(Guid? id, int? courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(l => l.Id == courseId);
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            teacher.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTeacher(Guid? id, int? courseId)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(l => l.Id == courseId);
            var teacher = await _context.Teachers.Include(t => t.Courses).FirstOrDefaultAsync(t => t.Id == id);
            teacher.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        public async Task AddGroup(int? groupId, int? courseId)
        {
            var course = await _context.Courses
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Tasks)
                .FirstOrDefaultAsync(l => l.Id == courseId);

            var group = await _context.Groups
                .FirstOrDefaultAsync(t => t.Id == groupId);

            group.Courses.Add(course);


            foreach (var section in course.Sections)
            {
                foreach (var task in section.Tasks)
                {
                    await _tasksRepository.AddStudentTaskForGroup(group.Id, task.Id);
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteGroup(int? groupId, int? courseId)
        {
            var group = await _context.Groups
                .Include(g => g.Users)
                    .ThenInclude(u => u.StudentTasks)
                .AsSplitQuery()
                .FirstOrDefaultAsync(t => t.Id == groupId);

            foreach (var student in group.Users)
            {
                foreach (var studentTask in student.StudentTasks)
                {
                    await _studentTasksRepository.Delete(studentTask.Id);
                }
            }

            var course = await _context.Courses.FirstOrDefaultAsync(l => l.Id == courseId);
            group = await _context.Groups.Include(g => g.Courses).FirstOrDefaultAsync(t => t.Id == groupId);
            group.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsGroupOwner(Guid id, int groupId)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.UserId == id && g.Id == groupId);
            if (course == null) return false;
            return true;
        }
        public async Task<bool> IsOneOfTeacher(Guid id, int groupId)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Teachers.Any(t => t.UserId == id) && g.Id == groupId);
            if (course == null) return false;
            return true;
        }
        public async Task<bool> IsStudentInGroup(Guid id, int groupId)
        {
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Groups.Any(g => g.Users.Any(u => u.Id == id)) && g.Id == groupId);
            if (course == null) return false;
            return true;
        }
    }
}
