using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Requests;

namespace TatooMarket.Api.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices]ICreateUser useCase, [FromForm]RequestCreateUser request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}
