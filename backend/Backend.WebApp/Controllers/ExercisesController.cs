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
    [Authorize]
    public class ExercisesController : BaseController
    {
        private readonly ExerciseService exerciseService;
        //private readonly ILogger _logger;
        public ExercisesController(ControllerDependencies dependencies, ExerciseService service) : base(dependencies)
        {
            this.exerciseService = service;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await exerciseService.GetExercises();
            return Ok(exercises);
        }

        [HttpGet("view")]
        public IActionResult ViewExercise(Guid id)
        {
            var exercise = exerciseService.GetExercise(id);
            return Ok(exercise);
        }

        [HttpGet("getExerciseForInsert")]
        public async Task<IActionResult> GetExerciseForInsert(Guid id)
        {
            var exercise = await exerciseService.GetInsertExerciseModel(id);
            return Ok(exercise);
        }

        [HttpPost("insertExercise")]
        public IActionResult InsertExercise([FromForm] InsertExerciseModel model)
        {
            if(model.ExerciseId == Guid.Empty)
            {
                //exerciseService.AddExercise(model);
            }
            else
            {
                //exerciseService.EditExercise(model);
            }
            return Ok();
        }
    }
}
