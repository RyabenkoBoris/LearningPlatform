using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class StudentTaskController : Controller
    {
        private readonly IStudentTasksRepository _studentTasksRepository;
        private readonly ICommentsRepository _commentsRepository;

        public StudentTaskController(IStudentTasksRepository studentTasksRepository, ICommentsRepository commentsRepository)
        {
            _studentTasksRepository = studentTasksRepository;
            _commentsRepository = commentsRepository;
        }

        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> Index(int id)
        {
            var studentTasks = await _studentTasksRepository.GetStudentTasks(id);
            var studentTaskVM = new StudentTaskViewModel
            {
                TaskId = id,
                StudentTasks = studentTasks,
            };
            return View(studentTaskVM);
        }
        [Authorize(Policy = "StudentTaskTeacherAccess")]
        public async Task<IActionResult> Current(int id)
        {
            var studentTask = await _studentTasksRepository.GetById(id);
            var studentTaskVM = new CurrentStudentTaskViewModel
            {
                StudentTaskId = id,
                TaskId = studentTask.TaskId,
                Comments = studentTask.Comments,
                StudentFiles = studentTask.FilePaths,
                StudentName = studentTask.User.Name,
                TaskName = studentTask.Task.Name,
                Mark = studentTask.Mark,
                MaxMark = studentTask.Task.MaxMark,
            };
            return View(studentTaskVM);
        }

        [HttpPost]
        [Authorize(Policy = "TeacherWriteComment")]
        public async Task<IActionResult> WriteComment(TaskIndexViewModel taskVM)
        {
            if (!ModelState.IsValid)
            {
                return View(taskVM);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _commentsRepository.Add(taskVM.Comment, Guid.Parse(userId), taskVM.StudentTaskId);
            return RedirectToAction("Current", new { id = taskVM.StudentTaskId });
        }

        [HttpGet]
        [Authorize(Policy = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id, int studentTaskId)
        {
            await _commentsRepository.Delete(id);
            return RedirectToAction("Current", new { id = studentTaskId });
        }

        [HttpPost]
        [Authorize(Policy = "TeacherWriteComment")]
        public async Task<IActionResult> Estimate(CurrentStudentTaskViewModel studentTaskVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Current", new { id = studentTaskVM.StudentTaskId });
            }
            await _studentTasksRepository.Estimate(studentTaskVM.StudentTaskId, studentTaskVM.Mark);
            return RedirectToAction("Current", new { id = studentTaskVM.StudentTaskId });
        }

    }
}
