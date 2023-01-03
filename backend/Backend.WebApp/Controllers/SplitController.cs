using Backend.BusinessLogic.Account;
using Backend.BusinessLogic.Splits;
using Backend.Entities.Enums;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SplitController : BaseController
    {
        private readonly SplitService service;
        public SplitController(ControllerDependencies dependencies, SplitService splitService) : base(dependencies)
        {
            this.service = splitService;
        }

        [HttpGet("getSplits")]
        public async Task<IActionResult> Index()
        {
            var model = await service.GetSplits();
            return Ok(model);
        }

        [HttpGet("viewSplit")]
        public IActionResult ViewSplit([FromQuery]Guid id)
        {
            var model = service.GetSplit(id);

            return Ok(model);
        }
        /*
                [HttpGet]
                public IActionResult AddSplit()
                {
                    var muscleGroups = Enum.GetValues(typeof(MuscleGroups)).Cast<MuscleGroups>()
                        .Select(v => new SelectListItem()
                        {
                            Text = v.ToString(),
                            Value = ((int)v).ToString(),
                        }).ToList();
                    var model = new SplitModel()
                    {
                        MusclesGroups = muscleGroups
                    };
                    return View(model);
                }


                [HttpPost]
                public IActionResult AddSplit(SplitModel model)
                {
                    model.CreatorId = CurrentUser.Id;
                    service.AddSplit(model);
                    return RedirectToAction("Index");
                }



                public IActionResult EditSplit(Guid id)
                {
                    var model = service.PopulateSplitModel(id);
                    return View(model);
                }

                [HttpPost]
                public IActionResult EditSplit(SplitModel model)
                {
                    model.CreatorId = CurrentUser.Id;
                    service.EditSplit(model);

                    return RedirectToAction("Index");
                }

                [HttpPost]
                public IActionResult DeleteSplit([FromBody]Guid id)
                {
                    var isDeleted = service.DeleteSplit(id);
                    return Ok(isDeleted);
                }

                [HttpPost]
                public IActionResult AddToUserSplits([FromBody]string id)
                {
                    var splitId = Guid.Parse(id);
                    var isValid = service.AddToUserSplits(splitId, CurrentUser.Id);
                    if (isValid)
                    {
                        return Json(new { message = "Split successfully added to your colection!" });
                    }
                    else
                    {
                        return Json(new { message = "The split is already in your colection!" });
                    }
                }
        */

    }
}
