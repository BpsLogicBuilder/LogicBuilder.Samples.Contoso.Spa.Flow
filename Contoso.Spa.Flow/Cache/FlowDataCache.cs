using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.ScreenSettings.Navigation;
using Contoso.Spa.Flow.ScreenSettings.Views;

namespace Contoso.Spa.Flow.Cache
{
    public class FlowDataCache : IFlowDataCache
    {
        public RequestedFlowStage RequestedFlowStage { get; set; } = new RequestedFlowStage();
        public NavigationBar NavigationBar { get; set; } = new NavigationBar();
        public ScreenSettingsBase? ScreenSettings { get; set; }
        public Dictionary<string, object> Items { get; set; } = [];
    }
}
