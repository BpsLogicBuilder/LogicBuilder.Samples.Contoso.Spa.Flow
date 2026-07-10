using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.Requests.TransientFlows;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Spa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransientFlowController(ITransientFlowHelper transientFlowHelper) : ControllerBase
    {
        private readonly ITransientFlowHelper _transientFlowHelper = transientFlowHelper;

        [HttpPost("RunSelectorFlow")]
        public IActionResult RunSelectorFlow([FromBody] SelectorFlowRequest selectorFlowRequest)
        {
            return Ok(_transientFlowHelper.RunSelectorFlow(selectorFlowRequest));
        }
    }
}
