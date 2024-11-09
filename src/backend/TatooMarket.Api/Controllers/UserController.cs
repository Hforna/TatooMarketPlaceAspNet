using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Communication.Requests.User;

namespace TatooMarket.Api.Controllers
{
    [AuthorizationUser]
    public class UserController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromServices]ICreateUser useCase, [FromForm]RequestCreateUser request)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile([FromServices]IGetUserProfile useCase)
        {
            var result = await useCase.Execute();

            return Ok(result);
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromServices]IUpdateUser useCase, [FromBody]RequestUpdateUser request)
        {
            await useCase.Execute(request);

            return NoContent();
        }
    }
}
