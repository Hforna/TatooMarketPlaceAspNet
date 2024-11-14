using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;

namespace TatooMarket.Api.Controllers
{
    public class TattooController : BaseController
    {
        [Authorize(Policy = "OnlySeller")]
        [HttpPost]
        public async Task<IActionResult> CreateTattoo([FromForm]RequestCreateTattoo request, [FromServices]ICreateTattoo useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpPost("create-review")]
        [AuthorizationUser]
        public async Task<IActionResult> CreateReview([FromBody]RequestCreateTattooReview request, [FromServices]ICreateTattooReview useCase)
        {
            await useCase.Execute(request);

            return NoContent();
        }
    }
}
