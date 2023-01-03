using Backend.BusinessLogic.Splits;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Mvc;
namespace Backend.WebApp.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly CommentService service;
        public CommentController(ControllerDependencies dependencies, CommentService commentService) : base(dependencies)
        {
            this.service = commentService;
        }

        [HttpPost("add")]
        public IActionResult AddComment([FromBody]CommentModel model)
        {
            model.Author = CurrentUser.Username;
            service.AddComment(model, CurrentUser);
            return Ok();
        }
        
        [HttpPost("delete")]
        public IActionResult DeleteComment([FromBody]string id)
        {
            var isDeleted = service.DeleteComment(Guid.Parse(id), CurrentUser);

            return Ok(isDeleted);
        }
    }
}
