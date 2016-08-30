namespace WebApplication.Models.EntityModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Course {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
