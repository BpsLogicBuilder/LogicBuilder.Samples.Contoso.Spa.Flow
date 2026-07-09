using Contoso.Spa.Flow.ScreenSettings.Navigation;
using Contoso.Spa.Flow.ScreenSettings.Views;

namespace Contoso.Spa.Flow.Cache.Interfaces
{
    public interface IFlowDataCache
    {
        RequestedFlowStage RequestedFlowStage { get; set; }
        NavigationBar NavigationBar { get; set; }
        ScreenSettingsBase? ScreenSettings { get; set; }
        Dictionary<string, object> Items { get; set; }
    }
}
