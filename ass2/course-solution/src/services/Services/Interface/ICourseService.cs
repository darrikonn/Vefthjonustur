namespace Services.Services.Interface {
    using System.Collections.Generic;
    using Models.EntityModels;

    public interface ICourseService {
        List<Course> GetCoursesOfSemester(int semester);
        Course GetCourseById(int id);
        string GetNameOfCourse(string courseId);
        bool DeleteCourseById(int id);
    }
}
