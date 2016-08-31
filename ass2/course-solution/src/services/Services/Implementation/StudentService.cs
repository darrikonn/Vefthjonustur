namespace Services.Services.Implementation {
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Models.EntityModels;
    using Interface;

    public class StudentService : IStudentService {
        private readonly ApplicationDbContext _db;

        public StudentService(ApplicationDbContext context) {
            _db = context;
        }

        public List<Student> GetStudentsOfCourse(int id) {
            var students = (from s in _db.Students
                            join l in _db.CourseStudentLinkers on s.SSN equals l.SSN
                            where l.Id == id
                            select s).ToList();

            return students;
        }

        public List<Student> AddStudentToCourse(int id, string ssn) {
            _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                SSN = ssn,
                Id = id
            });
            _db.SaveChanges();

            return GetStudentsOfCourse(id);
        }
    }
}
