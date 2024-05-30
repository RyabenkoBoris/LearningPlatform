using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly IAdminUsersRepository _userRepository;

        public AdminController(IAdminUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var adminVM = await _userRepository.GetAdmins();
            return View(adminVM);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserViewModel adminVM = new UserViewModel
            {
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(adminVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel adminVM)
        {
            if (!ModelState.IsValid)
            {
                adminVM.Departments = await _userRepository.GetDepartments();
                adminVM.Faculties = await _userRepository.GetFaculties();
                return View(adminVM);
            }
            await _userRepository.Register(adminVM.Email, adminVM.Password, adminVM.Name, adminVM.DepartmentId, Enum.Role.Admin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var admin = await _userRepository.GetById(id);
            if (admin == null) return View("Error");
            var adminVM = new UserViewModel
            {
                Id = id,
                Email = admin.Email,
                Name = admin.Name,
                DepartmentId = admin.DepartmentId,
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(adminVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel adminVM)
        {
            if (!ModelState.IsValid)
            {
                adminVM.Departments = await _userRepository.GetDepartments();
                adminVM.Faculties = await _userRepository.GetFaculties();
                return View(adminVM);
            }
            await _userRepository.Update(adminVM.Id, adminVM.Email, adminVM.Password, adminVM.Name, adminVM.DepartmentId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var admin = await _userRepository.GetById(id);
            if (admin == null) return View("Error");
            var adminVM = new UserViewModel
            {
                Id = id,
                Email = admin.Email,
                Name = admin.Name,
                DepartmentId = admin.DepartmentId,
                Departments = await _userRepository.GetDepartments(),
                Faculties = await _userRepository.GetFaculties(),
            };
            return View(adminVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel adminVM)
        {
            await _userRepository.DeleteTeacher(adminVM.Id);
            return RedirectToAction("Index");
        }
    }
}
