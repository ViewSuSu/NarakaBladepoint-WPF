using NarakaBladepoint.Framework.Core.Infrastructure;
using NarakaBladepoint.Modules.CommonFunction.Domain.Events;

namespace NarakaBladepoint.Modules.CommonFunction.Domain.Bases
{
    internal abstract class CommonFunctionPageViewModelBase : ViewModelBase, IActiveAware
    {
        internal static event EventHandler? MainContentNavigator_Removed;

        public CommonFunctionPageViewModelBase()
        {
            MainContentNavigator.Removed += MainContentNavigator_Removed;
        }

        private DelegateCommand _returnToHallCommand;

        /// <summary>
        /// 返回大厅
        /// </summary>
        public DelegateCommand ReturnToHallCommand =>
            _returnToHallCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<NavigateToHallEvent>().Publish();
            });

        public event EventHandler IsActiveChanged;

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (!value)
                {
                    MainContentNavigator_Removed?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
