namespace WebApplication.Models.EntityModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourseTemplate {
        [Key]
        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public string Name { get; set; }

#region ForeignKeys
        public virtual Course Course { get; set; }
#endregion
    }
}
