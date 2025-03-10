using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]  // This hides the controller from Swagger

public class ErrorController : ControllerBase
{
    [Route("/error")]

    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;


        return Problem(title: exception?.Message, statusCode: 400);
    }
}