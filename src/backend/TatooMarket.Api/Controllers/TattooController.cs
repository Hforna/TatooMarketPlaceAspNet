using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;

namespace TatooMarket.Api.Controllers
{
    public class TattooController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTattoo([FromBody]RequestCreateTattoo request, [FromServices]ICreateTattoo useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
