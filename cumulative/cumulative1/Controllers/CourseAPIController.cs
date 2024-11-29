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

        /// <summary>
        /// Receives a Course’s Id and returns the associated course information
        /// </summary>
        /// <param name="CourseId">The primary key of the course table</param>
        /// <example>
        /// curl -X GET "https://localhost:7293/api/Course/FindCourse/2"
        /// -> {"courseId":2,"courseCode":"http5102","teacherId":2,"startDate":"2018-09-04","finishDate":"2018-12-14","courseName":"Project Management"}
        /// </example>
        /// <returns>The associated course object matching the primary key</returns>
        [HttpGet]
        [Route(template: "FindCourse/{CourseId}")]
        public Course FindCourse(int CourseId)
        {
            Course SelectedCourse = new Course();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM courses WHERE courseid=@id";
                Command.Parameters.AddWithValue("@id", CourseId);

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        SelectedCourse.CourseId = Convert.ToInt32(ResultSet["courseid"]);
                        SelectedCourse.CourseCode = ResultSet["coursecode"].ToString();
                        SelectedCourse.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        SelectedCourse.StartDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["startdate"]));
                        SelectedCourse.FinishDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["finishdate"]));
                        SelectedCourse.CourseName = ResultSet["coursename"].ToString();
                    }
                }
            }
            return SelectedCourse;
        }

        /// <summary>
        /// This endpoint will receive Course Data and add the course to the DB
        /// </summary>
        /// <param name="NewCourse">The course object to add</param>
        /// <returns>The course ID that was inserted</returns>
        /// <example>
        /// POST: api/Course/AddCourse
        /// Header: Content-Type: application/json
        /// Data: {"CourseCode":"IXD5200", "TeacherId"="6", "StartDate"="2024/01/01", "FinishDate"="2024/05/01", CourseName="UI design"}
        /// curl -X "POST" -H "Content-Type: application/json" -d "{\"courseCode\":\"IXD5200\", \"teacherId\":6, \"startDate\":\"2024-01-01\", \"finishDate\":\"2024-05-01\", \"courseName\":\"UI design\"}" "https://localhost:7293/api/Course/AddCourse"
        /// -> 0 // should be the LastInsertedId
        /// </example>
        [HttpPost(template: "AddCourse")]
        public int AddCourse([FromBody] Course NewCourse)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "INSERT INTO courses (courseid, coursecode, teacherid, startdate, finishdate, coursename) values (0, @code, @tid, @sdate, @fdate, @name)";
                Command.Parameters.AddWithValue("@code", NewCourse.CourseCode);
                Command.Parameters.AddWithValue("@tid", NewCourse.TeacherId);
                Command.Parameters.AddWithValue("@sdate", NewCourse.StartDate);
                Command.Parameters.AddWithValue("@fdate", NewCourse.FinishDate);
                Command.Parameters.AddWithValue("@name", NewCourse.CourseName);

                Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId);
            }
            return 0;
        }


        /// <summary>
        /// Receives an ID and deletes the course from the system
        /// </summary>
        /// <param name="CourseId">The course ID primary key to delete</param>
        /// <returns>The number of courses deleted</returns>
        /// <example>
        /// curl -X DELETE "https://localhost:7293/api/Course/DeleteCourse/0" 
        /// ->1
        /// </example>
        [HttpDelete(template: "DeleteCourse/{CourseId}")]
        public int DeleteCourse(int CourseId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "DELETE FROM courses WHERE courseid=@id";
                Command.Parameters.AddWithValue("@id", CourseId);

                return Command.ExecuteNonQuery();
            }
        }
    }
}
