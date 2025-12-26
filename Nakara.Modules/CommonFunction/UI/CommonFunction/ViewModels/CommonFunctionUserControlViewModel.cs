using Nakara.Modules.CommonFunction.UI.HeroList;
using Nakara.Modules.CommonFunction.UI.HeroList.Views;

namespace Nakara.Modules.CommonFunction.UI.CommonFunction.ViewModels
{
    public partial class CommonFunctionUserControlViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;

        public CommonFunctionUserControlViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            HeroListCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                    .Publish(nameof(HeroListUserControl));
            });
        }

        public DelegateCommand HeroListCommand { get; }
    }
}
