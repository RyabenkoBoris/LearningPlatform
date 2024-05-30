using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IStudentTasksRepository _studentTasksRepository;
        private readonly IConfiguration Configuration;
        public TaskController(ITasksRepository tasksRepository, IFilesRepository filesRepository
                            , IConfiguration configuration, ICommentsRepository commentsRepository
                            , IStudentTasksRepository studentTasksRepository)
        {
            _tasksRepository = tasksRepository;
            _filesRepository = filesRepository;
            _commentsRepository = commentsRepository;
            Configuration = configuration;
            _studentTasksRepository = studentTasksRepository;
        }
        [Authorize(Policy = "TaskAccess")]
        public async Task<IActionResult> Index(int id)
        {
            var task = await _tasksRepository.GetById(id);
            if (task is null) RedirectToAction("Index", "Course");
            var course = await _tasksRepository.GetCourseById(id);

            var taskVM = new TaskIndexViewModel
            {
                TaskId = id,
                Name = task.Name,
                CreatorName = task.User.Name,
                DueDate = task.DueDate,
                CreateDate = task.CreateDate,
                TaskText = task.TaskText,
                Id = course.Id,
                Files = task.FilePaths,
                MaxMark = task.MaxMark,
            };
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Enum.Role.Student.ToString())
            {
                var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var studentTask = await _tasksRepository.GetStudentTaskById(id, Guid.Parse(studentId));
                taskVM.StudentTaskId = studentTask.Id;
                taskVM.StudentFiles = studentTask.FilePaths;
                taskVM.Comments = await _commentsRepository.GetStudentTaskComments(studentTask.Id);
                taskVM.IsSubmitted = studentTask.IsSubmitted;
                taskVM.Mark = studentTask.Mark;
            }

            return View(taskVM);
        }
        [HttpGet]
        [Authorize(Policy = "TaskManipulation")]
        public IActionResult AddFile(int id)
        {
            var fileVM = new FileViewModel
            {
                TaskId = id,
            };
            return View(fileVM);
        }
        [HttpPost]
        [RequestSizeLimit(314572800)]
        [Authorize(Policy = "TaskManipulation")]
        public async Task<IActionResult> AddFile(FileViewModel fileVM)
        {
            if (!ModelState.IsValid)
            {
                return View(fileVM);
            }
            foreach (var file in fileVM.Files)
            {
                await _filesRepository.Add(file, fileVM.TaskId);
            }
            return RedirectToAction("Index", new { id = fileVM.TaskId });
        }
        [HttpGet]
        [Authorize(Policy = "FileDownload")]
        public async Task<IActionResult> Download(int id, int taskId)
        {
            var filePath = await _filesRepository.GetById(id);
            if (filePath is null) RedirectToAction("Index", "Course");
            var positionOptions = Configuration.GetSection(PathOptions.Position)
                                                     .Get<PathOptions>();
            var folderPath = positionOptions.Path;

            var fullPath = folderPath + filePath.Path;

            if (System.IO.File.Exists(fullPath))
            {
                return File(System.IO.File.OpenRead(fullPath), "application/octet-stream", filePath.Name);
            }

            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpGet]
        [Authorize(Policy = "FileDelete")]
        public async Task<IActionResult> Delete(int id, int taskId)
        {
            await _filesRepository.Delete(id);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpGet]
        [Authorize(Policy = "StudentTaskManipulation")]
        public IActionResult AddSF(int id, int taskId)
        {
            var fileVM = new FileViewModel
            {
                StudentTaskId = id,
                TaskId = taskId,
            };
            return View(fileVM);
        }
        [HttpPost]
        [RequestSizeLimit(314572800)]
        [Authorize(Policy = "StudentTaskManipulation")]
        public async Task<IActionResult> AddSF(FileViewModel fileVM)
        {
            if (!ModelState.IsValid)
            {
                return View(fileVM);
            }
            foreach (var file in fileVM.Files)
            {
                await _filesRepository.AddStudentFile(file, fileVM.StudentTaskId);
            }
            return RedirectToAction("Index", new { id = fileVM.TaskId });
        }

        [HttpPost]
        [Authorize(Policy = "StudentWriteComment")]
        public async Task<IActionResult> WriteComment(TaskIndexViewModel taskVM)
        {
            if (!ModelState.IsValid)
            {
                return View(taskVM);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _commentsRepository.Add(taskVM.Comment, Guid.Parse(userId), taskVM.StudentTaskId);
            return RedirectToAction("Index", new { id = taskVM.TaskId });
        }

        [HttpGet]
        [Authorize(Policy = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id, int taskId)
        {
            await _commentsRepository.Delete(id);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpGet]
        [Authorize(Policy = "StudentTaskManipulation")]
        public async Task<IActionResult> Submit(int id, int taskId)
        {
            await _studentTasksRepository.Submit(id);
            return RedirectToAction("Index", new { id = taskId });
        }

        [HttpGet]
        [Authorize(Policy = "StudentTaskManipulation")]
        public async Task<IActionResult> Cancel(int id, int taskId)
        {
            await _studentTasksRepository.Cancel(id);
            return RedirectToAction("Index", new { id = taskId });
        }
    }
}
