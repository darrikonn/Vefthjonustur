namespace WebApplication.Services.Interfaces {
  using System.Collections.Generic;
  using WebApplication.Models.DTOs;
  using WebApplication.Models.ViewModels;

  public interface ICourseService {
    List<CourseInstance> getCourses();
    CourseDTO getCourse(int id);
    int addCourse(CourseViewModel model);
    TeacherDTO getTeacher(int id);
  }
}
