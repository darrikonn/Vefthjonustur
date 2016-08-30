namespace WebApplication.Models.EntityModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourseStudentLinker {
        [Key]
        [ForeignKey("Student")]
        public string SSN { get; set; }
        [Key]
        [ForeignKey("Course")]
        public int Id { get; set; }

#region ForeignKeys
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
#endregion
    }
}
