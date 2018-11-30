namespace WebApplication.API.CoursesController {
  using System;
  using Microsoft.AspNetCore.Mvc;
  using System.Net.Http;
  using WebApplication.Services.Interfaces;
  using WebApplication.Models.ViewModels;

  [Route("/api/courses")]
  public class CoursesController : Controller {
    private readonly ICourseService _service;
    public CoursesController(ICourseService cs) {
      _service = cs;
    }

    [HttpGet]
    public IActionResult Courses() {
      return Ok(_service.getCourses());
    }

    [HttpGet]
    [Route("/{id}", Name="GetCourse")]
    public IActionResult Course(int id) {
      return Ok(_service.getCourse(id));
    }

    [HttpGet]
    [Route("/{id}")]
    public IActionResult Teacher(int id) {
      return Ok(_service.getTeacher(id));
    }

    [HttpPost]
    public IActionResult AddCourse(CourseViewModel model) {
      var id = _service.addCourse(model);

      return Created(Url.Link("GetCourse", new {id}), _service.getCourse(id));
    }
  }
}
