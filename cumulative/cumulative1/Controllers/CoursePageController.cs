using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;
using Mysqlx.Datatypes;

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

        [HttpGet]
        public IActionResult Show(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string CourseCode, int TeacherId, DateOnly StartDate, DateOnly FinishDate, string CourseName)
        {
            Course NewCourse = new Course();
            NewCourse.CourseCode = CourseCode;
            NewCourse.TeacherId = TeacherId;
            NewCourse.StartDate = StartDate;
            NewCourse.FinishDate = FinishDate;
            NewCourse.CourseName = CourseName;

            int CourseId = _api.AddCourse(NewCourse);

            return RedirectToAction("Show", new { id = CourseId });
        }
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Course SelectedCourse = _api.FindCourse(id);
            return View(SelectedCourse);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int RowsAffected = _api.DeleteCourse(id);

            return RedirectToAction("List");
        }
    }
}
