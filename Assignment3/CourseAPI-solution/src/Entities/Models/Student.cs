namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This class represents the student.
    /// The class gets mapped to the database.
    /// </summary>
    public class Student {
        /// <summary>
        /// The social security number of a student.
        /// This is the key in this table.
        /// Example:
        ///     1) 1501933119
        /// </summary>
        [Key]
        public string SSN { get; set; }
        
        /// <summary>
        /// The name of a student.
        /// Example:
        ///     1) Darri Steinn Konradsson
        /// </summary>
        public string Name { get; set; }
    }
}
