﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoursesAPI.Models;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.Services;
using CoursesAPI.Tests.MockObjects;
using Xunit;

namespace CoursesAPI.Tests.Services {
	public class CourseServicesTests {
		private MockUnitOfWork<MockDataContext> _mockUnitOfWork;
		private CoursesServiceProvider _service;
		private List<TeacherRegistration> _teacherRegistrations;

		private const string SSN_DABS    = "1203735289";
		private const string SSN_GUNNA   = "1234567890";
		private const string SSN_DAVID   = "2101932879";
		private const string SSN_DARRI   = "1501933119";
		private const string INVALID_SSN = "9876543210";

		private const string NAME_GUNNA  = "Guðrún Guðmundsdóttir";
        private const string NAME_DABS   = "Daníel B. Sigurgeirsson";
        private const string NAME_DAVID  = "Davíð Guðni Halldórsson";
        private const string NAME_DARRI  = "Darri Steinn Konráðsson";

        private const string VEFT_TEMPLATE_NAME = "Vefþjónustur";
        private const string VEFT_TEMPLATE_ID = "T-514-VEFT";
        private const string PROG_TEMPLATE_NAME = "Forritun 1";
        private const string PROG_TEMPLATE_ID = "T-112-PROG";
        private const string GAGN_TEMPLATE_NAME = "Gagnaskipan";
        private const string GAGN_TEMPLATE_ID = "T-113-GAGN";

		private const int COURSEID_VEFT_20153 = 1337;
		private const int COURSEID_VEFT_20163 = 1338;
		private const int COURSEID_PROG_20143 = 1339;
		private const int COURSEID_PROG_20163 = 1340;
		private const int COURSEID_GAGN_20153 = 1341;
		private const int COURSEID_GAGN_20163 = 1342;
		private const int INVALID_COURSEID    = 9999;

		public CourseServicesTests() {
			_mockUnitOfWork = new MockUnitOfWork<MockDataContext>();

			#region Persons
			var persons = new List<Person> {
				// Of course I'm the first person,
				// did you expect anything else?
				new Person {
					ID    = 1,
					Name  = NAME_DABS,
					SSN   = SSN_DABS,
					Email = "dabs@ru.is"
				},
				new Person {
					ID    = 2,
					Name  = NAME_GUNNA,
					SSN   = SSN_GUNNA,
					Email = "gunna@ru.is"
				},
				new Person {
					ID    = 3,
					Name  = NAME_DAVID,
					SSN   = SSN_DAVID,
					Email = "davidgh13@ru.is"
				},
				new Person {
					ID    = 4,
					Name  = NAME_DARRI,
					SSN   = SSN_DARRI,
					Email = "darrik13@ru.is"
				}
			};
			#endregion

			#region Course templates

			var courseTemplates = new List<CourseTemplate> {
				new CourseTemplate {
					CourseID    = VEFT_TEMPLATE_ID,
					Description = "Í þessum áfanga verður fjallað um vefþj...",
					Name        = VEFT_TEMPLATE_NAME
				},
				new CourseTemplate {
					CourseID    = PROG_TEMPLATE_ID,
					Description = "Í þessum áfanga verður forritad slatta...",
					Name        = PROG_TEMPLATE_NAME
				},
				new CourseTemplate {
					CourseID    = GAGN_TEMPLATE_ID,
					Description = "Í þessum áfanga verður forritad i drasl...",
					Name        = GAGN_TEMPLATE_NAME
				}
			};
			#endregion

			#region Courses
			var courses = new List<CourseInstance> {
				new CourseInstance {
					ID         = COURSEID_VEFT_20153,
					CourseID   = VEFT_TEMPLATE_ID,
					SemesterID = "20153"
				},
				new CourseInstance {
					ID         = COURSEID_VEFT_20163,
					CourseID   = VEFT_TEMPLATE_ID,
					SemesterID = "20163"
				},
				new CourseInstance {
					ID         = COURSEID_GAGN_20153,
					CourseID   = GAGN_TEMPLATE_ID,
					SemesterID = "20153"
				},
				new CourseInstance {
					ID         = COURSEID_GAGN_20163,
					CourseID   = GAGN_TEMPLATE_ID,
					SemesterID = "20163"
				},
				new CourseInstance {
					ID         = COURSEID_PROG_20143,
					CourseID   = PROG_TEMPLATE_ID,
					SemesterID = "20143"
				},
				new CourseInstance {
					ID         = COURSEID_PROG_20163,
					CourseID   = PROG_TEMPLATE_ID,
					SemesterID = "20163"
				}
		    };
			#endregion

			#region Teacher registrations
			_teacherRegistrations = new List<TeacherRegistration> {
				new TeacherRegistration {
					ID               = 101,
					CourseInstanceID = COURSEID_VEFT_20153,
					SSN              = SSN_DABS,
					Type             = TeacherType.MainTeacher
				},
				new TeacherRegistration {
					ID               = 102,
					CourseInstanceID = COURSEID_PROG_20163,
					SSN              = SSN_DARRI,
					Type             = TeacherType.AssistantTeacher
				},
				new TeacherRegistration {
					ID               = 103,
					CourseInstanceID = COURSEID_PROG_20163,
					SSN              = SSN_DAVID,
					Type             = TeacherType.AssistantTeacher
				},
				new TeacherRegistration {
					ID               = 104,
					CourseInstanceID = COURSEID_PROG_20143,
					SSN              = SSN_DAVID,
					Type             = TeacherType.MainTeacher
				},
				new TeacherRegistration {
					ID               = 105,
					CourseInstanceID = COURSEID_GAGN_20163,
					SSN              = SSN_DARRI,
					Type             = TeacherType.MainTeacher
				}
			};
			#endregion

			_mockUnitOfWork.SetRepositoryData(persons);
			_mockUnitOfWork.SetRepositoryData(courseTemplates);
			_mockUnitOfWork.SetRepositoryData(courses);
			_mockUnitOfWork.SetRepositoryData(_teacherRegistrations);

			_service = new CoursesServiceProvider(_mockUnitOfWork);
		}

        #region GetCoursesBySemester
        /// <summary>
        /// The function should return all courses on a given semester (no more, no less!)
        /// </summary>
        [Fact]
        public void GetCoursesBySemester_ReturnsAllCoursesOfSemester() {
            // Arrange:
            string semester0 = "20163",
                   semester1 = "20013";

            // Act:
            var result0 = _service.GetCourseInstancesBySemester(semester0);
            var result1 = _service.GetCourseInstancesBySemester(semester1);

            // Assert:
            Assert.Equal(3, result0.Count);
            var veft = result0.SingleOrDefault(x => x.TemplateID == VEFT_TEMPLATE_ID);
            var prog = result0.SingleOrDefault(x => x.TemplateID == PROG_TEMPLATE_ID);
            var gagn = result0.SingleOrDefault(x => x.TemplateID == GAGN_TEMPLATE_ID);
            Assert.Equal(COURSEID_VEFT_20163, veft.CourseInstanceID);
            Assert.Equal(VEFT_TEMPLATE_NAME, veft.Name);
            Assert.Equal(VEFT_TEMPLATE_ID, veft.TemplateID);
            Assert.Equal(COURSEID_PROG_20163, prog.CourseInstanceID);
            Assert.Equal(PROG_TEMPLATE_NAME, prog.Name);
            Assert.Equal(PROG_TEMPLATE_ID, prog.TemplateID);
            Assert.Equal(COURSEID_GAGN_20163, gagn.CourseInstanceID);
            Assert.Equal(GAGN_TEMPLATE_NAME, gagn.Name);
            Assert.Equal(GAGN_TEMPLATE_ID, gagn.TemplateID);

            Assert.Equal(0, result1.Count);
        }

        /// <summary>
        /// If no semester is defined, it should return all courses for the semester 20153
        /// </summary>
        [Fact]
        public void GetCoursesBySemester_ReturnsCoursesOf2053WhenNoDataIsDefined() {
            // Arrange:

            // Act:
            var result0 = _service.GetCourseInstancesBySemester();

            // Assert:
            Assert.Equal(2, result0.Count);
            var veft = result0.SingleOrDefault(x => x.TemplateID == VEFT_TEMPLATE_ID);
            var gagn = result0.SingleOrDefault(x => x.TemplateID == GAGN_TEMPLATE_ID);
            Assert.Equal(COURSEID_VEFT_20153, veft.CourseInstanceID);
            Assert.Equal(VEFT_TEMPLATE_NAME, veft.Name);
            Assert.Equal(VEFT_TEMPLATE_ID, veft.TemplateID);
            Assert.Equal(COURSEID_GAGN_20153, gagn.CourseInstanceID);
            Assert.Equal(GAGN_TEMPLATE_NAME, gagn.Name);
            Assert.Equal(GAGN_TEMPLATE_ID, gagn.TemplateID);
        }

        /// <summary>
        /// For each course returned, the name of the main teacher of the course should be included 
        /// (see the definition of CourseInstanceDTO).
        /// </summary>
        [Fact]
        public void GetCoursesBySemester_ReturnsTheMainTeachersOfEachCourse() {
            // Arrange:
            string semester0 = "20153",
                   semester1 = "20143",
                   semester2 = "20163";

            // Act:
            var result0 = _service.GetCourseInstancesBySemester(semester0);
            var result1 = _service.GetCourseInstancesBySemester(semester1);
            var result2 = _service.GetCourseInstancesBySemester(semester2);

            // Assert:
            Assert.Equal(2, result0.Count);
            var veft = result0.SingleOrDefault(x => x.TemplateID == VEFT_TEMPLATE_ID);
            Assert.Equal(NAME_DABS, veft.MainTeacher);
            Assert.Equal(1, result1.Count);
            Assert.Equal(NAME_DAVID, result1[0].MainTeacher);
            Assert.Equal(3, result2.Count);
            var gagn = result2.SingleOrDefault(x => x.TemplateID == GAGN_TEMPLATE_ID);
            Assert.Equal(NAME_DARRI, gagn.MainTeacher);
        }

        /// <summary>
        /// If the main teacher hasn't been defined, the name of the main teacher should be returned as 
        /// an empty string.
        /// </summary>
        [Fact]
        public void GetCoursesBySemester_ReturnsEmptyStringForNoMainTeacherOfCourse() {
            // Arrange:
            string semester0 = "20163",
                   semester1 = "20153";

            // Act:
            var result0 = _service.GetCourseInstancesBySemester(semester0);
            var result1 = _service.GetCourseInstancesBySemester(semester1);

            // Assert:
            Assert.Equal(3, result0.Count);
            var prog = result0.SingleOrDefault(x => x.TemplateID == PROG_TEMPLATE_ID);
            var veft = result0.SingleOrDefault(x => x.TemplateID == VEFT_TEMPLATE_ID);
            Assert.Equal("", prog.MainTeacher);
            Assert.Equal("", veft.MainTeacher);
            Assert.Equal(2, result1.Count);
            var gagn = result1.SingleOrDefault(x => x.TemplateID == GAGN_TEMPLATE_ID);
            Assert.Equal("", gagn.MainTeacher);
        }
        #endregion

		#region AddTeacher

		/// <summary>
		/// Adds a main teacher to a course which doesn't have a
		/// main teacher defined already (see test data defined above).
		/// </summary>
		[Fact]
		public void AddTeacher_WithValidTeacherAndCourse() {
			// Arrange:
			var model = new AddTeacherViewModel {
				SSN  = SSN_GUNNA,
				Type = TeacherType.MainTeacher
			};
			var prevCount = _teacherRegistrations.Count;
			// Note: the method uses test data defined in [TestInitialize]

			// Act:
			var dto = _service.AddTeacherToCourse(COURSEID_VEFT_20163, model);

			// Assert:

			// Check that the dto object is correctly populated:
			Assert.Equal(SSN_GUNNA, dto.SSN);
			Assert.Equal(NAME_GUNNA, dto.Name);

			// Ensure that a new entity object has been created:
			var currentCount = _teacherRegistrations.Count;
			Assert.Equal(prevCount + 1, currentCount);

			// Get access to the entity object and assert that
			// the properties have been set:
			var newEntity = _teacherRegistrations.Last();
			Assert.Equal(COURSEID_VEFT_20163, newEntity.CourseInstanceID);
			Assert.Equal(SSN_GUNNA, newEntity.SSN);
			Assert.Equal(TeacherType.MainTeacher, newEntity.Type);

			// Ensure that the Unit Of Work object has been instructed
			// to save the new entity object:
			Assert.True(_mockUnitOfWork.GetSaveCallCount() > 0);
		}

		[Fact]
//		[ExpectedException(typeof(AppObjectNotFoundException))]
		public void AddTeacher_InvalidCourse() {
			// Arrange:
			var model = new AddTeacherViewModel {
				SSN  = SSN_GUNNA,
				Type = TeacherType.AssistantTeacher
			};
			// Note: the method uses test data defined in [TestInitialize]

			// Act:
			Assert.Throws<AppObjectNotFoundException>( () => _service.AddTeacherToCourse(INVALID_COURSEID, model) );
		}

		/// <summary>
		/// Ensure it is not possible to add a person as a teacher
		/// when that person is not registered in the system.
		/// </summary>
		[Fact]
//		[ExpectedException(typeof (AppObjectNotFoundException))]
		public void AddTeacher_InvalidTeacher() {
			// Arrange:
			var model = new AddTeacherViewModel {
				SSN  = INVALID_SSN,
				Type = TeacherType.MainTeacher
			};
			// Note: the method uses test data defined in [TestInitialize]

			// Act:
			Assert.Throws<AppObjectNotFoundException>( () => _service.AddTeacherToCourse(COURSEID_VEFT_20153, model));
		}

		/// <summary>
		/// In this test, we test that it is not possible to
		/// add another main teacher to a course, if one is already
		/// defined.
		/// </summary>
		[Fact]
		//[ExpectedExceptionWithMessage(typeof (AppValidationException), "COURSE_ALREADY_HAS_A_MAIN_TEACHER")]
		public void AddTeacher_AlreadyWithMainTeacher() {
			// Arrange:
			var model = new AddTeacherViewModel {
				SSN  = SSN_GUNNA,
				Type = TeacherType.MainTeacher
			};
			// Note: the method uses test data defined in [TestInitialize]

			// Act:
			Exception ex = Assert.Throws<AppValidationException>( () => _service.AddTeacherToCourse(COURSEID_VEFT_20153, model));
			Assert.Equal(ex.Message, "COURSE_ALREADY_HAS_A_MAIN_TEACHER");
		}

		/// <summary>
		/// In this test, we ensure that a person cannot be added as a
		/// teacher in a course, if that person is already registered
		/// as a teacher in the given course.
		/// </summary>
		[Fact]
		// [ExpectedExceptionWithMessage(typeof (AppValidationException), "PERSON_ALREADY_REGISTERED_TEACHER_IN_COURSE")]
		public void AddTeacher_PersonAlreadyRegisteredAsTeacherInCourse() {
			// Arrange:
			var model = new AddTeacherViewModel {
				SSN  = SSN_DABS,
				Type = TeacherType.AssistantTeacher
			};
			// Note: the method uses test data defined in [TestInitialize]

			// Act:
			Exception ex = Assert.Throws<AppValidationException>( () => _service.AddTeacherToCourse(COURSEID_VEFT_20153, model));
			Assert.Equal(ex.Message, "PERSON_ALREADY_REGISTERED_TEACHER_IN_COURSE");
		}

		#endregion
	}
}
