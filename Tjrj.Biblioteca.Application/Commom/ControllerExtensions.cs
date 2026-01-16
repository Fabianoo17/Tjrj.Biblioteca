using Microsoft.AspNetCore.Mvc;

namespace Tjrj.Biblioteca.Application.Commom
{
    public static class ControllerExtensions
    {
        public static ActionResult ToActionResult<T>(
            this ControllerBase controller,
            ServiceResult<T> result)
        {
            if (result.Success)
                return controller.Ok(result.Data);

            return result.ErrorCode switch
            {
                Errors.NotFound => controller.NotFound(new { error = result.ErrorMessage }),
                Errors.Conflict => controller.Conflict(new { error = result.ErrorMessage }),
                Errors.Validation => controller.BadRequest(new { error = result.ErrorMessage }),
                _ => controller.BadRequest(new { error = result.ErrorMessage })
            };
        }
    }
}
