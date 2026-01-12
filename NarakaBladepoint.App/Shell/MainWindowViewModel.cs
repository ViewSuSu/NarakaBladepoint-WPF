using NarakaBladepoint.App.Shell.Infrastructure;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.Social.UI.Setting.Views;
using NarakaBladepoint.Modules.TopUp.UI.Views;
using NarakaBladepoint.Shared.Consts;

namespace NarakaBladepoint.App.Shell
{
    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly HomePageVisualNavigator homePageVisualNavigator;

        private DelegateCommand _exitGameCommand;

        public MainWindowViewModel(
            IContainerProvider containerProvider,
            HomePageVisualNavigator homePageVisualNavigator
        )
            : base(containerProvider)
        {
            this.homePageVisualNavigator = homePageVisualNavigator;
            this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                .Subscribe(
                    args =>
                    {
                        if (!IsCanNavigate(args.ViewName))
                        {
                            return;
                        }

                        this.homePageVisualNavigator.RequestNavigate(args.ViewName, args.Parameter);
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>()
                .Subscribe(() => this.homePageVisualNavigator.RemoveTop(), ThreadOption.UIThread);

            this.eventAggregator.GetEvent<RemoveAllHomePageRegionEvent>()
                .Subscribe(() => this.homePageVisualNavigator.RemoveAll(), ThreadOption.UIThread);

            this.eventAggregator.GetEvent<LoadMainContentRegionEvent>()
                .Subscribe(
                    (args) =>
                    {
                        if (!IsCanNavigate(args.ViewName))
                        {
                            return;
                        }

                        if (args.Parameter != default)
                            this.regionManager.RequestNavigate(
                                GlobalConstant.MainContentRegion,
                                args.ViewName,
                                args.Parameter
                            );
                        else
                            this.regionManager.RequestNavigate(
                                GlobalConstant.MainContentRegion,
                                args.ViewName
                            );
                    },
                    ThreadOption.UIThread
                );

            this.eventAggregator.GetEvent<RemoveMainContentRegionEvent>()
                .Subscribe(
                    () =>
                    {
                        RevemoveRegionByName(GlobalConstant.MainContentRegion);
                    },
                    ThreadOption.UIThread
                );
        }

        private bool IsCanNavigate(string viewName)
        {
            foreach (var region in regionManager.Regions)
            {
                foreach (var view in region.ActiveViews)
                {
                    if (view.GetType().Name == viewName)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public DelegateCommand ExitGameCommand =>
            _exitGameCommand ??= new DelegateCommand(() =>
            {
                if (regionManager.Regions.Any(x => x.ActiveViews.Any()))
                {
                    return;
                }
                this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(SettingPage)));
            });

        private void RevemoveRegionByName(string regionName)
        {
            var region = regionManager.Regions[regionName];
            region.RemoveAll();
        }
    }
}
