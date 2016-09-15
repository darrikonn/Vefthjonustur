using System;
using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Services {
	public class CoursesServiceProvider {
		private readonly IUnitOfWork _uow;

		private readonly IRepository<CourseInstance> _courseInstances;
		private readonly IRepository<TeacherRegistration> _teacherRegistrations;
		private readonly IRepository<CourseTemplate> _courseTemplates; 
		private readonly IRepository<Person> _persons;

		public CoursesServiceProvider(IUnitOfWork uow) {
			_uow = uow;

			_courseInstances      = _uow.GetRepository<CourseInstance>();
			_courseTemplates      = _uow.GetRepository<CourseTemplate>();
			_teacherRegistrations = _uow.GetRepository<TeacherRegistration>();
			_persons              = _uow.GetRepository<Person>();
		}

        private string GetNameOfPerson(string ssn) {
            return (from p in _persons.All()
                    where p.SSN == ssn
                    select p.Name).DefaultIfEmpty("").Single();
        }

		/// <summary>
		/// You should implement this function, such that all tests will pass.
		/// </summary>
		/// <param name="courseInstanceID">The ID of the course instance which the teacher will be registered to.</param>
		/// <param name="model">The data which indicates which person should be added as a teacher, and in what role.</param>
		/// <returns>Should return basic information about the person.</returns>
        public PersonDTO AddTeacherToCourse(int courseInstanceID, AddTeacherViewModel model) {
            // check if a course exists
            var course = (from c in _courseInstances.All()
                    where c.ID == courseInstanceID
                    select c).SingleOrDefault();
            if (course == null) {
                throw new AppObjectNotFoundException();
            }
            // check if a person exists
            var person = (from t in _persons.All()
                    where t.SSN == model.SSN
                    select t).SingleOrDefault();
            if (person == null) {
                throw new AppObjectNotFoundException();
            }
            // check if a course has a main teacher
            if (model.Type == TeacherType.MainTeacher) {
                var registration = (from r in _teacherRegistrations.All()
                        where r.CourseInstanceID == courseInstanceID &&
                        r.Type == TeacherType.MainTeacher
                        select r).SingleOrDefault();
                if (registration != null) {
                    throw new AppValidationException("COURSE_ALREADY_HAS_A_MAIN_TEACHER");
                }
            }
            // check if a teacher is teaching this course
            var teacher = (from t in _teacherRegistrations.All()
                    where t.CourseInstanceID == courseInstanceID &&
                    t.SSN == model.SSN
                    select t).SingleOrDefault();
            if (teacher != null) {
                throw new AppValidationException("PERSON_ALREADY_REGISTERED_TEACHER_IN_COURSE");
            }

            // validation passed, try to add the teacher to the course
            try {
                _teacherRegistrations.Add(new TeacherRegistration {
                        SSN = model.SSN,
                        CourseInstanceID = courseInstanceID,
                        Type = model.Type
                        });

                _uow.Save();
            } catch(Exception e) {
                throw new Exception(e.Message);
            }

            return new PersonDTO {
                SSN = person.SSN,
                    Name = person.Name
            };
        }

		/// <summary>
		/// You should write tests for this function. You will also need to
		/// modify it, such that it will correctly return the name of the main
		/// teacher of each course.
		/// </summary>
		/// <param name="semester"></param>
		/// <returns></returns>
        public List<CourseInstanceDTO> GetCourseInstancesBySemester(string semester = null) {
            if (string.IsNullOrEmpty(semester))
            {
                semester = "20153";
            }

            var courses = (from c in _courseInstances.All()
                    join ct in _courseTemplates.All() on c.CourseID equals ct.CourseID
                    join tr in _teacherRegistrations.All() on c.ID equals tr.CourseInstanceID into reges
                    from r in reges.Where(x => x.Type == TeacherType.MainTeacher).DefaultIfEmpty()
                    where c.SemesterID == semester
                    select new CourseInstanceDTO
                    {
                    Name               = ct.Name,
                    TemplateID         = ct.CourseID,
                    CourseInstanceID   = c.ID,
                    MainTeacher        = GetNameOfPerson(r == null ? "" : r.SSN)
                    }).ToList();

            return courses;
        }
	}
}
