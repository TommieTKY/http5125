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
                        CurrentTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
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
        /// -> {"teacherId":100,"teacherFname":"Teacher Not Found","teacherLname":"Teacher Not Found","employeeNumber":"Teacher Not Found","hireDate":"0001-01-01T00:00:00","salary":0,"courseList":"Teacher Not Found"}
        /// curl -X GET "https://localhost:7293/api/Teacher/FindTeacher/10"
        /// -> {"teacherId":10,"teacherFname":"John","teacherLname":"Taram","employeeNumber":"T505","hireDate":"2015-10-23T00:00:00","salary":79.63,"courseList":"Massage Therapy"}}
        /// </example>
        /// <returns>The associated teacher object matching the primary key</returns>
        [HttpGet]
        [Route(template: "FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            Teacher SelectedTeacher = new Teacher();

            MySqlConnection Connection = _context.AccessDatabase();
            Connection.Open();
            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = "SELECT teachers.*, GROUP_CONCAT(courses.coursename SEPARATOR ', ') AS courseName FROM teachers LEFT JOIN courses ON teachers.teacherid=courses.teacherid WHERE teachers.teacherid=@id GROUP BY teachers.teacherid";
            Command.Parameters.AddWithValue("@id", TeacherId);

            MySqlDataReader ResultSet = Command.ExecuteReader();
            if (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["salary"]);
                SelectedTeacher.CourseList = ResultSet["courseName"].ToString();
            }
            else //Error Handling when trying to access a teacher that does not exist
            {
                SelectedTeacher.TeacherId = TeacherId;
                SelectedTeacher.TeacherFname = "Teacher Not Found";
                SelectedTeacher.TeacherLname = "Teacher Not Found";
                SelectedTeacher.EmployeeNumber = "Teacher Not Found";
                SelectedTeacher.HireDate = DateTime.MinValue;
                SelectedTeacher.Salary = 0;
                SelectedTeacher.CourseList = "Teacher Not Found";
            }
            ResultSet.Close();
            Connection.Close();
            return SelectedTeacher;
        }

        /// <summary>
        /// This endpoint will receive Teacher Data and add the teacher to the DB
        /// </summary>
        /// <param name="NewTeacher">The teacher object to add</param>
        /// <returns>The teacher ID that was inserted</returns>
        /// <example>
        /// POST: api/Teacher/AddTeacher
        /// Header: Content-Type: application/json
        /// Data: {"TeacherFname":"Tommie", "TeacherLname"="Tong", "EmployeeNumber"="T600", "Salary"=100}
        /// curl -X "POST" -H "Content-Type: application/json" -d "{\"teacherFname\":\"Tommie\", \"teacherLname\":\"Tong\", \"employeeNumber\":\"T600\", \"salary\":100, \"courseList\": \"\"}" "https://localhost:7293/api/Teacher/AddTeacher"
        /// ->17
        /// </example>
        [HttpPost(template:"AddTeacher")]
        public int AddTeacher([FromBody] Teacher NewTeacher)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "INSERT INTO teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (0, @fname, @lname, @enum, CURRENT_DATE(), @salary)";
                Command.Parameters.AddWithValue("@fname", NewTeacher.TeacherFname);
                Command.Parameters.AddWithValue("@lname", NewTeacher.TeacherLname);
                Command.Parameters.AddWithValue("@enum", NewTeacher.EmployeeNumber);
                Command.Parameters.AddWithValue("@salary", NewTeacher.Salary);

                Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId);
            }
            return 0;
        }

        /// <summary>
        /// Receives an ID and deletes the teacher from the system
        /// </summary>
        /// <param name="TeacherId">The teacher ID primary key to delete</param>
        /// <returns>The number of teachers deleted</returns>
        /// <example>
        /// DELETE api/Teacher/DeleteTeacher/17 -> 1
        /// curl -X DELETE "https://localhost:7293/api/Teacher/DeleteTeacher/17" 
        /// ->1
        /// DELETE api/Teacher/DeleteTeacher/-100 -> 0
        /// </example>
        [HttpDelete(template:"DeleteTeacher/{TeacherId}")]
        public int DeleteTeacher(int TeacherId)
        {
            using(MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "DELETE FROM teachers WHERE teacherid=@id";
                Command.Parameters.AddWithValue("@id", TeacherId);

                return Command.ExecuteNonQuery();
            }
        }
    }
}
