using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly MainContentNavigator mainContentNavigator;
        private DelegateCommand _returnCommand;
        private DelegateCommand _cancelQueueCommand;

        /// <summary>
        /// 提示消息文本
        /// </summary>
        private string _tipMessage;
        public string TipMessage
        {
            get => _tipMessage;
            set
            {
                _tipMessage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否正在排队
        /// </summary>
        private bool _isQueuing;
        public bool IsQueuing
        {
            get => _isQueuing;
            set
            {
                if (_isQueuing == value)
                    return;

                _isQueuing = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 排队耗时（秒）
        /// </summary>
        private int _queueTime;
        public int QueueTime
        {
            get => _queueTime;
            set
            {
                _queueTime = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(QueueTimeFormatted));
            }
        }

        /// <summary>
        /// 格式化的排队时间 MM:SS
        /// </summary>
        public string QueueTimeFormatted
        {
            get
            {
                int minutes = _queueTime / 60;
                int seconds = _queueTime % 60;
                return $"{minutes:D2}:{seconds:D2}";
            }
        }

        public MainWindowViewModel(
            IContainerProvider containerProvider,
            HomePageVisualNavigator homePageVisualNavigator,
            MainContentNavigator mainContentNavigator
        )
            : base(containerProvider)
        {
            this.homePageVisualNavigator = homePageVisualNavigator;
            this.mainContentNavigator = mainContentNavigator;

            // 订阅提示消息事件
            this.eventAggregator.GetEvent<TipMessageEvent>()
                .Subscribe(
                    message =>
                    {
                        TipMessage = message;
                    },
                    ThreadOption.UIThread
                );

            // 订阅排队状态变化事件
            this.eventAggregator.GetEvent<QueueStatusChangedEvent>()
                .Subscribe(
                    isQueuing =>
                    {
                        IsQueuing = isQueuing;
                    },
                    ThreadOption.UIThread
                );

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

        public DelegateCommand ReturnCommand =>
            _returnCommand ??= new DelegateCommand(() =>
            {
                if (homePageVisualNavigator.HasActiveRegion)
                {
                    this.homePageVisualNavigator.RemoveTop();
                }
                else if (mainContentNavigator.HasActiveContent)
                {
                    this.mainContentNavigator.Remove();
                }
                else
                {
                    this.eventAggregator.GetEvent<LoadHomePageRegionEvent>()
                        .Publish(new NavigationArgs(nameof(SettingPage)));
                }
            });

        public DelegateCommand CancelQueueCommand =>
            _cancelQueueCommand ??= new DelegateCommand(() =>
            {
                IsQueuing = false;
                eventAggregator.GetEvent<QueueStatusChangedEvent>().Publish(false);
            });

        private void RevemoveRegionByName(string regionName)
        {
            var region = regionManager.Regions[regionName];
            region.RemoveAll();
        }
    }
}
