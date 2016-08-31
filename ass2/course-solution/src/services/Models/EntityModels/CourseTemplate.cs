namespace Services.Models.EntityModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourseTemplate {
        [Key]
        public string CourseId { get; set; }
        public string Name { get; set; }
    }
}
