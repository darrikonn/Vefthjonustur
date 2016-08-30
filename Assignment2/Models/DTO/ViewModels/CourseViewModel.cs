namespace WebApplication.Models.DTO.ViewModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CourseViewModel {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
