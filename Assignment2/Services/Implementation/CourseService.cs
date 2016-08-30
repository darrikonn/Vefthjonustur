namespace WebApplication.Services.Implementation {
    using System.Collections.Generic;
    using WebApplication.Data;
    using WebApplication.Models.DTO.DTOs;

    public class CourseService {
        private readonly ApplicationDbContext _db;

        public CourseService(ApplicationDbContext context) {
            _db = context;
        }

        public List<CourseDTO> GetCoursesOfSemester(string semester) {
            return null;
        }
    }
}
