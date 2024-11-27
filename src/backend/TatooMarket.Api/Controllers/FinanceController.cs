using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TatooMarket.Api.Controllers
{
    [Authorize(Policy = "OnlySeller")]
    public class FinanceController : BaseController
    {
        //[HttpPost("create-account")]
        //public async Task<IActionResult> CreateAccount([FromBody]RequestCreateAccount request)
        //{
        //
        //}
    }
}
