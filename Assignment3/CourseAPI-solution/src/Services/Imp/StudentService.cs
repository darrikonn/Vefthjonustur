namespace CourseAPI.Services.Imp {
    using System.Linq;
    using System.Collections.Generic;
    using CourseAPI.Entities.Data;
    using CourseAPI.Entities.Models;
    using CourseAPI.Services.Itf;
    using CourseAPI.Services.Exceptions;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTOModels;

    /// <summary>
    /// This class implements the functions that the IStudentService interface specifies.
    /// This class is internal so it doesn't need XML documentation.
    /// </summary>
    public class StudentService : IStudentService {
        private readonly ApplicationDbContext _db;

        public StudentService(ApplicationDbContext context) {
            _db = context;
        }

#region PrivateFunctions
        private void ValidateUser(string ssn) {
            var student = (from s in _db.Students
                           where s.SSN == ssn
                           select s).SingleOrDefault();

            if (student == null) {
                throw new CustomObjectNotFoundException("Student not found!");
            }
        }

        private WaitingListLinker GetWaitingListLink(int id, string ssn) {
            var link = (from l in _db.WaitingListLinkers
                        where l.Id == id &&
                              l.SSN == ssn
                        select l).SingleOrDefault();

            return link;
        }

        private CourseStudentLinker GetCourseStudentLink(int id, string ssn) {
            var link = (from l in _db.CourseStudentLinkers
                        where l.Id == id &&
                              l.SSN == ssn
                        select l).SingleOrDefault();

            return link;
        }

        private void ValidateCSLink(int id, string ssn) {
            var link = GetCourseStudentLink(id, ssn);

            if (link != null) {
                throw new CustomConflictException("Link already exists!");
            }
        }

        private void ValidateWLLink(int id, string ssn) {
            var link = GetWaitingListLink(id, ssn);

            if (link != null) {
                throw new CustomConflictException("Link already exists!");
            }
        }

        private List<Student> GetStudentsEntitiesOfCourse(int id) {
            var students = (from s in _db.Students
                            join l in _db.CourseStudentLinkers on s.SSN equals l.SSN
                            where l.Id == id &&
                            l.IsActive
                            select s).ToList();

            return students;
        }
#endregion

        public List<StudentDTO> GetStudentsOfCourse(int id) {
            var students = GetStudentsEntitiesOfCourse(id);
            
            return students.Select(s => new StudentDTO {
                SSN = s.SSN,
                Name = s.Name
            }).ToList();
        }

        public List<StudentDTO> AddStudentToCourse(int id, LinkerViewModel model, int capacity) {
            ValidateUser(model.SSN);
            ValidateCSLink(id, model.SSN);

            if (GetStudentsEntitiesOfCourse(id).Count + 1 >= capacity) {
                throw new CustomForbiddenException("Capacity of course reached");
            }

            _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                SSN = model.SSN,
                Id = id
            });
            _db.SaveChanges();

            // remove from waiting list
            var link = GetWaitingListLink(id, model.SSN);
            if (link != null) {
                _db.WaitingListLinkers.Remove(link);
                _db.SaveChanges();
            }

            return GetStudentsOfCourse(id);
        }

        public List<StudentDTO> GetStudentsOfCourseWaitingList(int id) {
            var students = (from s in _db.Students
                            join w in _db.WaitingListLinkers on s.SSN equals w.SSN
                            where w.Id == id
                            select s).ToList();
            
            return students.Select(s => new StudentDTO {
                SSN = s.SSN,
                Name = s.Name
            }).ToList();
        }

        public List<StudentDTO> AddStudentToCourseWaitingList(int id, LinkerViewModel model) {
            ValidateUser(model.SSN);
            ValidateWLLink(id, model.SSN);

            _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                SSN = model.SSN,
                Id = id
            });
            _db.SaveChanges();

            return GetStudentsOfCourseWaitingList(id);
        }

        public bool DeleteStudentFromCourse(int id, LinkerViewModel model) {
            var link = GetCourseStudentLink(id, model.SSN);
            if (link == null) {
                throw new CustomObjectNotFoundException("Not found maaan!");
            }

            link.IsActive = false;
            return _db.SaveChanges() > 0;
        }
    }
}
