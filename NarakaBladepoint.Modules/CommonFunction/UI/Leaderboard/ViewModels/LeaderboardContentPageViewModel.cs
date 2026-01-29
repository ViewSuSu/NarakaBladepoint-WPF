using System.Collections.ObjectModel;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class LeaderboardContentPageViewModel : ViewModelBase
    {
        public ObservableCollection<LeaderboardRowViewModel> PlayersSolo { get; } = new();
        public ObservableCollection<LeaderboardRowViewModel> PlayersDuo { get; } = new();
        public ObservableCollection<LeaderboardRowViewModel> PlayersTrio { get; } = new();

        public LeaderboardRowViewModel Unranked { get; set; }

        public LeaderboardContentPageViewModel()
        {
            // design-time sample data (5 items)
            for (int i = 1; i <= 5; i++)
            {
                PlayersSolo.Add(
                    new LeaderboardRowViewModel
                    {
                        Rank = i,
                        Avatar = "",
                        Name = $"玩家{i}",
                        RealmIcon = "",
                        RealmName = i <= 3 ? "穹苍魁首" : "日曜名宿",
                        Score = 5870 - (i - 1) * 100,
                        Matches = 70 + i,
                    }
                );

                PlayersDuo.Add(
                    new LeaderboardRowViewModel
                    {
                        Rank = i,
                        Avatar = "",
                        Name = $"双人玩家{i}",
                        RealmIcon = "",
                        RealmName = i <= 3 ? "穹苍魁首" : "日曜名宿",
                        Score = 5800 - (i - 1) * 90,
                        Matches = 60 + i,
                    }
                );

                PlayersTrio.Add(
                    new LeaderboardRowViewModel
                    {
                        Rank = i,
                        Avatar = "",
                        Name = $"三人玩家{i}",
                        RealmIcon = "",
                        RealmName = i <= 3 ? "穹苍魁首" : "日曜名宿",
                        Score = 5700 - (i - 1) * 80,
                        Matches = 50 + i,
                    }
                );
            }

            Unranked = new LeaderboardRowViewModel
            {
                Rank = 0,
                Avatar = "",
                Name = "野排牢张",
                RealmIcon = "",
                RealmName = "凡尘武师",
                Score = 3000,
                Matches = 0,
            };
        }

        // ViewModelBase/BindableBase already provides property change notification.
    }

    internal class LeaderboardRowViewModel
    {
        public int Rank { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string RealmIcon { get; set; }
        public string RealmName { get; set; }
        public int Score { get; set; }
        public int Matches { get; set; }
    }
}
