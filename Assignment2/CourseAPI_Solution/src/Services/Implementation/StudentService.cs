namespace CourseAPI.Services.Implementation {
    using System.Linq;
    using System.Collections.Generic;
    using CourseAPI.Entities.Data;
    using CourseAPI.Entities.Models;
    using CourseAPI.Services.Interface;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTO;

    /// <summary>
    /// This class implements the functions that the IStudentService interface specifies.
    /// This class is internal so it doesn't need XML documentation.
    /// </summary>
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
            var link = (from l in _db.CourseStudentLinkers
                        where l.Id == id &&
                              l.SSN == model.SSN
                        select l).SingleOrDefault();
            if (link != null) {
                return null;
            }

            _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                SSN = model.SSN,
                Id = id
            });
            _db.SaveChanges();

            return GetStudentsOfCourse(id);
        }
    }
}
