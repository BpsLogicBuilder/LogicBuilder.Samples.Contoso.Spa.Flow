using Contoso.Spa.Flow.ScreenSettings.Views;
using LogicBuilder.Domain;

namespace Contoso.Spa.Flow.Requests
{
    public class DetailRequest : RequestBase
    {
        public BaseModel? Entity { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
