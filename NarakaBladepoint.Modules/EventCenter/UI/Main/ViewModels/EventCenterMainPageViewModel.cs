using NarakaBladepoint.Modules.EventCenter.Domain.EventParameters;
using NarakaBladepoint.Modules.EventCenter.Domain.Events;

namespace NarakaBladepoint.Modules.EventCenter.UI.Main.ViewModels
{
    internal class EventCenterMainPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        /// <summary>
        /// 是否选中近期热点Tab
        /// </summary>
        private bool _isSelectedLatestNews = false;
        public bool IsSelectedLatestNews
        {
            get { return _isSelectedLatestNews; }
            set
            {
                _isSelectedLatestNews = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中限时活动Tab
        /// </summary>
        private bool _isSelectedLimitedEvent = false;
        public bool IsSelectedLimitedEvent
        {
            get { return _isSelectedLimitedEvent; }
            set
            {
                _isSelectedLimitedEvent = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中揽月阁Tab
        /// </summary>
        private bool _isSelectedMoonGazingPavilion = false;
        public bool IsSelectedMoonGazingPavilion
        {
            get { return _isSelectedMoonGazingPavilion; }
            set
            {
                _isSelectedMoonGazingPavilion = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中白泽月卡Tab
        /// </summary>
        private bool _isSelectedBaiZeCard = false;
        public bool IsSelectedBaiZeCard
        {
            get { return _isSelectedBaiZeCard; }
            set
            {
                _isSelectedBaiZeCard = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中绑定有礼Tab
        /// </summary>
        private bool _isSelectedBindReward = false;
        public bool IsSelectedBindReward
        {
            get { return _isSelectedBindReward; }
            set
            {
                _isSelectedBindReward = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中英雄归来Tab
        /// </summary>
        private bool _isSelectedWelcomeBack = false;
        public bool IsSelectedWelcomeBack
        {
            get { return _isSelectedWelcomeBack; }
            set
            {
                _isSelectedWelcomeBack = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中传火Tab
        /// </summary>
        private bool _isSelectedPassingTheFlame = false;
        public bool IsSelectedPassingTheFlame
        {
            get { return _isSelectedPassingTheFlame; }
            set
            {
                _isSelectedPassingTheFlame = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中蓬莱指南Tab
        /// </summary>
        private bool _isSelectedPenglaiGuide = false;
        public bool IsSelectedPenglaiGuide
        {
            get { return _isSelectedPenglaiGuide; }
            set
            {
                _isSelectedPenglaiGuide = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中更新公告Tab
        /// </summary>
        private bool _isSelectedPatchNote = false;
        public bool IsSelectedPatchNote
        {
            get { return _isSelectedPatchNote; }
            set
            {
                _isSelectedPatchNote = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中网易支付好礼Tab
        /// </summary>
        private bool _isSelectedNetEasePayRewards = false;
        public bool IsSelectedNetEasePayRewards
        {
            get { return _isSelectedNetEasePayRewards; }
            set
            {
                _isSelectedNetEasePayRewards = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否选中宝箱定向保底Tab
        /// </summary>
        private bool _isSelectedTargetedChestGuarantee = false;
        public bool IsSelectedTargetedChestGuarantee
        {
            get { return _isSelectedTargetedChestGuarantee; }
            set
            {
                _isSelectedTargetedChestGuarantee = value;
                RaisePropertyChanged();
            }
        }

        public EventCenterMainPageViewModel() { }

        /// <summary>
        /// 当导航到此页面时执行
        /// </summary>
        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            // 尝试从导航参数中获取各个Tab的选中状态
            if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedLatestNews),
                    out bool isSelectedLatestNews
                )
            )
            {
                this.IsSelectedLatestNews = isSelectedLatestNews;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedLimitedEvent),
                    out bool isSelectedLimitedEvent
                )
            )
            {
                this.IsSelectedLimitedEvent = isSelectedLimitedEvent;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedMoonGazingPavilion),
                    out bool isSelectedMoonGazingPavilion
                )
            )
            {
                this.IsSelectedMoonGazingPavilion = isSelectedMoonGazingPavilion;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedBaiZeCard),
                    out bool isSelectedBaiZeCard
                )
            )
            {
                this.IsSelectedBaiZeCard = isSelectedBaiZeCard;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedBindReward),
                    out bool isSelectedBindReward
                )
            )
            {
                this.IsSelectedBindReward = isSelectedBindReward;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedWelcomeBack),
                    out bool isSelectedWelcomeBack
                )
            )
            {
                this.IsSelectedWelcomeBack = isSelectedWelcomeBack;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedPassingTheFlame),
                    out bool isSelectedPassingTheFlame
                )
            )
            {
                this.IsSelectedPassingTheFlame = isSelectedPassingTheFlame;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedPenglaiGuide),
                    out bool isSelectedPenglaiGuide
                )
            )
            {
                this.IsSelectedPenglaiGuide = isSelectedPenglaiGuide;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedPatchNote),
                    out bool isSelectedPatchNote
                )
            )
            {
                this.IsSelectedPatchNote = isSelectedPatchNote;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedNetEasePayRewards),
                    out bool isSelectedNetEasePayRewards
                )
            )
            {
                this.IsSelectedNetEasePayRewards = isSelectedNetEasePayRewards;
            }
            else if (
                navigationContext.Parameters.TryGetValue(
                    nameof(NavigatedToEventCenterParameter.IsSelectedTargetedChestGuarantee),
                    out bool isSelectedTargetedChestGuarantee
                )
            )
            {
                this.IsSelectedTargetedChestGuarantee = isSelectedTargetedChestGuarantee;
            }
        }
    }
}
