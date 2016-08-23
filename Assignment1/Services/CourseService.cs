namespace WebApplication.Services {
    using WebApplication.Models.EntityModels;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService {
        private List<Course> _courses;

        public CourseService(List<Course> courses) {
            _courses = courses;
        }

        public Course GetCourseById(int id) {
            var data = (from c in _courses
                        where c.Id == id
                        select c).SingleOrDefault();
            return data;
        }
        
    }
}
