namespace cumulative1.Models
{
    /// <summary>
    /// represent information about a teacher
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Primary key for each teacher.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the teacher's first name.
        /// </summary>
        public string TeacherFname { get; set; }

        /// <summary>
        /// Gets or sets the teacher's last name.
        /// </summary>
        public string TeacherLname { get; set; }

        /// <summary>
        /// Gets or sets the employee number for the teacher.
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// Gets or sets the date the teacher was hired.
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Gets or sets the teacher's salary.
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the teacher's courses.
        /// </summary>
        public string CourseList { get; set; }
    }
}
