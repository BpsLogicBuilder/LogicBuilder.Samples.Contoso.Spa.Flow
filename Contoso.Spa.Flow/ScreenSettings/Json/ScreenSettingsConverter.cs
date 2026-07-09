using Contoso.Spa.Flow.ScreenSettings.Views;
using LogicBuilder.Expressions.Utils.Json;

namespace Contoso.Spa.Flow.ScreenSettings.Json
{
    public class ScreenSettingsConverter : JsonTypeConverter<ScreenSettingsBase>
    {
        public override string TypePropertyName => nameof(ScreenSettingsBase.TypeString);
    }
}
