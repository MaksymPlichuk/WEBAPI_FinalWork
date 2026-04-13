using Microsoft.AspNetCore.Mvc;
using WEBAPI_FinalWork.BLL.Services;

namespace WEBAPI_FinalWork.API.Extensions
{
    public static class ControllerBaseExtension
    {
        public static IActionResult GetAction(this ControllerBase controller, ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                return controller.Ok(response);
            }
            else
            {
                return controller.BadRequest(response);
            }
        }
    }
}
