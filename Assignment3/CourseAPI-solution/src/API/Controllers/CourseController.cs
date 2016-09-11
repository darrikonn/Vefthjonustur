namespace CourseAPI.API.Controllers {
    using System;
    using System.Linq;
    using CourseAPI.API.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Services.Itf;
    using CourseAPI.Services.Exceptions;

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
        /// The semester is an optional parameter.
        /// If no semester is specified, than the default semester will be used: 2016 fall.
        /// </summary>
        /// <returns>
        /// Returns a list of CourseDTOs
        /// </returns>
        [HttpGet]
        public IActionResult Courses(string semester = null) {
            try {
                var courses = _courseService.
                    GetCoursesOfSemester(semester ?? ConstantVariables.CurrentSemester);
                foreach (var course in courses) {
                    course.NumberOfStudents = _studentService.
                        GetStudentsOfCourse(course.Id).Count;
                }
                
                return Ok(courses);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// GET: /api/courses/{id}
        /// Examples: 
        ///     1) <hostname>/api/courses/1
        ///     2) curl -i -X GET <hostname>/api/courses/1
        /// </summary>
        /// <returns>
        /// Returns a more detailed course object, called CourseDetailsDTO.
        /// </returns>
        [HttpGet]
        [Route("{id:int}", Name="GetCourse")]
        public IActionResult Course(int id) {
            try {
                var course = _courseService.
                    GetCourseById(id);

                course.Students = _studentService.
                    GetStudentsOfCourse(id);

                return Ok(course);
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// GET: /api/courses/{id}/students
        /// Examples:
        ///     1) <hostname>/api/courses/1/students
        ///     2) curl -i -X GET <hostname>/api/courses/1/students
        /// </summary>
        /// <returns>
        /// Returns a list of all students (StudentDTO) in a given course.
        /// </returns>
        [HttpGet]
        [Route("{id:int}/students", Name="GetStudentsOfCourse")]
        public IActionResult Students(int id) {
            try {
                var course = _courseService
                    .GetCourseById(id);

                return Ok(_studentService
                    .GetStudentsOfCourse(id));
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }
        
        /// <summary>
        /// GET: /api/courses/{id}/waitinglist
        /// Examples:
        ///     1) <hostname>/api/courses/1/waitinglist
        ///     2) curl -i -X GET <hostname>/api/courses/1/waitinglist
        /// </summary>
        /// <returns>
        /// Returns a list of all students (StudentDTO) in the waiting list of a course.
        /// </returns>
        [HttpGet]
        [Route("{id:int}/waitinglist", Name="GetCourseWaitingList")]
        public IActionResult WaitingList(int id) {
            try {
                var course = _courseService
                    .GetCourseById(id);

                return Ok(_studentService
                    .GetStudentsOfCourseWaitingList(id));
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }
#endregion

#region PUT
        /// <summary>
        /// PUT: /api/courses/{id}
        /// Examples:
        ///     1) curl -i -X PUT -d "TemplateId=T-500-ERR"&Semester=20153&MaxStudents=4" <hostname>/api/courses/1
        /// Allows the client of the API to modify the given course instance.
        /// Mutable objects are StartDate and EndDate.
        /// Immutable objects are TemplateId, Semester and MaxStudents.
        /// </summary>
        /// <returns>
        /// Returns the updated course (CourseDetailsDTO).
        /// </returns>
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody]CourseViewModel model) {
            // validate model
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                return StatusCode(412, errors);
            }

            try {
                var course = _courseService.
                    UpdateCourseById(id, model);

                course.Students = _studentService.
                    GetStudentsOfCourse(id);

                return Ok(course);
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }
#endregion

#region DELETE
        /// <summary>
        /// DELETE: /api/courses/{id}
        /// Examples:
        ///     1) curl -i -X DELETE <hostname>/api/courses/1
        /// Removes the given course. 
        /// </summary>
        /// <returns>
        /// Returns no content if deletion was successful.
        /// </returns>
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id) {
            try {
                if (!_courseService.DeleteCourseById(id)) {
                    throw new Exception();
                }
                
                return NoContent();
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// DELETE: /api/courses/{id}/students/{ssn}
        /// Examples:
        ///     1) curl -i -X DELETE <hostname>/api/courses/1/students/1234567890
        /// "Removes" a student from a given course.
        /// It's actually not removing, but instead it updates the active attribute of a link
        /// </summary>
        /// <returns>
        /// Returns no content if deletion was successful.
        /// </returns>
        [HttpDelete]
        [Route("{id:int}/students/{ssn}")]
        public IActionResult Delete(int id, string ssn) {
            try {
                if (!_studentService.DeleteStudentFromCourse(id, ssn)) {
                    throw new Exception();
                }
                
                return NoContent();
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }
#endregion

#region POST
        /// <summary>
        /// POST: /api/courses
        /// Examples:
        ///     1) curl -i -X -d "TemplateId=T-500-ERR&Semester=20153&MaxStudents=10" POST <hostname>/api/courses
        /// Adds a new course to the database.
        /// Mutable objects are StartDate and EndDate.
        /// Immutable objects are TemplateId, Semester and MaxStudents.
        /// </summary>
        /// <returns>
        /// Returns the newly added course (CourseDetailsDTO) with status code 201.
        /// </returns>
        [HttpPost]
        public IActionResult Add([FromBody]CourseViewModel model) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                return StatusCode(412, errors);
            }

            try {
                var id = _courseService.AddCourse(model);

                return Created(Url.Link("GetCourse", new { id }), 
                        _courseService.GetCourseById(id));
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// POST: /api/courses/{id}/students
        /// Examples:
        ///     1) curl -i -X -d "SSN=1501933119" POST <hostname>/api/courses/1/students
        /// Adds a new student to a given course. The request body contains the student info.
        /// SSN is an immutable object.
        /// </summary>
        /// <returns>
        /// Returns the student name and a message; with status code 201 if successful.
        /// </returns>
        [HttpPost]
        [Route("{id:int}/students")]
        public IActionResult StudentAdd(int id, [FromBody]LinkerViewModel model) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                return StatusCode(412, errors);
            }

            try {
                var course = _courseService.GetCourseById(id);

                var student = _studentService.AddStudentToCourse(id, model, course.MaxStudents);

                return Created(Url.Link("GetStudentsOfCourse", new { id }), 
                        $"{student.Name} is now enrolled in the course\n"); 
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(CustomConflictException e) {
                return StatusCode(412, e.Message);
                //return StatusCode(409, e.Message);
            } catch(CustomForbiddenException e) {
                return StatusCode(412, e.Message);
                //return StatusCode(403, e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// POST: /api/courses/{id}/waitinglist
        /// Examples:
        ///     1) curl -i -X -d "SSN=1501933119" POST <hostname>/api/courses/1/waitinglist
        /// Adds a new student to the waiting list of a  given course. 
        /// The request body contains the student info.
        /// SSN is an immutable object
        /// </summary>
        /// <returns>
        /// Returns the student name + message; with status code 200 if successful.
        /// </returns>
        [HttpPost]
        [Route("{id:int}/waitinglist")]
        public IActionResult WaitingListAdd(int id, [FromBody]LinkerViewModel model) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                return StatusCode(412, errors);
            }

            try {
                var course = _courseService.GetCourseById(id);

                var student = _studentService.AddStudentToCourseWaitingList(id, model);
                
                return Ok($"{student.Name} is now registered on the waiting list\n");
                // return Created(Url.Link("GetCourseWaitingList", new { id }), student); 
            } catch(CustomObjectNotFoundException e) {
                return NotFound(e.Message);
            } catch(CustomConflictException e) {
                //return StatusCode(409, e.Message);
                return StatusCode(412, e.Message);
            } catch(CustomForbiddenException e) {
                return StatusCode(412, e.Message);
                //return StatusCode(403, e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
        }
#endregion
    }
}
