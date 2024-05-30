using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var departmentVM = await _departmentRepository.Get();
            return View(departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departmentVM = new DepartmentViewModel
            {
                Faculties = await _departmentRepository.GetFaculties(),
            };
            return View(departmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                departmentVM.Faculties = await _departmentRepository.GetFaculties();
                return View(departmentVM);
            }
            await _departmentRepository.Add(departmentVM.Name, departmentVM.Abbreviation, departmentVM.FacultyId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetById(id);
            if (department == null) return View("Error");
            var departmentVM = new DepartmentViewModel
            {
                Id = id,
                Name = department.Name,
                Abbreviation = department.Abbreviation,
                FacultyId = department.FacultyId,
                Faculties = await _departmentRepository.GetFaculties(),
            };
            return View(departmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                departmentVM.Faculties = await _departmentRepository.GetFaculties();
                return View(departmentVM);
            }
            await _departmentRepository.Update(departmentVM.Id, departmentVM.Name, departmentVM.Abbreviation, departmentVM.FacultyId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _departmentRepository.GetById(id);
            if (department == null) return View("Error");
            var departmentVM = new DepartmentViewModel
            {
                Id = id,
                Name = department.Name,
                Abbreviation = department.Abbreviation,
                FacultyId = department.FacultyId,
                Faculties = await _departmentRepository.GetFaculties()
            };
            return View(departmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            await _departmentRepository.Delete(departmentVM.Id);
            return RedirectToAction("Index");
        }
    }
}
