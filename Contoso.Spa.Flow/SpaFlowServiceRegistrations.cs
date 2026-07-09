using Contoso.Spa.Flow;
using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.Interfaces;
using LogicBuilder.RulesDirector;

#pragma warning disable IDE0130 //Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130
{
    public static class SpaFlowServiceRegistrations
    {
        public static IServiceCollection AddSpaFlowServices(this IServiceCollection services)
        {
            return services
                .AddAppUtilsServices()
                .AddFlowFactories()
                .AddRulesCacheService()
                .AddTransient<ICustomActions, CustomActions>()
                .AddTransient<ICustomDialogs, CustomDialogs>()
                .AddTransient<IFlowManager, FlowManager>()
                .AddScoped<IFlowDataCache, FlowDataCache>()
                .AddScoped<Progress>();
        }
    }
}
