using Microsoft.AspNetCore.Mvc;

namespace NebulaPlugin.Api.Controllers;

/// <summary>
/// Bu sınıf, ControllerBase'i kalıtır.
/// </summary>

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
    => result is not null ? Ok(result) : NotFound();

}

