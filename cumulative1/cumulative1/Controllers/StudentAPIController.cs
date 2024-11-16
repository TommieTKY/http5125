using cumulative1.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace cumulative1.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : Controller
    {
        private readonly SchoolDbContext _context;
        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of Students from the db
        /// </summary>
        /// <example>
        /// curl -X GET "https://localhost:7293/api/Student/ListStudents"
        /// GET api/Student/ListStudents -> [{"studentId":1,"studentFname":"Sarah","studentLname":"Valdez","studentNumber":"N1678","enrolDate":"2018-06-18"},...]
        /// </example>
        /// <returns>
        /// A list of strings
        /// </returns>
        [HttpGet]
        [Route(template: "ListStudents")]
        public List<Student> ListStudents()
        {
            List<Student> students = new List<Student>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a new command for our database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY
                Command.CommandText = "SELECT * FROM students";

                // Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row the Result Set
                    while (ResultSet.Read())
                    {
                        Student currentStudent = new Student();
                        //Add the data to the object from the db
                        currentStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        currentStudent.StudentFname = ResultSet["studentfname"].ToString();
                        currentStudent.StudentLname = ResultSet["studentlname"].ToString();
                        currentStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                        currentStudent.EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));
                        // Add the object to the students list
                        students.Add(currentStudent);
                    }
                }
                //Return the final list of author names
                return students;
            }
        }
    }
}