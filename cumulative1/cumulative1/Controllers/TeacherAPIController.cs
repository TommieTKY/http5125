using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using cumulative1.Models;
using MySql.Data.MySqlClient;

namespace cumulative1.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <param name="dateStart">the start date of the search range</param>
        /// <param name="dateEnd">the end date of the search range</param>
        /// <example>
        /// curl -X GET "https://localhost:7293/api/Teacher/ListTeachers"
        /// GET api/Teacher/ListTeachers -> [{"teacherId":1,"teacherFname":"Alexander","teacherLname":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":"$55.30","courseList":"Web Application Development"}...]
        /// </example>
        /// <returns>
        /// A list of strings
        /// </returns>
        [HttpGet]
        [Route(template: "ListTeachers")]        
        public List<Teacher> ListTeachers(DateTime? dateStart, DateTime? dateEnd) // OR DateTime start=null
        {
            List<Teacher> Teachers = new List<Teacher>();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a new command (query) for our database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL QUERY
                string query = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courseName FROM teachers LEFT JOIN courses ON teachers.teacherid=courses.teacherid GROUP BY teachers.teacherid";

                // search for a Teacher by their Hire Date within a range
                if (dateStart != null && dateEnd != null && dateEnd > dateStart)
                {
                    query = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courseName FROM teachers LEFT JOIN courses ON teachers.teacherid=courses.teacherid WHERE hiredate BETWEEN @start AND @end GROUP BY teachers.teacherid";
                    Command.Parameters.AddWithValue("@start", dateStart.Value.ToString("yyyy-MM-dd"));
                    Command.Parameters.AddWithValue("@end", dateEnd.Value.ToString("yyyy-MM-dd"));
                }

                Command.CommandText = query;
                Command.Prepare(); // executed multiple times with different parameters

                // Gather Result Set of Query into a variable
                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    //Loop Through Each Row the Result Set
                    //until ResultSet = false
                    while (ResultSet.Read())
                    {
                        Teacher CurrentTeacher = new Teacher();
                        //Access Column information by the DB column name as an index
                        CurrentTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                        CurrentTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                        CurrentTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                        CurrentTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                        CurrentTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        CurrentTeacher.Salary = "$"+(ResultSet["salary"].ToString());
                        CurrentTeacher.CourseList = ResultSet["courseName"].ToString();
                        Teachers.Add(CurrentTeacher);
                    }
                }
                //Return the final list of author names
                return Teachers;
            }
        }

        /// <summary>
        /// Receives a teacher’s Id and returns the associated teacher information
        /// </summary>
        /// <param name="TeacherId">The primary key of the teacher table</param>
        /// <example>
        /// GET: api/FindTeacher/1 -> ["teacherId":1,"teacherFname":"Alexander","teacherLname":"Bennett","employeeNumber":"T378","hireDate":"2016-08-05T00:00:00","salary":"$55.30","courseList":"Web Application Development"]
        /// curl -X GET "https://localhost:7293/api/Teacher/FindTeacher/100"
        /// -> {"teacherId":100,"teacherFname":"Teacher Not Found","teacherLname":"Teacher Not Found","employeeNumber":"Teacher Not Found","hireDate":"0001-01-01T00:00:00","salary":"Teacher Not Found","courseList":"Teacher Not Found"}
        /// curl -X GET "https://localhost:7293/api/Teacher/FindTeacher/-1"
        /// -> {"teacherId":-1,"teacherFname":"Teacher Not Found","teacherLname":"Teacher Not Found","employeeNumber":"Teacher Not Found","hireDate":"0001-01-01T00:00:00","salary":"Teacher Not Found","courseList":"Teacher Not Found"}
        /// curl -X GET "https://localhost:7293/api/Teacher/FindTeacher/10"
        /// -> {"teacherId":10,"teacherFname":"John","teacherLname":"Taram","employeeNumber":"T505","hireDate":"2015-10-23T00:00:00","salary":"$79.63","courseList":"Massage Therapy"}
        /// </example>
        /// <returns>The associated teacher object matching the primary key</returns>
        [HttpGet]
        [Route(template: "FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            //Finds a Teacher by the Teacher’s ID
            Teacher SelectedTeacher = new Teacher();

            //Error Handling when trying to access a teacher that does not exist
            if (TeacherId < 1 || TeacherId > 10)
            {
                SelectedTeacher.TeacherId = TeacherId;
                SelectedTeacher.TeacherFname = "Teacher Not Found";
                SelectedTeacher.TeacherLname = "Teacher Not Found";
                SelectedTeacher.EmployeeNumber = "Teacher Not Found";
                SelectedTeacher.HireDate = DateTime.MinValue;
                SelectedTeacher.Salary = "Teacher Not Found";
                SelectedTeacher.CourseList = "Teacher Not Found";
            }
            else { 

            //create a connection to db
            MySqlConnection Connection = _context.AccessDatabase();

            //open the connection to the db
            Connection.Open();

            //Created a db command
            MySqlCommand Command = Connection.CreateCommand();

            //set the command text; left join the teaching courses
            Command.CommandText = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courseName FROM teachers LEFT JOIN courses ON teachers.teacherid=courses.teacherid WHERE teachers.teacherid=@id GROUP BY teachers.teacherid";
            Command.Parameters.AddWithValue("@id", TeacherId);

            //gather result set into a variable
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //read through the results in a loop
            
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                SelectedTeacher.Salary = "$" + (ResultSet["salary"].ToString());
                SelectedTeacher.CourseList = ResultSet["courseName"].ToString();
                } 

            //As we do not using (MySqlDataReader ResultSet = Command.ExecuteReader())
            ResultSet.Close();
            //As we do not using (MySqlConnection Connection = _context.AccessDatabase())
            Connection.Close();

            }
            return SelectedTeacher;         
        }
    }
}
