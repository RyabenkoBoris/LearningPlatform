using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class LessonController : Controller
    {
        private readonly ILessonsRepository _lessonsRepository;

        public LessonController(ILessonsRepository lessonsRepository)
        {
            _lessonsRepository = lessonsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var lessonsVM = await _lessonsRepository.Get();
            return View(lessonsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var lessonVM = new LessonViewModel();
            return View(lessonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LessonViewModel lessonVM)
        {
            if (!ModelState.IsValid)
            {
                return View(lessonVM);
            }
            await _lessonsRepository.Add(lessonVM.Name);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lesson = await _lessonsRepository.GetById(id);
            if (lesson == null) return View("Error");
            var lessonVM = new LessonViewModel
            {
                Id = id,
                Name = lesson.Name,
            };
            return View(lessonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LessonViewModel lessonVM)
        {
            if (!ModelState.IsValid)
            {
                return View(lessonVM);
            }
            await _lessonsRepository.Update(lessonVM.Id, lessonVM.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _lessonsRepository.GetById(id);
            if (lesson == null) return View("Error");
            var lessonVM = new LessonViewModel
            {
                Id = id,
                Name = lesson.Name
            };
            return View(lessonVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LessonViewModel lessonVM)
        {
            await _lessonsRepository.Delete(lessonVM.Id);
            return RedirectToAction("Index");
        }
    }
}
