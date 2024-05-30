using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var facultyVM = await _facultyRepository.Get();
            return View(facultyVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var facultyVM = new FacultyViewModel();
            return View(facultyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FacultyViewModel facultyVM)
        {
            if (!ModelState.IsValid)
            {
                return View(facultyVM);
            }
            await _facultyRepository.Add(facultyVM.Name, facultyVM.Abbreviation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _facultyRepository.GetById(id);
            if (faculty == null) return View("Error");
            var facultyVM = new FacultyViewModel
            {
                Id = id,
                Name = faculty.Name,
                Abbreviation = faculty.Abbreviation,
            };
            return View(facultyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FacultyViewModel facultyVM)
        {
            if (!ModelState.IsValid)
            {
                return View(facultyVM);
            }
            await _facultyRepository.Update(facultyVM.Id, facultyVM.Name, facultyVM.Abbreviation);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _facultyRepository.GetById(id);
            if (faculty == null) return View("Error");
            var facultyVM = new FacultyViewModel
            {
                Id = id,
                Name = faculty.Name,
                Abbreviation = faculty.Abbreviation,
            };
            return View(facultyVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FacultyViewModel facultyVM)
        {
            await _facultyRepository.Delete(facultyVM.Id);
            return RedirectToAction("Index");
        }
    }
}
