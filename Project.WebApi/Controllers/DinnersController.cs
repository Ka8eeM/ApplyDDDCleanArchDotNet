using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers;

[Route("api/[controller]")]
public class DinnersController : ApiController
{
    public DinnersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }
}