namespace CourseAPI.Models.DTO {
    /// <summary>
    /// This class represents the detail of each individual course. 
    /// Inherits from CourseDTO, with more specific details.
    /// </summary>
    public class CourseDetailsDTO : CourseDTO {
        /// <summary>
        /// The number of students in a specific course.
        /// </summary>
        public int NumberOfStudents { get; set; }
    }
}
