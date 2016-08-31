namespace Services.Entities.EntityModels {
    using System.ComponentModel.DataAnnotations;

    public class Student {
        [Key]
        public string SSN { get; set; }
        public string Name { get; set; }
    }
}
