using LogicBuilder.Expressions.Utils.ExpressionDescriptors;

namespace Contoso.Spa.Flow.Responses.TransientFlows
{
    public class SelectorFlowResponse : BaseFlowResponse
    {
        public SelectorLambdaDescriptor? Selector { get; set; }
    }
}
