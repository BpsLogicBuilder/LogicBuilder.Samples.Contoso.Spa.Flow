using AutoMapper;
using Contoso.Spa.Flow;
using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Cache.Interfaces;
using Contoso.Spa.Flow.Interfaces;
using LogicBuilder.App.Spa.AutoMapperProfiles;
using LogicBuilder.App.Utils.Rules;
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
                .AddRulesCacheService
                (
                    new RulesLoaderRequest
                    (
                        "Contoso.Spa.Flow.Rulesets",
                        typeof(FlowActivity),
                        [
                            typeof(LogicBuilder.App.Utils.Interfaces.ITypeHelper).Assembly,
                            typeof(LogicBuilder.App.Spa.Forms.Parameters.CommandButtonParameters).Assembly,
                            typeof(LogicBuilder.App.Spa.Forms.Configuration.CommandButtonDescriptor).Assembly,
                            typeof(LogicBuilder.Forms.Parameters.Expansions.SelectExpandDefinitionParameters).Assembly,
                            typeof(Contoso.Domain.Entities.StudentModel).Assembly,
                            typeof(Contoso.Data.Entities.Course).Assembly,
                            typeof(DirectorBase).Assembly,
                            typeof(string).Assembly
                        ]
                    )
                )
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
