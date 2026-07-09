using Contoso.Domain;
using LogicBuilder.Domain;

namespace Contoso.Spa.Flow.Requests.TransientFlows
{
    public class SelectorFlowRequest
    {
        public BaseModel? Entity { get; set; }
        public string? ReloadItemsFlowName { get; set; }
    }
}
