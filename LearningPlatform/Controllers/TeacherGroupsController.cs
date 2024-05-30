using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TeacherGroupsController : Controller
    {
        private readonly ITeachersRepository _teachersRepository;

        public TeacherGroupsController(ITeachersRepository teachersRepository)
        {
            _teachersRepository = teachersRepository;
        }
        public async Task<IActionResult> Index(Guid id)
        {
            var teacherGroupsVM = await _teachersRepository.GetByIdWithGroups(id);
            return View(teacherGroupsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Add(Guid id)
        {
            var teacher = await _teachersRepository.GetByIdWithGroups(id);
            var teacherGroupsVM = new TeacherGroupsViewModel
            {
                Id = id,
                Name = teacher.User.Name,
                Groups = await _teachersRepository.GetNonAvailableGroups(id),
            };
            return View(teacherGroupsVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(TeacherGroupsViewModel teacherGroupsVM)
        {
            if (!ModelState.IsValid)
            {
                var teacher = await _teachersRepository.GetByIdWithLessons(teacherGroupsVM.Id);
                teacherGroupsVM.Name = teacher.User.Name;
                return View(teacherGroupsVM);
            }
            await _teachersRepository.AddGroup(teacherGroupsVM.Id, teacherGroupsVM.GroupId);
            return RedirectToAction("Index", new { id = teacherGroupsVM.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, int groupId)
        {
            var teacher = await _teachersRepository.GetByIdWithGroups(id);
            if (teacher == null) return View("Error");
            var teacherGroupsVM = new TeacherGroupsViewModel
            {
                GroupId = groupId,
                Name = teacher.User.Name,
                Groups = await _teachersRepository.GetGroups(),
            };
            return View(teacherGroupsVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TeacherGroupsViewModel teacherGroupsVM, int groupId)
        {
            await _teachersRepository.DeleteGroup(teacherGroupsVM.Id, groupId);
            return RedirectToAction("Index", new { id = teacherGroupsVM.Id });
        }
    }
}
