using LearningPlatform.Interfaces;
using LearningPlatform.Models;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class JournalController : Controller
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IAssesmentTypesRepository _assesmentTypesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IGradesRepository _gradesRepository;

        public JournalController(IJournalRepository journalRepository, IAssesmentTypesRepository assesmentTypesRepository
                                , IUsersRepository usersRepository, IGradesRepository gradesRepository)
        {
            _journalRepository = journalRepository;
            _assesmentTypesRepository = assesmentTypesRepository;
            _usersRepository = usersRepository;
            _gradesRepository = gradesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<JournalEntity> journals = [];
            if (role == Enum.Role.Teacher.ToString() || role == Enum.Role.Admin.ToString())
            {
                journals = await _journalRepository.GetTeacherJournals(Guid.Parse(id));
            }
            else if (role == Enum.Role.Student.ToString())
            {
                journals = await _journalRepository.GetByUserId(Guid.Parse(id));
            }
            return View(journals);
        }
        [Authorize(Policy = "JournalTeacherAccess")]
        public async Task<IActionResult> Students(int id)
        {
            var journal = await _journalRepository.GetWithUser(id);
            var journalVM = new JournalStudentsViewModel
            {
                JournalId = id,
                Students = journal.Group.Users,
                GroupName = journal.Group.Name,
            };
            return View(journalVM);
        }
        [Authorize(Policy = "JournalTeacherAccess")]
        public async Task<IActionResult> Student(int id, Guid userId)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await _usersRepository.GetStudentById(userId);
            var teacher = await _usersRepository.GetTeacherById(Guid.Parse(teacherId));
            var grades = await _gradesRepository.GetUserGrades(userId, id);
            int sum = 0;
            foreach (var grade in grades)
            {
                sum += grade.Mark;
            }
            var studentJounalVM = new StudentJournalViewModel
            {
                TeacherId = teacher.TeacherId,
                StudentId = userId,
                JournalId = id,
                Grades = await _gradesRepository.GetUserGrades(userId, id),
                AssesmentTypes = await _assesmentTypesRepository.Get(),
                Sum = sum,
                StudentName = student.Name,
            };
            return View(studentJounalVM);
        }
        [Authorize(Policy = "JournalStudentAccess")]
        public async Task<IActionResult> Lesson(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var grades = await _gradesRepository.GetUserGrades(Guid.Parse(userId), id);
            return View(grades);
        }

        [HttpPost]
        [Authorize(Policy = "Rate")]
        public async Task<IActionResult> Add(StudentJournalViewModel studentJounalVM)
        {
            if (!ModelState.IsValid)
            {
                var grades = await _gradesRepository.GetUserGrades(studentJounalVM.StudentId, studentJounalVM.JournalId);
                int sum = 0;
                foreach (var grade in grades)
                {
                    sum += grade.Mark;
                }
                studentJounalVM.Grades = grades;
                studentJounalVM.AssesmentTypes = await _assesmentTypesRepository.Get();
                studentJounalVM.Sum = sum;
                return RedirectToAction("Student", new { id = studentJounalVM.JournalId, userId = studentJounalVM.StudentId });
            }
            await _gradesRepository.Add(studentJounalVM.Mark, studentJounalVM.Note, studentJounalVM.StudentId, studentJounalVM.JournalId
                                       , (Guid)studentJounalVM.TeacherId, (int)studentJounalVM.AssesmentTypeId);
            return RedirectToAction("Student", new { id = studentJounalVM.JournalId, userId = studentJounalVM.StudentId });
        }
        [HttpGet]
        [Authorize(Policy = "Grade")]
        public async Task<IActionResult> Delete(int id, Guid studentId, int journalId)
        {
            await _gradesRepository.Delete(id);
            return RedirectToAction("Student", new { id = journalId, userId = studentId });
        }
    }
}
