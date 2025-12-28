using Nakara.Framework.Core.Bases.Commands;
using Nakara.Framework.Core.Bases.ViewModels;
using Nakara.Modules.EventCenter.Domain.Consts;
using Nakara.Modules.EventCenter.Domain.Events;
using Nakara.Modules.EventCenter.UI.LatestNews.Views;

namespace Nakara.Modules.EventCenter.UI.Main.ViewModels
{
    internal class EventCenterMainUserControlViewModel : CanReturnToMainWindowPageViewModelBase
    {
        private readonly IRegionManager regionManager;

        public EventCenterMainUserControlViewModel(
            IEventAggregator eventAggregator,
            IRegionManager regionManager
        )
            : base(eventAggregator) { }
    }
}
