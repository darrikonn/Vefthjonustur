namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// This class represents the mapping between a course name and the course string id.
    /// The class gets mapped to a table in the database.
    /// </summary>
    public class CourseTemplate {
        /// <summary>
        /// The string id of a course.
        /// This is the key in the table.
        /// Example: 
        ///     1) T-514-VEFT
        /// </summary>
        [Key]
        public string CourseId { get; set; }

        /// <summary>
        /// The name of a course.
        /// Example:
        ///     1) Web Services
        /// </summary>
        public string Name { get; set; }
    }
}
