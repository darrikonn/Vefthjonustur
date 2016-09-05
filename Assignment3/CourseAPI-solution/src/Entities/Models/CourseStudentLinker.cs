namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// This class represents the link between a course and a student.
    /// The class gets mapped to a table in the database.
    /// The keys and foreign keys are added using fluent API.
    /// </summary>
    public class CourseStudentLinker {
        /// <summary>
        /// The integer id of a course.
        /// This is a key in this table.
        /// Example:
        ///     1) 1
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The social security number of a student.
        /// This is a key in this table.
        /// Example:
        ///     1) 1501933119
        /// </summary>
        public string SSN { get; set; }

        public bool IsActive { get; set; }

#region ForeignKeys
        /// <summary>
        /// The foreign key from SSN in CourseStudentLinker equals SSN in Student.
        /// </summary>
        [ForeignKey("SSN")]
        public virtual Student Student { get; set; }

        /// <summary>
        /// The foreign key from Id in CourseStudentLinker equals Id in Course.
        /// </summary>
        [ForeignKey("Id")]
        public virtual Course Course { get; set; }
#endregion

        public CourseStudentLinker() {
            IsActive = true;
        }
    }
}
