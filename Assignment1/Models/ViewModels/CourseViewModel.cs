namespace WebApplication.Models.ViewModels {
    using System;

    public class CourseViewModel {
        public string Name { get; set; }
        public string TemplateId { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
