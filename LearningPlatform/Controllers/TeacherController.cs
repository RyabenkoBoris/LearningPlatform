using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TeacherController : Controller
    {
        private readonly IAdminUsersRepository _userRepository;

        public TeacherController(IAdminUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var teacherVM = await _userRepository.GetTeachers();
            return View(teacherVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserViewModel teacherVM = new UserViewModel
            {
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(teacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel teacherVM)
        {
            if (!ModelState.IsValid)
            {
                teacherVM.Departments = await _userRepository.GetDepartments();
                teacherVM.Faculties = await _userRepository.GetFaculties();
                return View(teacherVM);
            }
            await _userRepository.Register(teacherVM.Email, teacherVM.Password, teacherVM.Name, teacherVM.DepartmentId, Enum.Role.Teacher);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var teacher = await _userRepository.GetById(id);
            if (teacher == null) return View("Error");
            var teacherVM = new UserViewModel
            {
                Id = id,
                Email = teacher.Email,
                Name = teacher.Name,
                DepartmentId = teacher.DepartmentId,
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(teacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel teacherVM)
        {
            if (!ModelState.IsValid)
            {
                teacherVM.Departments = await _userRepository.GetDepartments();
                teacherVM.Faculties = await _userRepository.GetFaculties();
                return View(teacherVM);
            }
            await _userRepository.Update(teacherVM.Id, teacherVM.Email, teacherVM.Password, teacherVM.Name, teacherVM.DepartmentId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var teacher = await _userRepository.GetById(id);
            if (teacher == null) return View("Error");
            var teacherVM = new UserViewModel
            {
                Id = id,
                Email = teacher.Email,
                Name = teacher.Name,
                DepartmentId = teacher.DepartmentId,
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(teacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel teacherVM)
        {
            await _userRepository.DeleteTeacher(teacherVM.Id);
            return RedirectToAction("Index");
        }
    }
}
