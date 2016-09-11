namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /*
     * This class maps the course template table to the database.
     * This class is internal.
     */
    public class CourseTemplate {
        [Key]
        public string TemplateId { get; set; }

        public string Name { get; set; }
    }
}
