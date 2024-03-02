using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ApiResponse<T> MakeResponse<T>(T data, bool success)
        {
            return new ApiResponse<T>
            {
                Data = data,
                Success = success
            };
        }

        protected ActionResult<ApiResponse<T>> GetActionResultForNullableItem<T>(T item) where T : class?
        {
            var isFound = item != null;
            Func<object?, ActionResult> func = isFound ? Ok : NotFound;
            var response = MakeResponse(item, isFound);
            return func(response);
        }
    }
}
