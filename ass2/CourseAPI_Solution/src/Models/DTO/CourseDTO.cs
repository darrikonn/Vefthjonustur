namespace CourseAPI.Models.DTO {
    /// <summary>
    /// This class represents the courses information. 
    /// </summary>
    public class CourseDTO {
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
