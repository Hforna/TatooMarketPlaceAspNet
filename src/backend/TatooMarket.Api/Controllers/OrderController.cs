using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Application.UseCases.Repositories.Order;
using TatooMarket.Communication.Requests.Order;

namespace TatooMarket.Api.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost("add_tattoo")]
        public async Task<IActionResult> AddTattooToOrder([FromServices]IAddTattooToOrder useCase, [FromBody]RequestAddTattooToOrder request)
        {
            var session = HttpContext.Session;

            await useCase.Execute(request, session);

            return NoContent();
        }
    }
}
