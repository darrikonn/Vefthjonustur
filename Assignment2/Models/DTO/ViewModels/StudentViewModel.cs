namespace WebApplication.Models.DTO.ViewModels {
    using System.ComponentModel.DataAnnotations;

    public class StudentViewModel {
        [Required]
        public string SSN { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
