using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.Requests;
using Contoso.Spa.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow.Interfaces
{
    public interface IFlowManager
    {
        DirectorBase Director { get; }
        IFlowDataCache FlowDataCache { get; }
        Progress Progress { get; }
        IFlowActivity FlowActivity { get; }
        IRulesCache RulesCache { get; }
        IServiceProvider ServiceProvider { get; }

        FlowSettings Start(string module, int stage);
        FlowSettings Next(RequestBase request);
        FlowSettings NavStart(NavBarRequest navBarRequest);
        void RunFlow(string flowName);
        void FlowComplete();
        void Terminate();
        void SetCurrentBusinessBackupData();
    }
}
