namespace Models.Models.ViewModels {
    using System.ComponentModel.DataAnnotations;

    public class LinkerViewModel {
        [Required]
        public string SSN { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
