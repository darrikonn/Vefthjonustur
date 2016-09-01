namespace CourseAPI.Models.ViewModels {
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This class represents the link between a course and a student.
    /// Used when adding a student to a course.
    /// </summary>
    public class LinkerViewModel {
        /// <summary>
        /// The social security number of a student.
        /// Required when adding a student to a course.
        /// Example:
        ///     1) 1501933119
        /// </summary>
        [Required]
        public string SSN { get; set; }

        /// <summary>
        /// The integer id of a course.
        /// Required when adding a student to a course.
        /// Example:
        ///     1) 1
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
