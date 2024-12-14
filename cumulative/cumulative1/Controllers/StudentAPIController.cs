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

        /// <summary>
        /// Receives a student’s Id and returns the associated student information
        /// </summary>
        /// <param name="StudentId">The primary key of the student table</param>
        /// <example>
        /// curl -X GET "https://localhost:7293/api/Student/FindStudent/3"
        /// ->["studentId":3,"studentFname":"Austin","studentLname":"Simon","studentNumber":"N1682","enrolDate":"2018-06-14"]
        /// </example>
        /// <returns>The associated student object matching the primary key</returns>
        [HttpGet]
        [Route(template: "FindStudent/{StudentId}")]
        public Student FindStudent(int StudentId)
        {
            Student SelectedStudent = new Student();

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM students WHERE studentid=@id";
                Command.Parameters.AddWithValue("@id", StudentId);

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        SelectedStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                        SelectedStudent.StudentFname = ResultSet["studentfname"].ToString();
                        SelectedStudent.StudentLname = ResultSet["studentlname"].ToString();
                        SelectedStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                        SelectedStudent.EnrolDate = DateOnly.FromDateTime(Convert.ToDateTime(ResultSet["enroldate"]));
                    }
                }
            }
            return SelectedStudent;
        }

        /// <summary>
        /// This endpoint will receive Student Data and add the student to the DB
        /// </summary>
        /// <param name="NewStudent">The student object to add</param>
        /// <returns>The student ID that was inserted</returns>
        /// <example>
        /// POST: api/Student/AddStudent
        /// Header: Content-Type: application/json
        /// Data: {"StudentFname":"Tommie", "StudentLname"="Tong", "StudentNumber"="N2222"}
        /// curl -X "POST" -H "Content-Type: application/json" -d "{\"studentFname\":\"Tommie\", \"studentLname\":\"Tong\", \"studentNumber\":\"N2222\"}" "https://localhost:7293/api/Student/AddStudent"
        /// ->37
        /// </example>
        [HttpPost(template: "AddStudent")]
        public int AddStudent([FromBody] Student NewStudent)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "INSERT INTO students (studentid, studentfname, studentlname, studentnumber, enroldate) values (0, @fname, @lname, @snum, CURRENT_DATE())";
                Command.Parameters.AddWithValue("@fname", NewStudent.StudentFname);
                Command.Parameters.AddWithValue("@lname", NewStudent.StudentLname);
                Command.Parameters.AddWithValue("@snum", NewStudent.StudentNumber);

                Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId);
            }
            return 0;
        }

        /// <summary>
        /// Receives an ID and deletes the student from the system
        /// </summary>
        /// <param name="StudentId">The student ID primary key to delete</param>
        /// <returns>The number of students deleted</returns>
        /// <example>
        /// curl -X DELETE "https://localhost:7293/api/Student/DeleteStudent/37" 
        /// ->1
        /// </example>
        [HttpDelete(template: "DeleteStudent/{StudentId}")]
        public int DeleteStudent(int StudentId)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "DELETE FROM students WHERE studentid=@id";
                Command.Parameters.AddWithValue("@id", StudentId);

                return Command.ExecuteNonQuery();
            }
        }

        [HttpPut(template: "UpdateStudent/{StudentId}")]
        public Student UpdateStudent(int StudentId, [FromBody] Student StudentData)
        {
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "UPDATE students SET studentfname=@fname, studentlname=@lname, studentnumber=@snum, enroldate=CURRENT_DATE() WHERE studentid=@id";
                Command.Parameters.AddWithValue("@fname", StudentData.StudentFname);
                Command.Parameters.AddWithValue("@lname", StudentData.StudentLname);
                Command.Parameters.AddWithValue("@snum", StudentData.StudentNumber);
                Command.Parameters.AddWithValue("@id", StudentId);
                Command.ExecuteNonQuery();
            }
            return FindStudent(StudentId);
        }
    }
}