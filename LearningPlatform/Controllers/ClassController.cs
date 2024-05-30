using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ClassController : Controller
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IClassesRepository _classesRepository;

        public ClassController(ITeachersRepository teachersRepository, IClassesRepository classesRepository)
        {
            _teachersRepository = teachersRepository;
            _classesRepository = classesRepository;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var teacher = await _teachersRepository.GetByUserId(id);
            var teacherVM = new TeacherClassesViewModel
            {
                Id = teacher.Id,
                Name = teacher.User.Name,
                Lessons = teacher.Lessons,
                Classes = await _classesRepository.Get(teacher.Id),
                ClassTypes = await _classesRepository.GetClassTypes(),
                SheduleSample = await _classesRepository.GetSheduleSampleList(1),
            };
            return View(teacherVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddClass(TeacherClassesViewModel teacherVM)
        {
            if (ModelState.IsValid)
            {
                await _classesRepository.Add(teacherVM.lessonNumber, teacherVM.isFirstWeek, teacherVM.DayOfWeek
                             , teacherVM.LessonId, teacherVM.Id, teacherVM.ClassTypeId);
            }
            var teacher = await _teachersRepository.GetByIdWithLessons(teacherVM.Id);
            return RedirectToAction("Index", new { id = teacher.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> AddGroup(int id)
        {
            var teacherId = await _classesRepository.GetTeacherIdById(id);
            var classVM = new ClassGroupsViewModel
            {
                TeacherId = teacherId,
                ClassId = id,
                Groups = await _classesRepository.GetNonAvailableGroups(teacherId, id),
            };
            return View(classVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddGroup(ClassGroupsViewModel classVM)
        {
            var teacher = await _teachersRepository.GetByIdWithLessons(classVM.TeacherId);
            if (!ModelState.IsValid)
            {
                return View(classVM);
            }
            await _classesRepository.AddGroup(classVM.ClassId, classVM.GroupId);
            return RedirectToAction("Index", new { id = teacher.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var teacherId = await _classesRepository.GetTeacherIdById(id);
            var classVM = new ClassGroupsViewModel
            {
                TeacherId = teacherId,
                ClassId = id,
                Groups = await _classesRepository.GetAvailableGroups(teacherId, id),
            };
            return View(classVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGroup(ClassGroupsViewModel classVM)
        {
            if (!ModelState.IsValid)
            {
                return View(classVM);
            }
            var teacher = await _teachersRepository.GetByIdWithLessons(classVM.TeacherId);
            await _classesRepository.DeleteGroup(classVM.ClassId, classVM.GroupId);
            return RedirectToAction("Index", new { id = teacher.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var teacherId = await _classesRepository.GetTeacherIdById(id);
            var classEntity = await _classesRepository.GetById(id);

            var teacherVM = new TeacherClassesViewModel
            {
                Id = teacherId,
                ClassId = id,
                LessonId = classEntity.LessonId,
                ClassTypeId = classEntity.ClassTypeId,
                Lessons = await _teachersRepository.GetAvailableLessons(teacherId),
                ClassTypes = await _classesRepository.GetClassTypes(),
            };
            return View(teacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeacherClassesViewModel teacherVM)
        {
            var teacher = await _teachersRepository.GetByIdWithLessons(teacherVM.Id);
            if (!ModelState.IsValid)
            {
                teacherVM.Lessons = teacher.Lessons;
                teacherVM.ClassTypes = await _classesRepository.GetClassTypes();
                return View(teacherVM);
            }
            await _classesRepository.Update(teacherVM.ClassId, teacherVM.LessonId, teacherVM.ClassTypeId);
            return RedirectToAction("Index", new { id = teacher.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var teacherId = await _classesRepository.GetTeacherIdById(id);
            var classEntity = await _classesRepository.GetById(id);

            var teacherVM = new TeacherClassesViewModel
            {
                Id = teacherId,
                ClassId = id,
                LessonId = classEntity.LessonId,
                ClassTypeId = classEntity.ClassTypeId,
                Lessons = await _teachersRepository.GetAvailableLessons(teacherId),
                ClassTypes = await _classesRepository.GetClassTypes(),
            };
            return View(teacherVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(TeacherClassesViewModel teacherVM)
        {
            var teacher = await _teachersRepository.GetByIdWithLessons(teacherVM.Id);
            await _classesRepository.Delete(teacherVM.ClassId);
            return RedirectToAction("Index", new { id = teacher.UserId });
        }
    }
}
