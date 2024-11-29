namespace cumulative1.Models
{
    public class Student
    {
        /// <summary>
        /// Primary key for each student.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the student's first name.
        /// </summary>
        public string StudentFname { get; set; }

        /// <summary>
        /// Gets or sets the student's last name.
        /// </summary>
        public string StudentLname { get; set; }

        /// <summary>
        /// Gets or sets the student number.
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// Gets or sets the date the student was enrolled.
        /// </summary>
        public DateOnly EnrolDate { get; set; }
    }
}
