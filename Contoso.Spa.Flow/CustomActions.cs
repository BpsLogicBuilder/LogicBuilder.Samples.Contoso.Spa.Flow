using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.ScreenSettings.Navigation;

namespace Contoso.Spa.Flow
{
    public class CustomActions(IFlowDataCache flowDataCache) : ICustomActions
    {
        private readonly IFlowDataCache flowDataCache = flowDataCache;

        public void UpdateNavigationBar(NavigationBar navBar)
        {
            this.flowDataCache.NavigationBar = navBar;
        }
    }
}
