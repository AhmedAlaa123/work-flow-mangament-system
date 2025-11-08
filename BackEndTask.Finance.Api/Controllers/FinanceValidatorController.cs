using BackEndTask.Finance.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTask.Finance.Api.Controllers
{
    [Route("v1/[controller]")]
    public class FinanceValidatorController : ControllerBase
    {
        [HttpPost("Validate-finance-request")]
        [ProducesResponseType(typeof(ExtranlValidationReponseDto), 200)]
        public async Task<IActionResult> ValidateFinanceRequests([FromBody] FlowDto flowDto)

        {
            return Ok(new ExtranlValidationReponseDto { IsAccepted=true });
        }
    }
}
