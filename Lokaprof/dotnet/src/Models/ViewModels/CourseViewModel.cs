namespace WebApplication.Models.ViewModels {
  using System.ComponentModel.DataAnnotations;
  
  public class CourseViewModel {
    [Required]
    public string Course { get; set; }
    [Required]
    public string Teacher { get; set; }
  }
}
