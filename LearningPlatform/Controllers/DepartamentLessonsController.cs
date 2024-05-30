using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DepartamentLessonsController : Controller
    {
        private readonly IDepartmentRepository _departamentRepository;

        public DepartamentLessonsController(IDepartmentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var departament = await _departamentRepository.GetWithLessonsById(id);
            return View(departament);
        }
        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var departament = await _departamentRepository.GetById(id);
            var departamentVM = new DepartamentLessonsViewModel
            {
                DepartamentId = id,
                DepartamentName = departament.Name,
                Lessons = await _departamentRepository.GetUnavailableLessons(id),
            };
            return View(departamentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(DepartamentLessonsViewModel departamentVM)
        {
            if (!ModelState.IsValid)
            {
                departamentVM.Lessons = await _departamentRepository.GetUnavailableLessons(departamentVM.DepartamentId);
                View(departamentVM);
            }
            await _departamentRepository.AddLesson(departamentVM.DepartamentId, departamentVM.LessonId);
            return RedirectToAction("Index", new { id = departamentVM.DepartamentId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, int lessonId)
        {
            var departament = await _departamentRepository.GetWithLessonsById(id);
            var departamentVM = new DepartamentLessonsViewModel
            {
                DepartamentId = id,
                DepartamentName = departament.Name,
                LessonId = lessonId,
                Lessons = departament.Lessons,
            };
            return View(departamentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartamentLessonsViewModel departamentVM)
        {
            await _departamentRepository.DeleteLesson(departamentVM.DepartamentId, departamentVM.LessonId);
            return RedirectToAction("Index", new { id = departamentVM.DepartamentId });
        }
    }
}
