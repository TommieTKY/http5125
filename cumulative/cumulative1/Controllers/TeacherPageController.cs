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
            return View(SelectedTeacher);
        }


        // GET: TeacherPage/New -> A webpage that prompts the user to enter new teacher information
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            int TeacherId = _api.AddTeacher(NewTeacher);

            return RedirectToAction("Show", new {id=TeacherId});
        }
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int RowsAffected = _api.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View(SelectedTeacher);
        }

        [HttpPost]
        public IActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Teacher UpdateTeacher = new Teacher();
            UpdateTeacher.TeacherFname = TeacherFname;
            UpdateTeacher.TeacherLname = TeacherLname;
            UpdateTeacher.EmployeeNumber = EmployeeNumber;
            UpdateTeacher.HireDate = HireDate;
            UpdateTeacher.Salary = Salary;

            _api.UpdateTeacher(id, UpdateTeacher);
            return RedirectToAction("Show", new {id=id});
        }
    }
}

// ControllerBase: Used for APIs that don’t require views. only need to return data (like JSON) and don’t use MVC views.
// Controller: Used for MVC applications that include views. return HTML along with JSON data 
