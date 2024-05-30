using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class StudentController : Controller
    {
        private readonly IAdminUsersRepository _userRepository;

        public StudentController(IAdminUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var studentVM = await _userRepository.GetStudents();
            return View(studentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StudentViewModel studentVM = new StudentViewModel
            {
                Departments = await _userRepository.GetDepartments(),
                Groups = await _userRepository.GetGroups(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(studentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentVM)
        {
            if (!ModelState.IsValid)
            {
                studentVM.Departments = await _userRepository.GetDepartments();
                studentVM.Groups = await _userRepository.GetGroups();
                studentVM.Faculties = await _userRepository.GetFaculties();
                return View(studentVM);
            }
            await _userRepository.RegisterStudent(studentVM.Email, studentVM.Password, studentVM.Name, studentVM.GroupId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _userRepository.GetById(id);
            if (student == null) return View("Error");
            var studentVM = new StudentViewModel
            {
                Id = id,
                Email = student.Email,
                Name = student.Name,
                GroupId = student.GroupId,
                Departments = await _userRepository.GetDepartments(),
                Groups = await _userRepository.GetGroups(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(studentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel studentVM)
        {
            if (!ModelState.IsValid)
            {
                studentVM.Departments = await _userRepository.GetDepartments();
                studentVM.Groups = await _userRepository.GetGroups();
                studentVM.Faculties = await _userRepository.GetFaculties();
                return View(studentVM);
            }
            await _userRepository.UpdateStudent(studentVM.Id, studentVM.Email, studentVM.Password, studentVM.Name, studentVM.GroupId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _userRepository.GetById(id);
            if (student == null) return View("Error");
            var studentVM = new StudentViewModel
            {
                Id = id,
                Email = student.Email,
                Name = student.Name,
                GroupId = student.GroupId,
                Departments = await _userRepository.GetDepartments(),
                Groups = await _userRepository.GetGroups(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(studentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(StudentViewModel studentVM)
        {
            await _userRepository.DeleteStudent(studentVM.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Find(string search)
        {
            var students = await _userRepository.GetByNameOrFacultyOrGroup(search);
            return View(students);
        }
    }
}
