namespace WebApplication.Entities.Models {
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class Course {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    [ForeignKey("TeacherId")]
    public virtual Teacher Teacher { get; set; }
  }
}
