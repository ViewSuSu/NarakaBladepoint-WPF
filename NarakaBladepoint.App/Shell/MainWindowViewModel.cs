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

                if (_isQueuing)
                {
                    StartQueueTimer();
                }
                else
                {
                    // 不立即停止，让隐藏动画自然完成后再停止
                    // 这样用户不会感知到计时器的跳变
                    // 延迟 300ms 后停止（隐藏动画完成时间）
                    Task.Delay(300)
                        .ContinueWith(_ =>
                        {
                            if (!_isQueuing)
                            {
                                StopQueueTimer();
                            }
                        });
                }
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
            }
        }

        private System.Timers.Timer _queueTimer;

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

        /// <summary>
        /// 启动排队计时器
        /// </summary>
        private void StartQueueTimer()
        {
            // 重置计时
            QueueTime = 0;

            if (_queueTimer != null)
            {
                _queueTimer.Stop();
                _queueTimer.Dispose();
            }

            // 创建新的计时器，每秒触发一次
            _queueTimer = new System.Timers.Timer(1000);
            _queueTimer.Elapsed += (s, e) =>
            {
                QueueTime++;
            };
            _queueTimer.AutoReset = true;
            _queueTimer.Start();
        }

        /// <summary>
        /// 停止排队计时器
        /// </summary>
        private void StopQueueTimer()
        {
            if (_queueTimer != null)
            {
                _queueTimer.Stop();
                _queueTimer.Dispose();
                _queueTimer = null;
            }

            // 重置计时
            QueueTime = 0;
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

        private void RevemoveRegionByName(string regionName)
        {
            var region = regionManager.Regions[regionName];
            region.RemoveAll();
        }
    }
}
