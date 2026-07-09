using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.ScreenSettings.Navigation;
using LogicBuilder.Attributes;

namespace Contoso.Spa.Flow
{
    public static class CustomActionUtilities
    {
        [AlsoKnownAs("SetupNavigationMenu")]
        public static void UpdateNavigationBar(ICustomActions customActions, NavigationBar navBar)
        {
            customActions.UpdateNavigationBar(navBar);
        }
    }
}
