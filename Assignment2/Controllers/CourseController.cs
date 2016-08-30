namespace WebApplication.Controllers {
    using System;
    using WebApplication.Services.Implementation;
    using WebApplication.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication.Data;
    using WebApplication.Models.DTO.ViewModels;

    [Route("/api/courses")]
    public class CourseController : Controller {
#region MemberVariables
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;
#endregion

#region Constructor
        public CourseController(ApplicationDbContext context) {
            _courseService = new CourseService(context);
            _studentService = new StudentService(context);
        }
#endregion

#region GET
        /// <summary>
        /// GET: /api/courses?semester=20153
        /// Returns a list containing exactly one element. The semester is an optional parameter
        /// </summary>
        [HttpGet]
        public IActionResult Courses(int? semester) {
            try {
                var courses = _courseService.
                    GetCoursesOfSemester(semester ?? ConstantVariables.CurrentSemester);
                
                return Ok(courses);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// GET: /api/courses/1
        /// Returns a more detailed object describing T-514-VEFT taught in 20153.
        /// If not found, then return HTTP 404
        /// </summary>
        [HttpGet]
        [Route("{id}:int")]
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
        /// GET: /api/courses/1/students
        /// Returns a list of all students in T-514-VEFT in fall 2015
        /// </summary>
        [HttpGet]
        [Route("{id}:int/students")]
        public IActionResult Students(int id) {
            try {
                var course = _courseService.
                    GetCourseById(id);

                return Ok(course);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region PUT
        /// <summary>
        /// PUT: /api/courses/1
        /// Allows the client of the API to modify the given course instance. The properties which
        /// should be mutable are StartDate and EndDate.
        /// If course is not found, return HTTP 404
        /// </summary>
        [HttpPut]
        [Route("{id}:int")]
        public IActionResult Update(int id, CourseViewModel model) {
            try {
                var course = _courseService.
                    UpdateCourseById(id, model);

                return Ok(course);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region DELETE
        /// <summary>
        /// DELETE: /api/courses/1
        /// Removes the given course. If not found, return HTTP 404.
        /// </summary>
        [HttpDelete]
        [Route("{id}:int")]
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
        /// Adds a new course
        /// </summary>
        [HttpPost]
        public IActionResult Add(CourseViewModel model) {
            return Ok();
        }

        /// <summary>
        /// POST: /api/courses/2/students
        /// Adds a new student to T-514-VEFT in 20163. The request body contains the student info.
        /// </summary>
        [HttpPost]
        [Route("{id}:int/students")]
        public IActionResult Add(int id, StudentViewModel model) {
            return Ok();
        }
#endregion
    }
}
