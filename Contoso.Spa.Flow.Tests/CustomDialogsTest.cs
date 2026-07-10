using LogicBuilder.App.Spa.Forms.Parameters.Common;
using System;

namespace Contoso.Spa.Flow.Tests
{
    public class CustomDialogsTest
    {
        [Fact]
        public void DisplayEditFormThrowsArgumentException_ForInvalidViewType()
        {
            //arrange
            EditFormSettingsParameters setting = new            (
                "",
                "",
                new FormRequestDetailsParameters("", "", "", "", "", ""),
                [],
                []
            );
            CustomDialogs customDialogs = new(null!, null!);

            //act && assert
            Assert.Throws<ArgumentException>(() => customDialogs.DisplayEditForm(setting, ScreenSettings.Views.ViewType.Grid, []));
        }

        [Fact]
        public void DisplayDetailFormThrowsArgumentException_ForInvalidViewType()
        {
            //arrange
            DetailFormSettingsParameters setting = new(
                "",
                "",
                new FormRequestDetailsParameters("", "", "", "", "", ""),
                []
            );
            CustomDialogs customDialogs = new(null!, null!);

            //act && assert
            Assert.Throws<ArgumentException>(() => customDialogs.DisplayDetailForm(setting, ScreenSettings.Views.ViewType.Grid, []));
        }
    }
}
