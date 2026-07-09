using Contoso.Spa.Flow.Cache;
using LogicBuilder.Attributes;

namespace Contoso.Spa.Flow.ScreenSettings.Navigation
{
    public class NavigationBar(
        [Comments("Brand text for the navigation bar.")]
            string brandText = "Contoso",

        [Comments("Current module indicator used to determine which menu item gets set to active.")]
            int currentModule = TargetModules.Home,

        [Comments("True if the grid is sortable otherwise false")]
            List<NavigationMenuItem>? MenuItems = null
        )
    {
        public string BrandText { get; set; } = brandText;
        public int CurrentModule { get; set; } = currentModule;
        public List<NavigationMenuItem> MenuItems { get; set; } = MenuItems ?? [];
    }
}
