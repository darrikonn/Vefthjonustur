namespace CourseAPI.Models.DTO {
    using System.Collections.Generic;

    /// <summary>
    /// This class represents the detail of each individual course. 
    /// Inherits from CourseDTO, with more specific details.
    /// </summary>
    public class CourseDetailsDTO : CourseDTO {
        /// <summary>
        /// The list of all students in a specific course.
        /// </summary>
        public List<StudentDTO> Students { get; set; }
    }
}
