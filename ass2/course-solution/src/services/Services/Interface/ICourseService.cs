namespace Services.Services.Interface {
    using System.Collections.Generic;
    using Models.Models.ViewModels;
    using Models.Models.DTO;

    public interface ICourseService {
        List<CourseDTO> GetCoursesOfSemester(int semester);
        CourseDetailsDTO GetCourseById(int id);
        CourseDetailsDTO UpdateCourseById(int id, CourseViewModel model);
        bool DeleteCourseById(int id);
        int AddCourse(CourseViewModel model);
    }
}
