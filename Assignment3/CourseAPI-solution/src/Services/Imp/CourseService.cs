namespace CourseAPI.Services.Imp {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CourseAPI.Entities.Data;
    using CourseAPI.Entities.Models;
    using CourseAPI.Services.Exceptions;
    using CourseAPI.Services.Itf;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTOModels;

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

        public List<CourseListDTO> GetCoursesOfSemester(string semester) {
            var courses = (from c in _db.Courses
                           where c.Semester == semester
                           select c).ToList();
                
            return courses.Select(c => new CourseListDTO {
                Id = c.Id,
                CourseId = c.CourseId,
                Name = GetNameOfCourse(c.CourseId)
            }).ToList();
        }

        public CourseDetailsDTO GetCourseById(int id) {
            var course = GetCourseFromDbById(id);

            if (course == null) {
                throw new CustomObjectNotFoundException("Course does not exist!\n");
            }

            return new CourseDetailsDTO {
                Id = course.Id,
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId),
                MaxStudents = course.MaxStudents
            };
        }

        public CourseDetailsDTO UpdateCourseById(int id, CourseViewModel model) {
            var course = GetCourseFromDbById(id);

            if (course == null) {
                throw new CustomObjectNotFoundException("Course does not exist!\n");
            }

            course.CourseId = model.TemplateId;
            course.Semester = model.Semester;
            
            if (model.StartDate.HasValue) {
                course.StartDate = model.StartDate.Value;
            }
            if (model.EndDate.HasValue) {
                course.EndDate = model.EndDate.Value;
            }

            _db.SaveChanges();

            return new CourseDetailsDTO {
                Id = course.Id,
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId)
            };
        }

        public bool DeleteCourseById(int id) {
            var course = GetCourseFromDbById(id);

            if (course == null) {
                throw new CustomObjectNotFoundException("Course does not exist!\n");
            }

            _db.Courses.Remove(course);
            return _db.SaveChanges() > 0;
        }

        public int AddCourse(CourseViewModel model) {
            try {
                var id  = _db.Courses.Any() ?
                    _db.Courses.Max(c => c.Id) + 1 :
                    1;

                var course = new Course {
                    Id = id,
                    Semester = model.Semester,
                    CourseId = model.TemplateId,
                    MaxStudents = model.MaxStudents,
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
