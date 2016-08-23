namespace WebApplication.Controllers {
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using WebApplication.Models.EntityModels;
    using System.Net.Http;
    using WebApplication.Services;

    [Route("/api/courses")]
    public class CourseController : Controller {
        private static List<Course> _courses;

        #region Constructor
        public CourseController() {
            var studentService = new StudentService();

            if (_courses == null) {
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

        // GET: /api/courses
        [HttpGet]
        public IActionResult GetCourses() {
            return Ok(_courses);
        }

        // GET: /api/courses/{id}
        [HttpGet("{id}")]
        [Route("api/courses/{id:int}", Name="GetCourse")]
        public IActionResult GetCourse(int id) {
            var course = (from c in _courses
                          where c.Id == id
                          select c).SingleOrDefault();

            if (course == null) return NotFound();
            return Ok(course);
        }

        // GET: /api/courses/{id}/students
        [HttpGet("{id}/students")]
        public IActionResult GetStudentsOfCourse(int id) {
            var course = (from c in _courses
                          where c.Id == id
                          select c).SingleOrDefault();

            if (course == null || course.Students == null) return NotFound();
            return Ok(course.Students);
            
        }
/*
        // POST: /api/courses/
        [HttpPost]
        public IActionResult AddCourse(string name, string templateId, 
                DateTime startDate, DateTime endDate) {
            try {
                var id = _courses.Max(c => c.Id) + 1;

                var course = new Course {
                    Id = id,
                    Name = name,
                    TemplateId = templateId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Students = new List<Student>()
                };

                _courses.Add(course);

                return Created(Url.Link("GetCourse", new { id }), course);
            } catch(Exception) {
                return InternalServerError(); 
            }
        }

        [HttpPut]
        public IActionResult EditCourse(int id, string name, string templateId, 
                DateTime startDate, DateTime endDate) {
            var course = (from c in _courses
                          where c.Id == id
                          select c).SingleOrDefault();
            if (course == null) {
                return BadRequest();
            }

            try {
                course.Name = name;
                course.TemplateId = templateId;
                course.StartDate = startDate;
                course.EndDate = endDate;

                return Ok(course);
            } catch(Exception) {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public IActionResult DeleteCourse() {
            return Ok();
        }*/
    }
}
