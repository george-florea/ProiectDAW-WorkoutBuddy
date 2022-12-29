using Backend.BusinessLogic.Exercises;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.ExtensionMethods;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class ExercisesController : BaseController
    {
        private readonly ExerciseService exerciseService;
        //private readonly ILogger _logger;
        public ExercisesController(ControllerDependencies dependencies, ExerciseService service) : base(dependencies)
        {
            this.exerciseService = service;
        }

        [HttpGet("get")]
        [Authorize]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await exerciseService.GetExercises();
            return Ok(exercises);
        }
    }
}
