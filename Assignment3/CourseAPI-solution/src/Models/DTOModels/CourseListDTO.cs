namespace CourseAPI.Models.DTOModels {
    /// <summary>
    /// This class represents the list of all courses. 
    /// Inherits from CourseDTO, with more specific details.
    /// </summary>
    public class CourseListDTO : CourseDTO {
        /// <summary>
        /// The number of students in a specific course.
        /// Example:
        ///     1) 4
        /// </summary>
        public int NumberOfStudents { get; set; }
    }
}
