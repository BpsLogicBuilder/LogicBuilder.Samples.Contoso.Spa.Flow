using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.Spa.Flow.Cache;
using LogicBuilder.App.Spa.AutoMapperProfiles;
using LogicBuilder.App.Spa.Business.Requests;
using LogicBuilder.App.Spa.Business.ScreenSettings.Views;
using LogicBuilder.App.Spa.Forms.Configuration.Common;
using LogicBuilder.App.Spa.Utils;
using LogicBuilder.App.Spa.Utils.Interfaces;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
            var result = flowManager.Start(initialFlow, 99);

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
        public void FlowWithChatTarget_StopsAtChatScreen()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var result = flowManager.Start(initialFlow, TargetModules.Chat);

            //assert
            var screenSettings = Assert.IsType<ScreenSettings<ChatFormSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Chat, result.ScreenSettings.ViewType);
            Assert.Equal(600, screenSettings.Settings.ChatWidth);
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
        public void PersistentFlowItemsFronNavBarRequest_AreTransferredToTheFlowDataCache_AndFlowSettings()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();
            var persistentFlowItems = new Dictionary<string, object> { ["UserId"] = 1, ["UserName"] = "Smith101", ["UserRating"] = 9.5 };

            //act
            var result = flowManager.NavStart
            (
                new NavBarRequest
                {
                    PersistentFlowItems = persistentFlowItems,
                    InitialModuleName = initialFlow,
                    TargetModule = TargetModules.Students
                }
            );

            //assert
            Assert.Equal(1, (int)flowManager.FlowDataCache.Items["UserId"]);
            Assert.Equal("Smith101", (string)flowManager.FlowDataCache.Items["UserName"]);
            Assert.Equal(9.5, (double)flowManager.FlowDataCache.Items["UserRating"]);

            Assert.Equal(1, (int)result.PersistentFlowItems["UserId"]);
            Assert.Equal("Smith101", result.PersistentFlowItems["UserName"]);
            Assert.Equal(9.5, result.PersistentFlowItems["UserRating"]);
            var screenSettings = Assert.IsType<ScreenSettings<GridSettingsDescriptor>>(result.ScreenSettings);
            Assert.Equal(ViewType.Grid, result.ScreenSettings.ViewType);
            Assert.Equal("Students", screenSettings.Settings.Title);
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
        public void FlowWithDepartmentsTarget_CanVisitCreateScreen_AndReturnToGrid()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var firstResult = flowManager.Start(initialFlow, TargetModules.Departments);
            var firstScreenSettings = (ScreenSettings<GridSettingsDescriptor>)firstResult.ScreenSettings;
            var selectedButton = firstScreenSettings.CommandButtons!.First(b => b.ShortString == "departments_CREATE");
            var secondResult = flowManager.Next
            (
                new GridRequest
                {
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = selectedButton.ShortString },
                    FlowState = ((Director)flowManager.Director).FlowState,
                    ViewType = ViewType.Grid
                }
            );

            //assert
            var secondScreenSettings = Assert.IsType<ScreenSettings<EditFormSettingsDescriptor>>(secondResult.ScreenSettings);
            Assert.Equal(ViewType.Create, secondScreenSettings.ViewType);
            Assert.Equal("Department", secondScreenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithDepartmentsTarget_CanVisitEditScreen_AndReturnToGrid()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var firstResult = flowManager.Start(initialFlow, TargetModules.Departments);
            var firstScreenSettings = (ScreenSettings<GridSettingsDescriptor>)firstResult.ScreenSettings;
            var selectedButton = firstScreenSettings.CommandButtons!.First(b => b.ShortString == "departments_EDIT");
            var secondResult = flowManager.Next
            (
                new GridRequest
                {
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = selectedButton.ShortString },
                    Entity = new DepartmentModel { DepartmentID = 1 },
                    FlowState = ((Director)flowManager.Director).FlowState,
                    ViewType = ViewType.Grid
                }
            );

            //assert
            var secondScreenSettings = Assert.IsType<ScreenSettings<EditFormSettingsDescriptor>>(secondResult.ScreenSettings);
            Assert.Equal(ViewType.Edit, secondScreenSettings.ViewType);
            Assert.Equal("Department", secondScreenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithDepartmentsTarget_CanVisitDetailScreen_AndReturnToGrid()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var firstResult = flowManager.Start(initialFlow, TargetModules.Departments);
            var firstScreenSettings = (ScreenSettings<GridSettingsDescriptor>)firstResult.ScreenSettings;
            var selectedButton = firstScreenSettings.CommandButtons!.First(b => b.ShortString == "departments_DETAIL");
            var secondResult = flowManager.Next
            (
                new GridRequest
                {
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = selectedButton.ShortString },
                    Entity = new DepartmentModel { DepartmentID = 1 },
                    FlowState = ((Director)flowManager.Director).FlowState,
                    ViewType = ViewType.Grid
                }
            );

            //assert
            var secondScreenSettings = Assert.IsType<ScreenSettings<DetailFormSettingsDescriptor>>(secondResult.ScreenSettings);
            Assert.Equal(ViewType.Detail, secondScreenSettings.ViewType);
            Assert.Equal("Department", secondScreenSettings.Settings.Title);
        }

        [Fact]
        public void FlowWithDepartmentsTarget_CanVisitDeleteScreen_AndReturnToGrid()
        {
            //arrange
            IFlowManager flowManager = serviceProvider!.GetRequiredService<IFlowManager>();

            //act
            var firstResult = flowManager.Start(initialFlow, TargetModules.Departments);
            var firstScreenSettings = (ScreenSettings<GridSettingsDescriptor>)firstResult.ScreenSettings;
            var selectedButton = firstScreenSettings.CommandButtons!.First(b => b.ShortString == "departments_DELETE");
            var secondResult = flowManager.Next
            (
                new GridRequest
                {
                    CommandButtonRequest = new CommandButtonRequest { NewSelection = selectedButton.ShortString },
                    Entity = new DepartmentModel { DepartmentID = 1 },
                    FlowState = ((Director)flowManager.Director).FlowState,
                    ViewType = ViewType.Grid
                }
            );

            //assert
            var secondScreenSettings = Assert.IsType<ScreenSettings<DetailFormSettingsDescriptor>>(secondResult.ScreenSettings);
            Assert.Equal(ViewType.Delete, secondScreenSettings.ViewType);
            Assert.Equal("Department", secondScreenSettings.Settings.Title);
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
