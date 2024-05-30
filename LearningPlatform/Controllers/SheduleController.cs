using LearningPlatform.Interfaces;
using LearningPlatform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearningPlatform.Controllers
{
    public class SheduleController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IClassesRepository _classesRepository;

        public SheduleController(IUsersRepository usersRepository, IClassesRepository classesRepository)
        {
            _usersRepository = usersRepository;
            _classesRepository = classesRepository;
        }

        public async Task<IActionResult> Index()
        {
            SheduleViewModel sheduleVM = new SheduleViewModel
            {
                ClassTypes = await _classesRepository.GetClassTypes(),
                SheduleSample = await _classesRepository.GetSheduleSampleList(1),
            };
            var role = User.FindFirstValue(ClaimTypes.Role);
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (role == Enum.Role.Teacher.ToString() || role == Enum.Role.Admin.ToString())
            {
                var teacher = await _usersRepository.GetTeacherById(Guid.Parse(id));
                sheduleVM.Classes = teacher.Teacher.Classes;
            }
            else if (role == Enum.Role.Student.ToString())
            {
                var student = await _usersRepository.GetStudentById(Guid.Parse(id));
                sheduleVM.Classes = student.Group.Classes;
            }
            return View(sheduleVM);
        }

    }
}
