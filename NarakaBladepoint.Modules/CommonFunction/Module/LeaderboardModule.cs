using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels;
using NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.Views;

namespace NarakaBladepoint.Modules.CommonFunction.Module
{
    /// <summary>
    /// Leaderboard模块，注册排行榜相关页面和ViewModel
    /// 包括：排行榜主页、内容页、英雄排行、积分排名、聚义榜、世界冠军等
    /// </summary>
    internal class LeaderboardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册排行榜主页
            containerRegistry.RegisterForNavigation<LeaderboardPage, LeaderboardPageViewModel>();
            
            // 注册排行榜内容页
            containerRegistry.RegisterForNavigation<
                LeaderboardContentPage,
                LeaderboardContentPageViewModel
            >();
            
            // 注册英雄排行页
            containerRegistry.RegisterForNavigation<
                HeroLeaderboardPage,
                HeroLeaderboardPageViewModel
            >();
            
            // 注册积分排名页
            containerRegistry.RegisterForNavigation<
                CollectionScoreRankingPage,
                CollectionScoreRankingPageViewModel
            >();
            
            // 注册聚义榜页
            containerRegistry.RegisterForNavigation<
                BrotherhoodRankingPage,
                BrotherhoodRankingPageViewModel
            >();
            
            // 注册世界冠军页
            containerRegistry.RegisterForNavigation<
                TournamentChampionPage,
                TournamentChampionPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                UniversityLeaderboard,
                UniversityLeaderboardViewModel
            >();
        }
    }
}
