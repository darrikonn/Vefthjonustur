namespace CourseAPI.API.Controllers {
    using System;
    using CourseAPI.API.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Services.Services.Interface;
    //using CourseAPI.Services.Services.Implementation;

    /// <summary>
    /// CourseAPI controller that acts as a REST service. See controllers methods on how to use.
    /// </summary>
    [Route("/api/courses")]
    public class CourseController : Controller {
#region MemberVariables
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
#endregion

#region Constructor
        // using dependency injection
        public CourseController(ICourseService ics, IStudentService iss) {
            _courseService = ics;
            _studentService = iss;
        }
#endregion

#region GET
        /// <summary>
        /// GET: /api/courses{semester}
        /// Examples: 
        ///     1) <hostname>/api/courses?semester=20153
        ///     2) curl -i -X GET -d "semester=20153" <hostname>/api/courses
        ///     3) <hostname>/api/courses
        /// Returns a list containing exactly one element. The semester is an optional parameter.
        /// If no semester is specified, than the default semester will be used: 2016 fall.
        /// </summary>
        [HttpGet]
        public IActionResult Courses(int? semester = null) {
            try {
                var courses = _courseService.
                    GetCoursesOfSemester(semester ?? ConstantVariables.CurrentSemester);
                
                return Ok(courses);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// GET: /api/courses/{id}
        /// Examples: 
        ///     1) <hostname>/api/courses/1
        ///     2) curl -i -X GET <hostname>/api/courses/1
        /// Returns a more detailed object describing the course.
        /// If not found, then return HTTP 404.
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name="GetCourse")]
        public IActionResult Course(int id) {
            try {
                var course = _courseService.
                    GetCourseById(id);

                if (course == null) {
                    return NotFound();
                }

                course.NumberOfStudents = _studentService.
                    GetStudentsOfCourse(id).Count;

                return Ok(course);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// GET: /api/courses/{id}/students
        /// Examples:
        ///     1) <hostname>/api/courses/1/students
        ///     2) curl -i -X GET <hostname>/api/courses/1/students
        /// Returns a list of all students in a given course.
        /// If course is not found, then return HTTP 404.
        /// </summary>
        [HttpGet]
        [Route("{id:int}/students")]
        public IActionResult Students(int id) {
            try {
                var course = _courseService
                    .GetCourseById(id);
                if (course == null) {
                    return NotFound();
                }

                return Ok(_studentService
                    .GetStudentsOfCourse(id));
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region PUT
        /// <summary>
        /// PUT: /api/courses/{id}
        /// Examples:
        ///     1) curl -i -X PUT -d "CourseId=T-500-ERR"&Semester=20153" <hostname>/api/courses/1
        /// Allows the client of the API to modify the given course instance.
        /// Mutable objects are StartDate and EndDate.
        /// Immutable objects are CourseId and Semester.
        /// If course is not found, return HTTP 404.
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, CourseViewModel model) {
            // validate model
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            try {
                var course = _courseService.
                    UpdateCourseById(id, model);

                course.NumberOfStudents = _studentService.
                    GetStudentsOfCourse(id).Count;

                return Ok(course);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region DELETE
        /// <summary>
        /// DELETE: /api/courses/{id}
        /// Examples:
        ///     1) curl -i -X DELETE <hostname>/api/courses/1
        /// Removes the given course. 
        /// If not found, return HTTP 404.
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id) {
            try {
                if (!_courseService.DeleteCourseById(id)) {
                    throw new Exception();
                }
                
                return NoContent();
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region POST
        /// <summary>
        /// POST: /api/courses
        /// Examples:
        ///     1) curl -i -X -d "CourseId=T-500-ERR&Semester=20153" POST <hostname>/api/courses
        /// Adds a new course to the database.
        /// Mutable objects are StartDate and EndDate.
        /// Immutable objects are CourseId and Semester.
        /// </summary>
        [HttpPost]
        public IActionResult Add(CourseViewModel model) {
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            try {
                var id = _courseService.AddCourse(model);

                return Created(Url.Link("GetCourse", new { id }), 
                        _courseService.GetCourseById(id));
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// POST: /api/courses/{id}/students
        /// Examples:
        ///     1) curl -i -X -d "SSN=1501933119&Id=1" POST <hostname>/api/courses
        /// Adds a new student to a given course. The request body contains the student info.
        /// Immutable objects are SSN and Id (the Id of the course).
        /// </summary>
        [HttpPost]
        [Route("{id:int}/students")]
        public IActionResult Add(int id, LinkerViewModel model) {
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            try {
                var student = _studentService.AddStudentToCourse(id, model);

                return Created(Url.Link("GetStudentsOfCourse", new { id }), student); 
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion
    }
}
