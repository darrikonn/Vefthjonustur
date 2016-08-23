namespace WebApplication.Models.ViewModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CourseViewModel {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TemplateId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
