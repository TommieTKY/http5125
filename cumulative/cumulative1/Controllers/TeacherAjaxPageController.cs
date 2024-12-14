using Microsoft.AspNetCore.Mvc;

namespace cumulative1.Controllers
{
    public class TeacherAjaxPageController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult UpdateTeacher()
        {
            return View();
        }
    }
}
