namespace CourseAPI.Entities.Models {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /*
     * This class maps the waiting list table to the database.
     * This class is internal.
     * Using fluent api for double key attributes
     */
    public class WaitingListLinker {
        // Key
        public int Id { get; set; }

        // Key
        public string SSN { get; set; }

#region ForeignKeys
        [ForeignKey("SSN")]
        public virtual Student Student { get; set; }

        [ForeignKey("Id")]
        public virtual Course Course { get; set; }
#endregion
    }
}
