using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly LearningDbContext _context;
        private readonly IJournalRepository _journalRepository;
        private readonly IAdminUsersRepository _adminUsersRepository;
        public GroupRepository(LearningDbContext context, IJournalRepository journalRepository, IAdminUsersRepository adminUsersRepository)
        {
            _context = context;
            _journalRepository = journalRepository;
            _adminUsersRepository = adminUsersRepository;
        }

        public async Task<List<GroupEntity>> Get()
        {
            return await _context.Groups
                .AsNoTracking()
                .Include(g => g.Department)
                    .ThenInclude(d => d.Faculty)
                .Include(g => g.Speciality)
                .ToListAsync();
        }

        public async Task<GroupEntity?> GetById(int id)
        {
            return await _context.Groups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<FacultyEntity>> GetFaculties()
        {
            return await _context.Faculties
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<DepartmentEntity>> GetDepartments()
        {
            return await _context.Departments
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<SpecialityEntity>> GetSpecialities()
        {
            return await _context.Specialities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Add(string name, int departmentId, int specialityId)
        {
            var groupEntity = new GroupEntity
            {
                Name = name,
                DepartmentId = departmentId,
                SpecialityId = specialityId
            };

            await _context.AddAsync(groupEntity);
            await _context.SaveChangesAsync();
            await _journalRepository.AddToGroup(groupEntity.Id);
        }

        public async Task Update(int id, string name, int specialityId)
        {
            await _context.Groups
                .Where(g => g.Id == id)
                .ExecuteUpdateAsync(g => g
                    .SetProperty(g => g.Name, name)
                    .SetProperty(g => g.SpecialityId, specialityId));
        }

        public async Task Delete(int id)
        {
            var students = await _context.Users
                .AsNoTracking()
                .Include(u => u.StudentTasks)
                .Where(u => u.GroupId == id)
                .ToListAsync();

            foreach (var student in students)
            {
                await _adminUsersRepository.DeleteStudent(student.Id);
            }

            await _context.Groups
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
