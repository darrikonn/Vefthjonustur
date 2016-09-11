namespace CourseAPI.Entities.Models {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /*
     * This class maps the course table to the database.
     * This class is internal.
     */
    public class Course {
        [Key]
        public int Id { get; set; }

        public string TemplateId { get; set; }

        public string Semester { get; set; }

        public int MaxStudents { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

#region ForeignKey
        [ForeignKey("TemplateId")]
        public virtual CourseTemplate CourseTemplate { get; set; }
#endregion
    }
}
