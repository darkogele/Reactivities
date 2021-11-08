using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
                return NotFound();

            if (result.StatusCodes == HttpStatusCode.Unauthorized)
                return Unauthorized();

            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);

            if (result.IsSuccess && result.Value == null)
                return NotFound();

            if (result.ValidationError != null)
            {
                ModelState.AddModelError(result.ValidationError.Keys.First(), result.ValidationError.Values.First());

                return ValidationProblem(ModelState);
            }

            return BadRequest(result.Error);
        }
    }
}