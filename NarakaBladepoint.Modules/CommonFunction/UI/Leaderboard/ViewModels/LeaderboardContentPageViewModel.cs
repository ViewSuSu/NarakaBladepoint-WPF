using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class LeaderboardContentPageViewModel : ViewModelBase
    {
        private readonly IAvatarProvider _avatarProvider;
        private List<ImageSource> _avatarSources = new();

        public ObservableCollection<LeaderboardRowViewModel> PlayersSolo { get; } = new();
        public ObservableCollection<LeaderboardRowViewModel> PlayersDuo { get; } = new();
        public ObservableCollection<LeaderboardRowViewModel> PlayersTrio { get; } = new();

        public LeaderboardRowViewModel Unranked { get; set; }

        public LeaderboardContentPageViewModel(IAvatarProvider avatarProvider)
        {
            _avatarProvider = avatarProvider;
            InitializeData();
        }

        private async void InitializeData()
        {
            try
            {
                var avatars = await _avatarProvider.GetAvatarsAsync();
                _avatarSources = avatars.Select(a => a.ImageSource).ToList();
            }
            catch
            {
                _avatarSources = new List<ImageSource>();
            }

            var random = new Random();

            for (int i = 1; i <= 20; i++)
            {
                var avatar = GetRandomAvatar(random);

                PlayersSolo.Add(
                    new LeaderboardRowViewModel
                    {
                        Rank = i,
                        Avatar = avatar,
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
                        Avatar = GetRandomAvatar(random),
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
                        Avatar = GetRandomAvatar(random),
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
                Avatar = GetRandomAvatar(random),
                Name = "野排牢张",
                RealmIcon = "",
                RealmName = "凡尘武师",
                Score = 3000,
                Matches = 0,
            };
        }

        private ImageSource GetRandomAvatar(Random random)
        {
            if (_avatarSources.Count == 0)
                return null;

            return _avatarSources[random.Next(_avatarSources.Count)];
        }

        // ViewModelBase/BindableBase already provides property change notification.
    }

    internal class LeaderboardRowViewModel
    {
        public int Rank { get; set; }
        public ImageSource Avatar { get; set; }
        public string Name { get; set; }
        public string RealmIcon { get; set; }
        public string RealmName { get; set; }
        public int Score { get; set; }
        public int Matches { get; set; }
    }
}
