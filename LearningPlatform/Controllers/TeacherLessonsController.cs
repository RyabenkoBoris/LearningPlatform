using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TeacherLessonsController : Controller
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IClassesRepository _classesRepository;

        public TeacherLessonsController(ITeachersRepository teachersRepository, IClassesRepository classesRepository)
        {
            _teachersRepository = teachersRepository;
            _classesRepository = classesRepository;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var teacherLessonsVM = await _teachersRepository.GetByIdWithLessons(id);
            return View(teacherLessonsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Add(Guid id)
        {
            var teacher = await _teachersRepository.GetByIdWithLessons(id);
            var teacherLessonVM = new TeacherLessonsViewModel
            {
                Id = id,
                Name = teacher.User.Name,
                Lessons = await _teachersRepository.GetNonAvailableLessons(id),
            };
            return View(teacherLessonVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(TeacherLessonsViewModel teacherLessonVM)
        {
            if (!ModelState.IsValid)
            {
                var teacher = await _teachersRepository.GetByIdWithLessons(teacherLessonVM.Id);
                teacherLessonVM.Name = teacher.User.Name;
                return View(teacherLessonVM);
            }
            await _teachersRepository.AddLesson(teacherLessonVM.Id, teacherLessonVM.LessonId);
            return RedirectToAction("Index", new { id = teacherLessonVM.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, int lessonId)
        {
            var teacher = await _teachersRepository.GetByIdWithLessons(id);
            if (teacher == null) return View("Error");
            var teacherLessonVM = new TeacherLessonsViewModel
            {
                LessonId = lessonId,
                Name = teacher.User.Name,
                Lessons = await _teachersRepository.GetLessons(),
            };
            return View(teacherLessonVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TeacherLessonsViewModel teacherLessonVM, int lessonId)
        {
            await _teachersRepository.DeleteLesson(teacherLessonVM.Id, lessonId);
            return RedirectToAction("Index", new { id = teacherLessonVM.Id });
        }
    }
}
