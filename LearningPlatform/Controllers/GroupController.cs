using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> Index()
        {
            var groupVM = await _groupRepository.Get();
            return View(groupVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var groupVM = new GroupViewModel
            {
                Departments = await _groupRepository.GetDepartments(),
                Specialities = await _groupRepository.GetSpecialities(),
                Faculties = await _groupRepository.GetFaculties()
            };
            return View(groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupViewModel groupVM)
        {
            if (!ModelState.IsValid)
            {
                groupVM.Departments = await _groupRepository.GetDepartments();
                groupVM.Specialities = await _groupRepository.GetSpecialities();
                groupVM.Faculties = await _groupRepository.GetFaculties();
                return View(groupVM);
            }
            await _groupRepository.Add(groupVM.Name, groupVM.DepartmentId, groupVM.SpecialityId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var group = await _groupRepository.GetById(id);
            if (group == null) return View("Error");
            var groupVM = new GroupViewModel
            {
                Id = id,
                Name = group.Name,
                DepartmentId = group.DepartmentId,
                SpecialityId = group.SpecialityId,
                Specialities = await _groupRepository.GetSpecialities(),
            };
            return View(groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupViewModel groupVM)
        {
            if (!ModelState.IsValid)
            {
                groupVM.Specialities = await _groupRepository.GetSpecialities();
                return View(groupVM);
            }
            await _groupRepository.Update(groupVM.Id, groupVM.Name, groupVM.SpecialityId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _groupRepository.GetById(id);
            if (group == null) return View("Error");
            var groupVM = new GroupViewModel
            {
                Id = id,
                Name = group.Name,
                DepartmentId = group.DepartmentId,
                SpecialityId = group.SpecialityId,
                Departments = await _groupRepository.GetDepartments(),
                Specialities = await _groupRepository.GetSpecialities(),
                Faculties = await _groupRepository.GetFaculties()
            };
            return View(groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GroupViewModel groupVM)
        {
            await _groupRepository.Delete(groupVM.Id);
            return RedirectToAction("Index");
        }
    }
}
