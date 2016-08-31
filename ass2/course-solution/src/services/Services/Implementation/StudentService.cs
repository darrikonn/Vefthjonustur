namespace Services.Services.Implementation {
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Entities.EntityModels;
    using Interface;
    using Models.Models.ViewModels;
    using Models.Models.DTO;

    public class StudentService : IStudentService {
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

        public List<StudentDTO> AddStudentToCourse(int id, LinkerViewModel model) {
            _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                SSN = model.SSN,
                Id = id
            });
            _db.SaveChanges();

            return GetStudentsOfCourse(id);
        }
    }
}
