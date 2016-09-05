namespace CourseAPI.Models.DTOModels {
    /// <summary>
    /// This class represents the students information. 
    /// </summary>
    public class StudentDTO {
        /// <summary>
        /// The social security number of a student.
        /// Example:
        ///     1) 1501933119
        /// </summary>
        public string SSN { get; set; }

        /// <summary>
        /// The name of a student.
        /// Example:
        ///     2) Darri Steinn Konradsson
        /// </summary>
        public string Name { get; set; }
    }
}
