namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;

    /*
     * This class maps the student table to the database.
     * This class is internal.
     */
    public class Student {
        [Key]
        public string SSN { get; set; }

        public string Name { get; set; }
    }
}
