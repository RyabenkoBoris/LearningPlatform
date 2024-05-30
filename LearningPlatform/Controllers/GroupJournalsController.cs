using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class GroupJournalsController : Controller
    {
        private readonly IJournalRepository _journalRepository;

        public GroupJournalsController(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            var groupJournals = await _journalRepository.GetByGroupId(id);
            return View(groupJournals);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var journal = await _journalRepository.GetWithLessonGroupById(id);
            if (journal == null) return RedirectToAction("Index", "Group");
            var journalVM = new JournalEditViewModel
            {
                JournalId = id,
                GroupId = journal.GroupId,
                Name = journal.Lesson.Name,
                GroupName = journal.Group.Name,
            };
            return View(journalVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JournalEditViewModel journalVM)
        {
            if (!ModelState.IsValid)
            {
                return View(journalVM);
            }
            await _journalRepository.SetSemester(journalVM.JournalId, journalVM.Semester);
            return RedirectToAction("Index", new { id = journalVM.GroupId });
        }

        [HttpGet]
        public async Task<IActionResult> Hide(int id, int groupId)
        {
            await _journalRepository.Hide(id);
            return RedirectToAction("Index", new { id = groupId });
        }

        [HttpGet]
        public async Task<IActionResult> Show(int id, int groupId)
        {
            await _journalRepository.Show(id);
            return RedirectToAction("Index", new { id = groupId });
        }
    }
}
