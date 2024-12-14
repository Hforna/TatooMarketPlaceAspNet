using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Api.Binders;
using TatooMarket.Application.UseCases.Repositories.Finance;
using TatooMarket.Communication.Enums;
using TatooMarket.Communication.Requests.Finance;
using TatooMarket.Domain.Repositories.Payment;

namespace TatooMarket.Api.Controllers
{
    public class FinanceController : BaseController
    {
        [Authorize(Policy = "OnlySeller")]
        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount([FromBody]RequestCreateFinanceAccount request, [FromServices]ICreateFinanceAccount useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [AuthorizationUser]
        [HttpGet("buy-tattoosCart")]
        public async Task<IActionResult> BuyTattoo([FromServices] IBuyTattoo UseCase)
        {
            var result = await UseCase.Execute();

            return Ok(result);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> WebHook([FromServices]IStripeService stripeService)
        {
            await stripeService.WebHookService(HttpContext.Request.Body.ToString(), Request.Headers["Stripe-Signature"]);

            return NoContent();
        }
    }
}
