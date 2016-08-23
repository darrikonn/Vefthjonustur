namespace WebApplication.Controllers {
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication.Models.EntityModels;
    using System.Linq;
    using System.Net.Http;
    using WebApplication.Services;
    using WebApplication.Models.ViewModels;

    [Route("/api/courses")]
    public class CourseController : Controller {
        private static List<Course> _courses;

#region Constructor
        public CourseController() {
            var studentService = new StudentService();

            if (_courses == null) {
                // using mock database
                _courses = new List<Course> {
                    new Course {
                        Id         = 0,
                        Name       = "Web services",
                        TemplateId = "T-514-VEFT",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3),
                        Students    = studentService.CreateStudents(3)
                    },
                    new Course {
                        Id         = 1,
                        Name       = "Litabok",
                        TemplateId = "T-550-Litur",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3),
                        Students    = studentService.CreateStudents(2)
                    },
                    new Course {
                        Id         = 2,
                        Name       = "Web Programming",
                        TemplateId = "T-411-WEPO",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(4),
                        Students    = studentService.CreateStudents(4)
                    },
                    new Course {
                        Id         = 3,
                        Name       = "Forritun 1",
                        TemplateId = "T-210-Forhud",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3),
                        Students    = studentService.CreateStudents(5)
                    },
                    new Course {
                        Id         = 4,
                        Name       = "Gagnaskipan",
                        TemplateId = "T-120-Gaggalagu",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(1),
                        Students    = studentService.CreateStudents(3)
                    }
                };
            }
        }
#endregion

#region GET
        // GET: /api/courses
        [HttpGet]
        public IActionResult GetCourses() {
            // don't need to error check
            return Ok(_courses);
        }

        // GET: /api/courses/{id}
        [HttpGet("{id}")]
        [Route("api/courses/{id:int}", Name="GetCourse")]
        public IActionResult GetCourse(int id) {
            // services
            var courseService = new CourseService(_courses);

            var course = courseService.GetCourseById(id);

            if (course == null) return NotFound();
            return Ok(course);
        }

        // GET: /api/courses/{id}/students
        [HttpGet("{id}/students")]
        [Route("api/courses/{id:int}/students", Name="GetStudentsOfCourse")]
        public IActionResult GetStudentsOfCourse(int id) {
            // services
            var courseService = new CourseService(_courses);

            var course = courseService.GetCourseById(id);

            // need to check if Students is null because that is not the same as an empty list
            // elsewhere when course is created, the Students should always be an empty list
            if (course == null || course.Students == null) return NotFound();
            return Ok(course.Students);
        }
#endregion

#region POST
        // POST: /api/courses/{id}/students
        [HttpPost("{id}/students")]
        public IActionResult AddStudentToCourse(StudentViewModel model, int id) {
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            // services
            var courseService = new CourseService(_courses);

            var course = courseService.GetCourseById(id);

            if (course == null) return NotFound();

            try {
                var student = new Student {
                    SSN = model.SSN,
                    Name = model.Name
                };
                course.Students.Add(student);

                return Created(Url.Link("GetStudentsOfCourse", new { id }), student);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }

        // POST: /api/courses
        [HttpPost]
        public IActionResult AddCourse(CourseViewModel model) {
            // also checking if StartDate and EndDate have values
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            try {
                // get next id
                var id = _courses.Count == 0 ? 
                    0 : 
                    _courses.Max(c => c.Id) + 1;

                var course = new Course {
                    Id = id,
                    Name = model.Name,
                    TemplateId = model.TemplateId,
                    StartDate = model.StartDate.Value,
                    EndDate = model.EndDate.Value,
                    Students = new List<Student>()
                };

                _courses.Add(course);

                return Created(Url.Link("GetCourse", new { id }), course);
            } catch(Exception e) {
                return StatusCode(500, e); 
            }
        }
#endregion

#region DELETE
        // DELETE: /api/courses/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id) {
            // services
            var courseService = new CourseService(_courses);

            var course = courseService.GetCourseById(id);

            if (course == null) return NotFound();

            try {
                _courses.Remove(course);

                return NoContent();
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion

#region PUT
        // PUT: /api/courses/{1}
        [HttpPut("{id}")]
        public IActionResult EditCourse(CourseViewModel model, int id) {
            // also checking if StartDate and EndDate have values
            if (!ModelState.IsValid) {
                return StatusCode(412);
            }

            // services
            var courseService = new CourseService(_courses);

            var course = courseService.GetCourseById(id);

            if (course == null) return NotFound();

            try {
                course.Name = model.Name;
                course.TemplateId = model.TemplateId;
                course.StartDate = model.StartDate.Value;
                course.EndDate = model.EndDate.Value;

                // using Ok because I want to return the course, else use 204 (NoContent)
                return Ok(course);
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
#endregion
    }
}
