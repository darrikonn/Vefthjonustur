namespace WebApplication.Services.Implementation {
    using System.Collections.Generic;
    using System.Linq;
    using WebApplication.Data;
    using WebApplication.Models.DTO.DTOs;
    using WebApplication.Models.EntityModels;
    using WebApplication.Models.DTO.ViewModels;

    public class CourseService {
        private readonly ApplicationDbContext _db;

        public CourseService(ApplicationDbContext context) {
            _db = context;
        }

#region PrivateFunctions
        private Course GetCourseFromDbById(int id) {
            var course = (from c in _db.Courses
                          where c.Id == id
                          select c).SingleOrDefault();
            return course;
        }

        private string GetNameOfCourse(string courseId) {
            var courseName = (from t in _db.CourseTemplates
                              where t.CourseId == courseId
                              select t.Name).SingleOrDefault();
            return courseName;
        }
#endregion

        public List<CourseDTO> GetCoursesOfSemester(int semester) {
            var courses = (from c in _db.Courses
                           where c.Semester == semester
                           select c).ToList();

            return courses.Select(c => new CourseDTO {
                CourseId = c.CourseId,
                Name = GetNameOfCourse(c.CourseId)
            }).ToList();
        }

        public CourseDetailsDTO GetCourseById(int id) {
            var course = GetCourseFromDbById(id);

            return new CourseDetailsDTO {
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId)
            };
        }

        public bool UpdateCourseById(int id, CourseViewModel model) {
            var course = GetCourseFromDbById(id);
            
            course.StartDate = model.StartDate.Value;
            course.EndDate = model.EndDate.Value;
            return true;
        }

        public bool DeleteCourseById(int id) {
            var course = GetCourseFromDbById(id);

            _db.Courses.Remove(course);
            return _db.SaveChanges() > 0; 
        }
    }
}
