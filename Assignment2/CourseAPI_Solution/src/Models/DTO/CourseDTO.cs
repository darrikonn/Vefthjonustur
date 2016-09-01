namespace CourseAPI.Models.DTO {
    /// <summary>
    /// This class represents the courses information. 
    /// </summary>
    public class CourseDTO {
        /// <summary>
        /// The id of a course.
        /// Example:
        ///     1) 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The string id of a course.
        /// Example: 
        ///     1) T-514-VEFT
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// The name of a course.
        /// Example:
        ///     1) Web Services
        /// </summary>
        public string Name { get; set; }
    }
}
