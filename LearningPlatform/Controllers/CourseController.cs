using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        public CourseController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        public async Task<IActionResult> Index()
        {
            CourseViewModel courseVM = new CourseViewModel();
            var role = User.FindFirstValue(ClaimTypes.Role);
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (role == Enum.Role.Teacher.ToString() || role == Enum.Role.Admin.ToString())
            {
                courseVM.Courses = await _coursesRepository.GetTeacherAvailableCourses(Guid.Parse(id));
            }
            else if (role == Enum.Role.Student.ToString())
            {
                courseVM.Courses = await _coursesRepository.GetStudentAvailableCourses(Guid.Parse(id));
            }
            return View(courseVM);
        }
        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public IActionResult Create()
        {
            var courseVM = new CourseViewModel
            {
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
            return View(courseVM);
        }
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Create(CourseViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }
            await _coursesRepository.Add(courseVM.Name, courseVM.UserId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _coursesRepository.GetById(id);
            var courseVM = new CourseViewModel
            {
                CourseId = id,
                Name = course.Name,
            };
            return View(courseVM);
        }
        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> Edit(CourseViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }
            await _coursesRepository.Update(courseVM.CourseId, courseVM.Name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _coursesRepository.GetById(id);
            var courseVM = new CourseViewModel
            {
                CourseId = id,
                Name = course.Name,
            };
            return View(courseVM);
        }

        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> Delete(CourseViewModel courseVM)
        {
            await _coursesRepository.Delete(courseVM.CourseId);
            return RedirectToAction("Index");
        }
    }
}
