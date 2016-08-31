namespace Services.Entities.EntityModels {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CourseStudentLinker {
        public int Id { get; set; }
        public string SSN { get; set; }

#region ForeignKeys
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
#endregion
    }
}
