using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TatooMarket.Api.Attributes;
using TatooMarket.Api.Binders;
using TatooMarket.Application.UseCases.Repositories.Address;
using TatooMarket.Application.UseCases.Repositories.Studio;
using TatooMarket.Communication.Requests.Address;
using TatooMarket.Communication.Requests.Studio;

namespace TatooMarket.Api.Controllers
{
    public class StudioController : BaseController
    {
        [Authorize(Policy = "OnlySeller")]
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] ICreateStudio useCase, RequestCreateStudio request)
        {
           var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("studio-price-catalog/{Id}")]
        public async Task<IActionResult> GetStudioPriceCatalog([FromRoute][ModelBinder(typeof(BinderId))]long Id, [FromServices]IStudioPriceCatalog useCase)
        {
            var result = await useCase.Execute(Id);

            return Ok(result);
        }

        [HttpPost("studio-address")]
        public async Task<IActionResult> CreateAddress([FromBody]RequestCreateAddress request, [FromServices]ICreateAddress useCase)
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }

        [HttpGet("{numberPage}")]
        public async Task<IActionResult> GetStudios([FromServices]IGetStudios useCase, int numberPage)
        {
            var result = await useCase.Execute(numberPage);

            if(!result.Studios.Any())
                return NoContent();

            return Ok(result);
        }
    }
}
