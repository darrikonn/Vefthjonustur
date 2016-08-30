namespace WebApplication.Models.EntityModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Course {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
