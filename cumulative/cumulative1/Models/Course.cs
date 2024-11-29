namespace cumulative1.Models
{
    public class Course
    {
        /// <summary>
        /// Primary key for each course.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the course code.
        /// </summary>
        public string CourseCode { get; set; }

        /// <summary>
        /// Gets or sets the teacher's ID.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the course's start date.
        /// </summary>
        public DateOnly StartDate { get; set; }

        /// <summary>
        /// Gets or sets the course's finish date.
        /// </summary>
        public DateOnly FinishDate { get; set; }

        /// <summary>
        /// Gets or sets the course name.
        /// </summary>
        public string CourseName { get; set; }
    }
}
