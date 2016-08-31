namespace Services.Services.Implementation {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models.EntityModels;
    using Interface;

    public class CourseService : ICourseService {
        private readonly ApplicationDbContext _db;

        public CourseService(ApplicationDbContext context) {
            _db = context;
        }

        public List<Course> GetCoursesOfSemester(int semester) {
            var courses = (from c in _db.Courses
                           where c.Semester == semester
                           select c).ToList();

            return courses;
        }

        public Course GetCourseById(int id) {
            var course = (from c in _db.Courses
                          where c.Id == id
                          select c).SingleOrDefault();

            return course;
        }

        public string GetNameOfCourse(string courseId) {
            var courseName = (from t in _db.CourseTemplates
                              where t.CourseId == courseId
                              select t.Name).SingleOrDefault();
            return courseName;
        }

        public bool DeleteCourseById(int id) {
            var course = GetCourseById(id);

            _db.Courses.Remove(course);
            return _db.SaveChanges() > 0; 
        }
/*
        public Course UpdateCourseById(int id, CourseViewModel model) {
            var course = GetCourseById(id);
            
            course.StartDate = model.StartDate.Value;
            course.EndDate = model.EndDate.Value;

            _db.SaveChanges();

            return new CourseDetailsDTO {
                CourseId = course.CourseId, 
                Name = GetNameOfCourse(course.CourseId)
            };
        }

        public int AddCourse(CourseViewModel model) {
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
        }*/
    }
}
