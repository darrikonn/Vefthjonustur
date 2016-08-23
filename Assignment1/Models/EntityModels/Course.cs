namespace WebApplication.Models.EntityModels {
    using System;
    using System.Collections.Generic;

    public class Course {
        public string Name { get; set; }
        public string TemplateId { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> Students { get; set; }
    }
}
