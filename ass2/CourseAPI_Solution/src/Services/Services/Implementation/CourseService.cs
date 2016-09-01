namespace CourseAPI.Services.Services.Implementation {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CourseAPI.Services.Data;
    using CourseAPI.Services.Entities.EntityModels;
    using CourseAPI.Services.Services.Interface;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTO;

    /// <summary>
    /// This class implements the functions that the ICourseService interface specifies.
    /// This class is internal so it doesn't need XML documentation.
    /// </summary>
    public class CourseService : ICourseService {
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

            if (course == null) {
                return null;
            }

            return new CourseDetailsDTO {
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId)
            };
        }

        public CourseDetailsDTO UpdateCourseById(int id, CourseViewModel model) {
            var course = GetCourseFromDbById(id);
            
            course.StartDate = model.StartDate.Value;
            course.EndDate = model.EndDate.Value;

            _db.SaveChanges();

            return new CourseDetailsDTO {
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId)
            };
        }

        public bool DeleteCourseById(int id) {
            var course = GetCourseFromDbById(id);

            _db.Courses.Remove(course);
            return _db.SaveChanges() > 0; 
        }

        public int AddCourse(CourseViewModel model) {
            try {
                var id  = _db.Courses.Any() ?
                    _db.Courses.Max(c => c.Id) + 1 :
                    0;

                var course = new Course {
                    Id = id,
                       Semester = model.Semester,
                       CourseId = model.CourseId,
                       EndDate = model.EndDate.HasValue ?
                           model.EndDate.Value : DateTime.Today,
                       StartDate = model.StartDate.HasValue ?
                           model.StartDate.Value : DateTime.Today
                };

                _db.Courses.Add(course);
                _db.SaveChanges();

                return id;
            } catch(Exception) {
                return -1;
            }
        }
    }
}
