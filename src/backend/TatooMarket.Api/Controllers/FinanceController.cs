using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Application.UseCases.Repositories.Finance;
using TatooMarket.Communication.Requests.Finance;

namespace TatooMarket.Api.Controllers
{
    [Authorize(Policy = "OnlySeller")]
    public class FinanceController : BaseController
    {
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody]RequestCreateFinanceAccount request, [FromServices]ICreateFinanceAccount useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
