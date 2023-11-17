using Microsoft.AspNetCore.Mvc;

namespace NebulaPlugin.Api.Controllers;

/// <summary>
/// Bu sınıf, ControllerBase'i kalıtır.
/// </summary>

[ApiController]
[Route("/api/[controller]")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Bu metod parametre değerine göre ActionResult tipi döndürür.
    /// </summary>

    /// <param name="T">Toplanacak olan ilk sayı.</param>
    /// <returns>Ok or NotFound</returns>
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        => result is not null ? Ok(result) : NotFound();
}

