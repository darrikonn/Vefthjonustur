namespace CourseAPI.Models.DTOModels {
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

        /// <summary>
        /// The maximum number of students in this course
        /// Example:
        ///     1) 10
        /// </summary>
        public int MaxStudents { get; set; }

        /// <summary>
        /// Construct an empty list in the constructor.
        /// </summary>
        public CourseDetailsDTO() {
            Students = new List<StudentDTO>();
        }
    }
}
