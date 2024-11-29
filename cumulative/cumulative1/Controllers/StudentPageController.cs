using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.Security.Cryptography.Xml;
using cumulative1.Models;

namespace cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        // retrieve student info rely on the API
        private readonly StudentAPIController _api;
        //StudentPageController holds a reference to the StudentAPIController object
        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }

        // GET: StudentPage/List
        public IActionResult List()
        {
            List<Student> stduents = _api.ListStudents();
            return View(stduents);
        }


        [HttpGet]
        public IActionResult Show(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string StudentFname, string StudentLname, string StudentNumber)
        {
            Student NewStudent = new Student();
            NewStudent.StudentFname = StudentFname;
            NewStudent.StudentLname = StudentLname;
            NewStudent.StudentNumber = StudentNumber;

            int StudentId = _api.AddStudent(NewStudent);

            return RedirectToAction("Show", new { id = StudentId });
        }


        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Student SelectedStudent = _api.FindStudent(id);
            return View(SelectedStudent);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int RowsAffected = _api.DeleteStudent(id);
            return RedirectToAction("List");
        }
    }
}
