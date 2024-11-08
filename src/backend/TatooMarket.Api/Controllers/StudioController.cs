using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Requests.Studio;

namespace TatooMarket.Api.Controllers
{
    [AuthorizationUser]
    public class StudioController : BaseController
    {
        [Authorize(Policy = "OnlySeller")]
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] ICreateStudio useCase, RequestCreateStudio request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
