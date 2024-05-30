using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class MemberController : Controller
    {
        public readonly ICoursesRepository _courseRepository;

        public MemberController(ICoursesRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [Authorize(Policy = "MemberCourse")]
        public async Task<IActionResult> Index(int id)
        {
            var teachersCourses = await _courseRepository.GetByIdWithTeachers(id);
            var groupsCourses = await _courseRepository.GetByIdWithGroups(id);
            var memberVM = new MemberViewModel
            {
                OwnerName = groupsCourses.User.Name,
                CourseId = id,
                Name = teachersCourses.User.Name,
                Teachers = teachersCourses.Teachers,
                Groups = groupsCourses.Groups,
            };
            return View(memberVM);
        }
        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> AddTeacher(int id)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var teachersCourses = await _courseRepository.GetNonAvailableTeachers(Guid.Parse(teacherId), id);
            var memberVM = new MemberTeachersViewModel
            {
                CourseId = id,
                Users = teachersCourses,
            };
            return View(memberVM);
        }
        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> AddTeacher(MemberTeachersViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var teachersCourses = await _courseRepository.GetNonAvailableTeachers(Guid.Parse(teacherId), memberVM.CourseId);
                memberVM.Users = teachersCourses;
                return View(memberVM);
            }
            await _courseRepository.AddTeacher(memberVM.TeacherId, memberVM.CourseId);
            return RedirectToAction("Index", new { id = memberVM.CourseId });
        }

        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> DeleteTeacher(int id, Guid teacherId)
        {
            var memberVM = new MemberTeachersViewModel
            {
                CourseId = id,
                TeacherName = await _courseRepository.GetTeacherNameById(teacherId),
                TeacherId = teacherId
            };
            return View(memberVM);
        }
        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> DeleteTeacher(MemberTeachersViewModel memberVM)
        {
            await _courseRepository.DeleteTeacher(memberVM.TeacherId, memberVM.CourseId);
            return RedirectToAction("Index", new { id = memberVM.CourseId });
        }

        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> AddGroup(int id)
        {
            var groupsCourses = await _courseRepository.GetNonAvailableGroups(id);
            var memberVM = new MemberGroupsViewModel
            {
                CourseId = id,
                Groups = groupsCourses,
            };
            return View(memberVM);
        }
        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> AddGroup(MemberGroupsViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                var groupsCourses = await _courseRepository.GetNonAvailableGroups(memberVM.CourseId);
                memberVM.Groups = groupsCourses;
                return View(memberVM);
            }
            await _courseRepository.AddGroup(memberVM.GroupId, memberVM.CourseId);
            return RedirectToAction("Index", new { id = memberVM.CourseId });
        }

        [HttpGet]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> DeleteGroup(int id, int groupId)
        {
            var groupsCourses = await _courseRepository.GetNonAvailableGroups(id);
            var memberVM = new MemberGroupsViewModel
            {
                CourseId = id,
                GroupName = await _courseRepository.GetGroupNameById(groupId),
                GroupId = groupId
            };
            return View(memberVM);
        }
        [HttpPost]
        [Authorize(Policy = "CourseManipulation")]
        public async Task<IActionResult> DeleteGroup(MemberGroupsViewModel memberVM)
        {
            await _courseRepository.DeleteGroup(memberVM.GroupId, memberVM.CourseId);
            return RedirectToAction("Index", new { id = memberVM.CourseId });
        }
    }
}
