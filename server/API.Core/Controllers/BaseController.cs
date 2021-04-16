using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.Core.Models;

namespace API.Core.Controllers
{
    public class BaseApiController : Controller
    {
        [NonAction]
        public IActionResult Json<T>(OrchestratorResult<T> result)
        {
            if (result.Result == ResultEnum.Unauthorized)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json("");
            }

            if (result.Result == ResultEnum.Error)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json("");
            }

            if (result.Result == ResultEnum.Success)
            {
                Response.StatusCode = (int) HttpStatusCode.OK;
            }

            if (result.Model == null)
            {
                return Json("");
            }

            return Json(result.Model);
        }
    }
}
