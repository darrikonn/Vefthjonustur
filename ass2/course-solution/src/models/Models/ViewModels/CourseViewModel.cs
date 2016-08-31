namespace Models.Models.ViewModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CourseViewModel {
        [Required]
        public string CourseId { get; set; }
        [Required]
        public int Semester { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
