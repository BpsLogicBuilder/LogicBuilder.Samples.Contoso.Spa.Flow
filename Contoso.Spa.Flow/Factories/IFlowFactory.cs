using Contoso.Spa.Flow.Interfaces;
using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow.Factories
{
    public interface IFlowFactory
    {
        DirectorBase GetDirector(IFlowManager flowManager);
        IFlowActivity GetFlowActivity(IFlowManager flowManager);
    }
}
