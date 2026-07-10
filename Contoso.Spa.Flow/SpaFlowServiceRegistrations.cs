using AutoMapper;
using Contoso.Spa.Flow;
using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.Interfaces;
using LogicBuilder.App.Spa.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.Mapping;
using LogicBuilder.RulesDirector;
using Microsoft.Extensions.Logging.Abstractions;

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
                .AddTransient<ITransientFlowHelper, TransientFlowHelper>()
                .AddScoped<IFlowDataCache, FlowDataCache>()
                .AddScoped<Progress>();
        }

        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BaseClassMappings>();
                cfg.AddProfile<ConnectorProfile>();
                cfg.AddProfile<ParameterToDescriptorProfile>();
                cfg.AddProfile<ExpressionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();

            }, new NullLoggerFactory());

            configuration.AssertConfigurationIsValid();

            return services
                .AddSingleton<AutoMapper.IConfigurationProvider>(configuration)
                .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
        }
    }
}
