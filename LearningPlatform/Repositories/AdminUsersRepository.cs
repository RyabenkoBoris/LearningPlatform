using LearningPlatform.Authentication;
using LearningPlatform.Enum;
using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class AdminUsersRepository : IAdminUsersRepository
    {
        private readonly LearningDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICoursesRepository _coursesRepository;
        private readonly IStudentTasksRepository _studentTasksRepository;
        public AdminUsersRepository(LearningDbContext context, ICoursesRepository coursesRepository, IStudentTasksRepository studentTasksRepository)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
            _coursesRepository = coursesRepository;
            _studentTasksRepository = studentTasksRepository;
        }
        public async Task<List<UserEntity>> GetAdmins()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(a => a.Role == Role.Admin)
                .Include(a => a.Department)
                    .ThenInclude(d => d.Faculty)
                .ToListAsync();
        }
        public async Task<List<UserEntity>> GetTeachers()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == Role.Teacher)
                .Include(a => a.Department)
                    .ThenInclude(d => d.Faculty)
                .ToListAsync();
        }
        public async Task<List<UserEntity>> GetStudents()
        {
            return await _context.Users
                .AsNoTracking()
                .Where(u => u.Role == Role.Student)
                .Include(a => a.Group)
                    .ThenInclude(g => g.Department)
                        .ThenInclude(d => d.Faculty)
                .ToListAsync();
        }
        public async Task<List<DepartmentEntity>> GetDepartments()
        {
            return await _context.Departments
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<FacultyEntity>> GetFaculties()
        {
            return await _context.Faculties
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<GroupEntity>> GetGroups()
        {
            return await _context.Groups
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<UserEntity?> GetById(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<List<UserEntity>> GetByNameOrFacultyOrGroup(string search)
        {
            return await _context.Users
                .AsNoTracking()
                .Where(s => s.Name.Contains(search) || s.Group.Name.Contains(search))
                .Include(a => a.Group)
                    .ThenInclude(g => g.Department)
                        .ThenInclude(d => d.Faculty)
                .ToListAsync();
        }
        public async Task Register(string email, string password, string name, int? departmentId, Role role)
        {
            Guid id = Guid.NewGuid();

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) return;

            var admin = new UserEntity
            {
                Id = id,
                Email = email,
                PasswordHash = _passwordHasher.Generate(password),
                Role = role,
                Name = name,
                DepartmentId = departmentId,
                Teacher = new TeacherEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = id,
                }
            };
            await _context.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterStudent(string email, string password, string name, int? groupId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) return;

            var student = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _passwordHasher.Generate(password),
                Role = Role.Student,
                Name = name,
                GroupId = groupId,
            };
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Guid id, string email, string password, string name, int? departmentId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) return;

            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(u => u.Email, email)
                    .SetProperty(u => u.PasswordHash, _passwordHasher.Generate(password))
                    .SetProperty(u => u.Name, name)
                    .SetProperty(u => u.DepartmentId, departmentId));
        }
        public async Task UpdateStudent(Guid id, string email, string password, string name, int? groupId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null) return;

            await _context.Users
                .Where(s => s.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(s => s.Email, email)
                    .SetProperty(s => s.PasswordHash, _passwordHasher.Generate(password))
                    .SetProperty(s => s.Name, name)
                    .SetProperty(s => s.GroupId, groupId));
        }
        public async Task DeleteTeacher(Guid id)
        {
            var teacher = await _context.Users
                .AsNoTracking()
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(u => u.Id == id);

            foreach (var course in teacher.Courses)
            {
                await _coursesRepository.Delete(course.Id);
            }

            await _context.Users
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteStudent(Guid id)
        {
            var student = await _context.Users
                .AsNoTracking()
                .Include(u => u.StudentTasks)
                .FirstOrDefaultAsync(d => d.Id == id);

            foreach (var studentTask in student.StudentTasks)
            {
                await _studentTasksRepository.Delete(studentTask.Id);
            }

            await _context.Users
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
