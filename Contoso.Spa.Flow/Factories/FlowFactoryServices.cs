using Contoso.Spa.Flow;
using Contoso.Spa.Flow.Factories;
using Contoso.Spa.Flow.Interfaces;
using LogicBuilder.RulesDirector;

#pragma warning disable IDE0130 //Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130
{
    internal static class FlowFactoryServices
    {
        internal static IServiceCollection AddFlowFactories(this IServiceCollection services)
        {
            return services
                .AddTransient<Func<IFlowManager, DirectorBase>>
                (
                    provider =>
                    flowManager => new Director(flowManager)
                )
                .AddTransient<Func<IFlowManager, IFlowActivity>>
                (
                    provider =>
                    flowManager => new FlowActivity(flowManager)
                )
                .AddTransient<IFlowFactory, FlowFactory>();
        }
    }
}
