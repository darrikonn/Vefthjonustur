namespace CourseAPI.Services.Imp {
    using System.Linq;
    using System.Collections.Generic;
    using CourseAPI.Entities.Data;
    using CourseAPI.Entities.Models;
    using CourseAPI.Services.Itf;
    using CourseAPI.Services.Exceptions;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTOModels;

    /*
     * This class implements the functions that the IStudentService interface specifies.
     * This class is internal.
     */
    public class StudentService : IStudentService {
        private readonly ApplicationDbContext _db;

        public StudentService(ApplicationDbContext context) {
            _db = context;
        }

#region PrivateFunctions
        private Student GetStudentBySSN(string ssn) {
            var student = (from s in _db.Students
                           where s.SSN == ssn
                           select s).SingleOrDefault();

            return student;
        }

        private void ValidateUser(string ssn) {
            var student = GetStudentBySSN(ssn);

            if (student == null) {
                throw new CustomObjectNotFoundException("Student not found!\n");
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

        private void ValidateWLLink(int id, string ssn) {
            var link = GetWaitingListLink(id, ssn);

            if (link != null) {
                throw new CustomConflictException("Person already on waiting list!\n");
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

        public StudentDTO AddStudentToCourse(int id, LinkerViewModel model, int capacity) {
            var student = GetStudentBySSN(model.SSN);
            if (student == null) {
                throw new CustomObjectNotFoundException("Student not found!\n");
            }

            if (GetStudentsEntitiesOfCourse(id).Count >= capacity) {
                throw new CustomForbiddenException("Max students reached\n");
            }

            var csLink = GetCourseStudentLink(id, model.SSN);
            if (csLink != null) {
                if (csLink.IsActive) {
                    throw new CustomConflictException($"{student.Name} is already enrolled as a student\n");
                }
                csLink.IsActive = true;
            } else {
                _db.CourseStudentLinkers.Add(new CourseStudentLinker {
                    SSN = model.SSN,
                    Id = id
                });
            }

            _db.SaveChanges();

            // remove from waiting list
            var wlLink = GetWaitingListLink(id, model.SSN);
            if (wlLink != null) {
                _db.WaitingListLinkers.Remove(wlLink);
                _db.SaveChanges();
            }

            return new StudentDTO {
                Name = student.Name   
            };
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

        public StudentDTO AddStudentToCourseWaitingList(int id, LinkerViewModel model) {
            var student = GetStudentBySSN(model.SSN);
            if (student == null) {
                throw new CustomObjectNotFoundException("Student not found!\n");
            }

            ValidateWLLink(id, model.SSN);

            var csLink = GetCourseStudentLink(id, model.SSN);
            if (csLink != null && csLink.IsActive) {
                throw new CustomForbiddenException($"{student.Name} is already enrolled as a student\n");
            }

            _db.WaitingListLinkers.Add(new WaitingListLinker {
                SSN = model.SSN,
                Id = id
            });
            _db.SaveChanges();

            return new StudentDTO {
                Name = student.Name   
            };
        }

        public bool DeleteStudentFromCourse(int id, string ssn) {
            ValidateUser(ssn);

            var link = GetCourseStudentLink(id, ssn);
            if (link == null) {
                throw new CustomObjectNotFoundException("Student does not exist in this course!\n");
            }

            link.IsActive = false;
            return _db.SaveChanges() > 0;
        }
    }
}
