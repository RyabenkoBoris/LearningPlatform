using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SpecialityController : Controller
    {
        private readonly ISpecialitiesRepository _specialitiesRepository;

        public SpecialityController(ISpecialitiesRepository specialitiesRepository)
        {
            _specialitiesRepository = specialitiesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var specialityVM = await _specialitiesRepository.Get();
            return View(specialityVM);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var specialityVM = new SpecialityViewModel();
            return View(specialityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecialityViewModel specialityVM)
        {
            if (!ModelState.IsValid)
            {
                return View(specialityVM);
            }
            await _specialitiesRepository.Add(specialityVM.Code, specialityVM.Name);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var speciality = await _specialitiesRepository.GetById(id);
            if (speciality == null) return View("Error");
            var specialityVM = new SpecialityViewModel
            {
                Id = id,
                Code = speciality.Code,
                Name = speciality.Name,
            };
            return View(specialityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpecialityViewModel specialityVM)
        {
            if (!ModelState.IsValid)
            {
                return View(specialityVM);
            }
            await _specialitiesRepository.Update(specialityVM.Id, specialityVM.Code, specialityVM.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var speciality = await _specialitiesRepository.GetById(id);
            if (speciality == null) return View("Error");
            var specialityVM = new SpecialityViewModel
            {
                Id = id,
                Code = speciality.Code,
                Name = speciality.Name,
            };
            return View(specialityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SpecialityViewModel specialityVM)
        {
            await _specialitiesRepository.Delete(specialityVM.Id);
            return RedirectToAction("Index");
        }
    }
}
