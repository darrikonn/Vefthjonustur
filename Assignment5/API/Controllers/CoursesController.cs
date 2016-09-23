using System;
using Microsoft.AspNetCore.Mvc;
using CoursesAPI.Models;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.Exceptions;
using CoursesAPI.Services.Utilities;
using System.Net.Http;

namespace CoursesAPI.Controllers {
	[Route("api/courses")]
	public class CoursesController : Controller {
		private readonly CoursesServiceProvider _service;

		public CoursesController(IUnitOfWork uow) {
			_service = new CoursesServiceProvider(uow);
		}

		[HttpGet]
		public IActionResult GetCoursesBySemester(int page = 0, string semester = null) {
            try {
                return Ok(_service.GetCourseInstancesBySemester(
                            LanguageUtils.GetLanguage(Request.Headers["Accept-Language"]),
                            page, semester));
            } catch(AppObjectNotFoundException e) {
                return StatusCode(415, e.Message);
            } catch(Exception e) {
                return StatusCode(500, e.Message);
            }
		}

		/// <summary>
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{id}/teachers")]
		public IActionResult AddTeacher(int id, AddTeacherViewModel model) {
			var result = _service.AddTeacherToCourse(id, model);
			return Created("TODO", result);
		}
	}
}
