namespace WebApplication.Services.Implementation {
    using System.Linq;
    using System.Collections.Generic;
    using WebApplication.Data;
    using WebApplication.Models.DTO.DTOs;
    using WebApplication.Models.EntityModels;
    using WebApplication.Models.DTO.ViewModels;

    public class StudentService {
        private readonly ApplicationDbContext _db;

        public StudentService(ApplicationDbContext context) {
            _db = context;
        }

        public List<StudentDTO> GetStudentsOfCourse(int id) {
            var students = (from s in _db.Students
                            join l in _db.CourseStudentLinkers on s.SSN equals l.SSN
                            where l.Id == id
                            select s).ToList();

            return students.Select(s => new StudentDTO {
                SSN = s.SSN,
                Name = s.Name
            }).ToList();
        }
    }
}
