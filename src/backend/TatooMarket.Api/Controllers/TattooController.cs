using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Api.Binders;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Communication.Requests.Tattoo;

namespace TatooMarket.Api.Controllers
{
    public class TattooController : BaseController
    {
        [Authorize(Policy = "OnlySeller")]
        [HttpPost]
        public async Task<IActionResult> CreateTattoo([FromForm] RequestCreateTattoo request, [FromServices] ICreateTattoo useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("{studioId}/{numberPage}")]
        public async Task<IActionResult> GetStudioTattoos([FromRoute][ModelBinder(typeof(BinderId))]long studioId, [FromRoute]int numberPage,
            [FromServices]IGetStudioTattoos useCase)
        {
            var result = await useCase.Execute(studioId, numberPage);

            if(result.Tattoos.Count == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("create-review")]
        [AuthorizationUser]
        public async Task<IActionResult> CreateReview([FromBody]RequestCreateTattooReview request, [FromServices]ICreateTattooReview useCase)
        {
            await useCase.Execute(request);

            return NoContent();
        }

        [HttpPost("weeks-tattoos")]
        public async Task<IActionResult> WeeksTattoos([FromBody]RequestSelectDate request, [FromServices]IGetWeeksTattoos useCase)
        {
            var result = await useCase.Execute(request);

            return Ok(result);
        }

        [HttpPost("create-tattooprice")]
        public async Task<IActionResult> CreateTattooPrice([FromForm]RequestCreateTattooPrice request, [FromServices]ICreateTattooPrice useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpPut("update-tattooprice/{Id}")]
        public async Task<IActionResult> UpdateTattooPrice([FromRoute][ModelBinder(typeof(BinderId))]long Id, [FromBody]RequestUpdateTattooPrice request,
            [FromServices]IUpdateTattooPrice useCase)
        {
            var result = await useCase.Execute(request, Id);

            return Ok(result);
        }



        [AuthorizationUser]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteReview([FromRoute][ModelBinder(typeof(BinderId))] long Id, [FromServices]IDeleteTattooReview useCase)
        {
            await useCase.Execute(Id);

            return NoContent();
        }
    }
}
