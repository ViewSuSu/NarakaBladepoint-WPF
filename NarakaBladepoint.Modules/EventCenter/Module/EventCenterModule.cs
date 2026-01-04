using NarakaBladepoint.Modules.EventCenter.UI.BaiZeCard.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.BaiZeCard.Views;
using NarakaBladepoint.Modules.EventCenter.UI.BindReward.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.BindReward.Views;
using NarakaBladepoint.Modules.EventCenter.UI.LatestNews.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.LatestNews.Views;
using NarakaBladepoint.Modules.EventCenter.UI.Main.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.Main.Views;
using NarakaBladepoint.Modules.EventCenter.UI.MoonGazingPavilion.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.MoonGazingPavilion.Views;
using NarakaBladepoint.Modules.EventCenter.UI.NetEasePayRewards.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.NetEasePayRewards.Views;
using NarakaBladepoint.Modules.EventCenter.UI.PassingTheFlame.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.PassingTheFlame.Views;
using NarakaBladepoint.Modules.EventCenter.UI.PatchNote.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.PatchNote.Views;
using NarakaBladepoint.Modules.EventCenter.UI.PenglaiGuide.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.PenglaiGuide.Views;
using NarakaBladepoint.Modules.EventCenter.UI.TargetedChestGuarantee.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.TargetedChestGuarantee.Views;
using NarakaBladepoint.Modules.EventCenter.UI.TimeLimitedEvent.ViewModels;
using NarakaBladepoint.Modules.EventCenter.UI.TimeLimitedEvent.Views;

namespace NarakaBladepoint.Modules.EventCenter.Module
{
    internal class EventCenterModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                EventCenterMainUserControl,
                EventCenterMainUserControlViewModel
            >();
            containerRegistry.RegisterForNavigation<LatestNewsPage, LatestNewsPageViewModel>();
            containerRegistry.RegisterForNavigation<PatchNotePage, PatchNotePageViewModel>();
            containerRegistry.RegisterForNavigation<
                NetEasePayRewardsPage,
                NetEasePayRewardsPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                MoonGazingPavilionPage,
                MoonGazingPavilionPageViewModel
            >();
            containerRegistry.RegisterForNavigation<BaiZeCardPage, BaiZeCardPageViewModel>();
            containerRegistry.RegisterForNavigation<BindRewardPage, BindRewardPageViewModel>();
            containerRegistry.RegisterForNavigation<PenglaiGuidePage, PenglaiGuidePageViewModel>();
            containerRegistry.RegisterForNavigation<
                TargetedChestGuaranteePage,
                TargetedChestGuaranteePageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                PassingTheFlamePage,
                PassingTheFlamePageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                TimeLimitedEventPage,
                TimeLimitedEventPageViewModel
            >();
        }
    }
}
