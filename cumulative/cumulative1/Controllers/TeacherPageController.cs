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
            IActionResult response = _api.FindTeacher(id);
            if (response is OkObjectResult okresult)
            {
                Teacher? SelectedTeacher = okresult.Value == null ? null : (Teacher)okresult.Value;
                return View(SelectedTeacher);
            }
            else
            {
                return View(null);
            }
            
        }


        // GET: TeacherPage/New -> A webpage that prompts the user to enter new teacher information
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            int TeacherId = _api.AddTeacher(NewTeacher);

            return RedirectToAction("Show", new {id=TeacherId});
        }
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            IActionResult response = _api.FindTeacher(id);
            if (response is OkObjectResult okresult)
            {
                Teacher? SelectedTeacher = okresult.Value == null ? null : (Teacher)okresult.Value;
                return View(SelectedTeacher);
            }
            else
            {
                return View(null);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int RowsAffected = _api.DeleteTeacher(id);

            return RedirectToAction("List");
        }
    }
}

// ControllerBase: Used for APIs that don’t require views. only need to return data (like JSON) and don’t use MVC views.
// Controller: Used for MVC applications that include views. return HTML along with JSON data 
