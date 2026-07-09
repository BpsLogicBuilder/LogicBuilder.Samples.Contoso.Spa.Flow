using Contoso.Spa.Flow.Interfaces;
using Contoso.Spa.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;

namespace Contoso.Spa.Flow
{
    public class Director(IFlowManager flowManager) : AppDirectorBase
    {
        private readonly IFlowManager _flowManager = flowManager;

        protected override IRulesCache RulesCache => _flowManager.RulesCache;
        protected override IFlowActivity FlowActivity => _flowManager.FlowActivity;
        protected override Progress Progress => _flowManager.Progress;

        public FlowState FlowState
        {
            get
            {
                return new FlowState
                {
                    Driver = this._driver,
                    Selection = this._selection,
                    CallingModuleDriverStack = [.. this._callingModuleDriverStack.OfType<string>()],
                    CallingModuleStack = [.. this._callingModuleStack.OfType<string>()],
                    ModuleBeginName = this._moduleBeginName,
                    ModuleEndName = this._moduleEndName
                };
            }
            set
            {
                this._driver = value.Driver;
                this._selection = value.Selection;
                this._callingModuleDriverStack = new System.Collections.Stack(value.CallingModuleDriverStack.Reverse<string>().ToList());
                this._callingModuleStack = new System.Collections.Stack(value.CallingModuleStack.Reverse<string>().ToList());
                this._moduleBeginName = value.ModuleBeginName;
                this._moduleEndName = value.ModuleEndName;
            }
        }

        public override void SetCurrentBusinessBackupData() => _flowManager.SetCurrentBusinessBackupData();
    }
}
