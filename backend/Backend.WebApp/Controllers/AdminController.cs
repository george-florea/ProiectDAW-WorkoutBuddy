using Backend.BusinessLogic.Exercises;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : BaseController
    {
        private readonly ExerciseService _service;
        public AdminController(ControllerDependencies dependencies, ExerciseService service) : base(dependencies)
        {
            _service = service;
        }

        [HttpGet("getPendingExercises")]
        [Authorize("admin")]
        public IActionResult GetPendingExercises()
        {
            var exercises = _service.GetPendingExercises();
            return Ok(exercises);
        }
    }
}
