namespace WebApplication.Controllers {
    using System;
    using WebApplication.Services.Implementation;
    using WebApplication.Helpers;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/courses")]
    public class CourseController : Controller {
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;

        public CourseController(CourseService courseService, StudentService studentService) {
            _courseService = courseService;
            _studentService = studentService;
        }

        /// <summary>
        /// GET: /api/courses
        /// Returns a list containing exactly one element
        /// </summary>
        [HttpGet]
        public IActionResult GetCourses() {
            try {
                _courseService.GetCoursesOfSemester(ConstantVariables.CurrentSemester);
                return Ok();
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        /// <summary>
        /// GET: /api/courses?semester=20153
        /// Returns a list containing two elements: T-514-VEFT and T-111-PROG, both taught in
        /// fall 2015
        /// </summary>

        /// <summary>
        /// GET: /api/courses/1
        /// Returns a more detailed object describing T-514-VEFT taught in 20153.
        /// If not found, then return HTTP 404
        /// </summary>

        /// <summary>
        /// PUT: /api/courses/1
        /// Allows the client of the API to modify the given course instance. The properties which
        /// should be mutable are StartDate and EndDate.
        /// If course is not found, return HTTP 404
        /// </summary>

        /// <summary>
        /// DELETE: /api/courses/1
        /// Removes the given course. If not found, return HTTP 404.
        /// </summary>

        /// <summary>
        /// GET: /api/courses/1/students
        /// Returns a list of all students in T-514-VEFT in fall 2015
        /// </summary>
        
        /// <summary>
        /// POST: /api/courses/2/students
        /// Adds a new student to T-514-VEFT in 20163. The request body contains the student info.
        /// </summary>
    }
}
