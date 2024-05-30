using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LearningPlatform.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ISectionsRepository _sectionsRepository;
        private readonly ITasksRepository _tasksRepository;

        public DetailsController(ICoursesRepository coursesRepository, ISectionsRepository sectionsRepository
                                , ITasksRepository tasksRepository)
        {
            _coursesRepository = coursesRepository;
            _sectionsRepository = sectionsRepository;
            _tasksRepository = tasksRepository;
        }
        [Authorize(Policy = "MemberCourse")]
        public async Task<IActionResult> Index(int id)
        {
            var course = await _coursesRepository.GetByIdWithSectionsTasks(id);
            if (course is null) return RedirectToAction("Index", "Course");
            var courseVM = new DetailsViewModel
            {
                CourseId = id,
                CourseName = course.Name,
                Sections = course.Sections,
            };
            return View(courseVM);
        }
        [HttpGet]
        [Authorize(Policy = "TeacherCourse")]
        public async Task<IActionResult> CreateSection(int id)
        {
            var course = await _coursesRepository.GetById(id);
            if (course is null) return RedirectToAction("Index", "Course");
            var sectionVM = new SectionViewModel
            {
                CourseId = id,
            };
            return View(sectionVM);
        }
        [HttpPost]
        [Authorize(Policy = "TeacherCourse")]
        public async Task<IActionResult> CreateSection(SectionViewModel sectionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sectionVM);
            }
            await _sectionsRepository.Add(sectionVM.Name, sectionVM.CourseId);
            return RedirectToAction("Index", new { id = sectionVM.CourseId });
        }
        [HttpGet]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> EditSection(int id)
        {
            var section = await _sectionsRepository.GetById(id);
            if (section is null) return RedirectToAction("Index", "Course");
            var course = await _sectionsRepository.GetCourseById(id);
            var sectionVM = new SectionViewModel
            {
                Id = id,
                Name = section.Name,
                CourseId = course.Id,
            };
            return View(sectionVM);
        }
        [HttpPost]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> EditSection(SectionViewModel sectionVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sectionVM);
            }
            await _sectionsRepository.Update(sectionVM.Id, sectionVM.Name);
            return RedirectToAction("Index", new { id = sectionVM.CourseId });
        }
        [HttpGet]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _sectionsRepository.GetById(id);
            if (section is null) return RedirectToAction("Index", "Course");
            var course = await _sectionsRepository.GetCourseById(id);
            if (section == null || section.Name == string.Empty) return RedirectToAction("Index", new { id = course.Id });
            var sectionVM = new SectionViewModel
            {
                Id = id,
                Name = section.Name,
                CourseId = course.Id,
            };
            return View(sectionVM);
        }
        [HttpPost]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> DeleteSection(SectionViewModel sectionVM)
        {
            await _sectionsRepository.Delete(sectionVM.Id);
            return RedirectToAction("Index", new { id = sectionVM.CourseId });
        }

        [HttpGet]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> CreateTask(int id)
        {
            var course = await _sectionsRepository.GetCourseById(id);
            if (course is null) return RedirectToAction("Index", "Course");
            var taskVM = new TaskViewModel
            {
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                DueTime = TimeOnly.FromDateTime(DateTime.Now),
                SectionId = id,
                CourseId = course.Id,
            };
            return View(taskVM);
        }
        [HttpPost]
        [Authorize(Policy = "SectionManipulation")]
        public async Task<IActionResult> CreateTask(TaskViewModel taskVM)
        {
            if (!ModelState.IsValid)
            {
                return View(taskVM);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var date = taskVM.DueDate.ToDateTime(taskVM.DueTime);
            await _tasksRepository.Add(taskVM.Name, taskVM.MaxMark, taskVM.SectionId, date, taskVM.TaskText, Guid.Parse(userId));
            return RedirectToAction("Index", new { id = taskVM.CourseId });
        }
        [HttpGet]
        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> EditTask(int id)
        {
            var task = await _tasksRepository.GetById(id);
            if (task is null) return RedirectToAction("Index", "Course");
            var course = await _tasksRepository.GetCourseById(id);
            var taskVM = new TaskViewModel
            {
                DueDate = DateOnly.FromDateTime(task.DueDate),
                DueTime = TimeOnly.FromDateTime(task.DueDate),
                Name = task.Name,
                MaxMark = task.MaxMark,
                TaskText = task.TaskText,
                TaskId = id,
                CourseId = course.Id,
            };
            return View(taskVM);
        }
        [HttpPost]
        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> EditTask(TaskViewModel taskVM)
        {
            if (!ModelState.IsValid)
            {
                return View(taskVM);
            }
            var date = taskVM.DueDate.ToDateTime(taskVM.DueTime);
            await _tasksRepository.Update(taskVM.TaskId, taskVM.Name, date, taskVM.TaskText);
            return RedirectToAction("Index", new { id = taskVM.CourseId });
        }
        [HttpGet]
        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _tasksRepository.GetById(id);
            if (task is null) return RedirectToAction("Index", "Course");
            var course = await _tasksRepository.GetCourseById(id);
            var taskVM = new TaskViewModel
            {
                Name = task.Name,
                TaskText = task.TaskText,
                MaxMark = task.MaxMark,
                TaskId = id,
                CourseId = course.Id,
            };
            return View(taskVM);
        }
        [HttpPost]
        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> DeleteTask(TaskViewModel taskVM)
        {
            await _tasksRepository.Delete(taskVM.TaskId);
            return RedirectToAction("Index", new { id = taskVM.CourseId });
        }
    }
}
