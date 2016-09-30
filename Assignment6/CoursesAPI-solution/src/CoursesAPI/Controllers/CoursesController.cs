namespace CoursesAPI.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Route("/api/courses")]
    public class CoursesController : Controller {
#region GET
        [HttpGet]
        public IActionResult Courses() {
            return Ok("Good evening Mr Bond, I've been expecting you.");
        }

        [HttpGet]
        [Authorize(Policy="IsTeacher")] // teacher + student
        [Route("{id:int}")]
        public IActionResult Course(int id) {
            return Ok("Say ‘hello’ to my little friend!");
        }
#endregion

#region POST
        [HttpPost]
        [Authorize(Policy="IsTeacher")] 
        public IActionResult AddCourse() {
            return Ok("Heeeeere’s Johnny!");
        }
#endregion
    }
}
