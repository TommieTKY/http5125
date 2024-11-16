using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace cumulative1.Controllers
{

    //An MVC Controller route to dynamic pages
    
    public class TeacherPageController: Controller
    {
        private readonly TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }

        //GET: TeacherPage/List -> A webpage that shows all teachers in the db
        [HttpGet]
        public IActionResult List(DateTime dateStart, DateTime dateEnd)
        {
            List<Teacher> Teachers = _api.ListTeachers(dateStart, dateEnd);

            //Direct to the /Views/TeacherPage/List.cshtml
            return View(Teachers);
        }

        // Get: TeacherPage/Show/{id} -> A webpage that displays a teacher by the Teacher’s ID
        [HttpGet]
        public IActionResult Show(int id) 
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);

            //direct to /Views/TeacherPage/Show.cshtml
            return View(SelectedTeacher);
        }
    }
}

// ControllerBase: Used for APIs that don’t require views. only need to return data (like JSON) and don’t use MVC views.
// Controller: Used for MVC applications that include views. return HTML along with JSON data 
