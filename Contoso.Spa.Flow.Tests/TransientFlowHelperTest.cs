using Contoso.Domain.Entities;
using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.Responses.TransientFlows;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Contoso.Spa.Flow.Tests
{
    public class TransientFlowHelperTest
    {
        static TransientFlowHelperTest()
        {
            Initialize();
        }
        #region Fields
        private static IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void TransientFlowH_CreatesSelector()
        {
            //arrange
            ITransientFlowHelper flowHelper = serviceProvider!.GetRequiredService<ITransientFlowHelper>();

            //act
            var result = flowHelper.RunSelectorFlow(new Requests.TransientFlows.SelectorFlowRequest 
            { 
                Entity = new CourseModel { },
                ReloadItemsFlowName = "get-selector"
            });

            //assert
            Assert.NotNull(result);
            var response = Assert.IsType<SelectorFlowResponse>(result);
            Assert.NotNull(response.Selector);
            Assert.IsType<SelectorLambdaDescriptor>(response.Selector);
        }

        #region Helpers
        [MemberNotNull(nameof(serviceProvider))]
        private static void Initialize()
        {
            serviceProvider ??= new ServiceCollection()
                .AddLogging()
                .AddSpaFlowServices()
                .AddAutoMapperServices()
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}
