using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;

namespace cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        // get info from the api
        private readonly CourseAPIController _api;
        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        // GET: CoursePage/List
        public IActionResult List()
        {
            List<Course> courses = _api.ListCourses();
            return View(courses);
        }
    }
}
