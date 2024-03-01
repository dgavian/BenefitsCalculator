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

        protected Func<object?, ActionResult> GetObjectResultFunc(bool isFound)
        {
            return isFound ? Ok : NotFound;
        }
    }
}
