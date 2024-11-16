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
    }
}
