using Microsoft.AspNetCore.Mvc;
using cumulative1.Models;
using MySql.Data.MySqlClient;

namespace cumulative1.Controllers
{
    [Route("api/Course")]
    [ApiController] //Adds API-specific enhancements 
    public class CourseAPIController : Controller
    {
        // injection of SchoolDbContext
        private readonly SchoolDbContext _context;
        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of Courses from the db
        /// </summary>
        /// <example>
        /// curl -X GET "https://localhost:7293/api/Course/ListCourses"
        /// GET api/Course/ListCourses -> {"courseId":1,"courseCode":"http5101","teacherId":1,"startDate":"2018-09-04","finishDate":"2018-12-14","courseName":"Web Application Development"},...]
        /// </example>
        /// <returns>A list of strings</returns>
        [HttpGet]
        [Route(template:"ListCourses")]
        public List<Course> ListCourses()
        {
            //Create an empty list of courses for return
            List<Course> courses = new List<Course>();

            // connect to db
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();

                // SQL statement
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM courses";

                // collect the result
                using (MySqlDataReader resultSet = command.ExecuteReader())
                {
                    while (resultSet.Read())
                    {
                        // create a new object
                        Course currentCourse = new Course();
                        currentCourse.CourseId = Convert.ToInt32(resultSet["courseid"]);
                        currentCourse.CourseCode = resultSet["coursecode"].ToString();
                        currentCourse.TeacherId = Convert.ToInt32(resultSet["teacherid"]);
                        currentCourse.StartDate = DateOnly.FromDateTime(Convert.ToDateTime(resultSet["startdate"]));
                        currentCourse.FinishDate = DateOnly.FromDateTime(Convert.ToDateTime(resultSet["finishdate"]));
                        currentCourse.CourseName = resultSet["coursename"].ToString();
                        // add object to the list
                        courses.Add(currentCourse);
                    }
                }
            }
            return courses;
        }
    }
}
