using AutoMapper;
using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.ScreenSettings.Views;
using LogicBuilder.App.Spa.AutoMapperProfiles;
using LogicBuilder.App.Spa.Forms.Configuration.Common;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.Spa.Flow.Tests
{
    public class InitialFlowTest
    {
        static InitialFlowTest()
        {
            InitializeMapperConfiguration();
            Initialize();
        }
        #region Fields
        private static MapperConfiguration MapperConfiguration;
        private static IServiceProvider serviceProvider;
        private const string initialFlow = "initial";
        #endregion Fields

        [Fact]
        public void FlowWithUnrecognizedTarget_EndsWithViewTypeComplete()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, 6);

            //assert
            Assert.Equal(ViewType.FlowComplete, result.ScreenSettings.ViewType);
        }

        [Fact]
        public void FlowWithAboutTarget_StopsAtAboutScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.About);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<ListFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.List, result.ScreenSettings.ViewType);
            Assert.Equal("About", screenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithCoursesTarget_StopsAtCoursesScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Courses);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<GridSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Grid, result.ScreenSettings.ViewType);
            Assert.Equal("Courses", screenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithHomeTarget_StopsAtHomeScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Home);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<HtmlPageSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Html, result.ScreenSettings.ViewType);
            Assert.Equal("Contoso University", screenSettings.Settings.ContentTemplate!.Title);
        }

        [Fact]
        public void FlowWithDepartmentsTarget_StopsAtDepartmentsScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Departments);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<GridSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Grid, result.ScreenSettings.ViewType);
            Assert.Equal("Departments", screenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithInstructorsTarget_StopsAtInstructorsScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Instructors);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<GridSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Grid, result.ScreenSettings.ViewType);
            Assert.Equal("Instructors", screenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithStudentsTarget_StopsAtStudentsScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Students);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<GridSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Grid, result.ScreenSettings.ViewType);
            Assert.Equal("Students", screenSettings.Settings.Title);
        }

        #region Helpers
        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration ??= new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BaseClassMappings>();
                cfg.AddProfile<ConnectorProfile>();
                cfg.AddProfile<ParameterToDescriptorProfile>();
                cfg.AddProfile<ExpressionParameterToDescriptorMappingProfile>();
                cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
            }, new NullLoggerFactory());
            MapperConfiguration.AssertConfigurationIsValid();
        }

        [MemberNotNull(nameof(serviceProvider))]
        private static void Initialize()
        {
            serviceProvider ??= new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddLogging()
                .AddSpaFlowServices()
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}
