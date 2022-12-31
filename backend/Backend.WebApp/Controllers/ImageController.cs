using Backend.BusinessLogic.Images;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        private readonly ImageService service;
        public ImageController(ControllerDependencies dependencies, ImageService service) : base(dependencies)
        {
            this.service = service;
        }

        [HttpGet("getImageById")]
        //[Authorize]
        public IActionResult GetImgContent(Guid id)
        {
            var model = service.GetImgContent(id);

            return File(model, "image/jpg");
        }
    }
}
