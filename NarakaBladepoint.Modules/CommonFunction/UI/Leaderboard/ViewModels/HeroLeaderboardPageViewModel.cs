using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class HeroLeaderboardPageViewModel : ViewModelBase
    {
        private readonly IAvatarProvider _avatarProvider;
        private List<ImageSource> _avatarSources = new();

        public ObservableCollection<HeroLeaderboardRowViewModel> HeroList1 { get; } = new();
        public ObservableCollection<HeroLeaderboardRowViewModel> HeroList2 { get; } = new();
        public ObservableCollection<HeroLeaderboardRowViewModel> HeroList3 { get; } = new();
        public ObservableCollection<HeroLeaderboardRowViewModel> HeroList4 { get; } = new();

        public HeroLeaderboardPageViewModel(IAvatarProvider avatarProvider)
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

            // 蒸湘区 (HeroList1)
            var zhengXiangQu = new[]
            {
                (rank: 1, name: "闪烁紫炫颜", score: 8556),
                (rank: 2, name: "呃哇哇哇下士", score: 8201),
                (rank: 3, name: "借秋の", score: 8180),
                (rank: 4, name: "ejznn", score: 8109),
                (rank: 5, name: "小小桶津神", score: 7982),
                (rank: 6, name: "拾年叉何娇", score: 7980),
                (rank: 7, name: "皓占月数次星星", score: 7900),
                (rank: 8, name: "怪我怪我怪我!", score: 7803),
            };

            foreach (var data in zhengXiangQu)
            {
                HeroList1.Add(
                    new HeroLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                    }
                );
            }

            // 衡阳市 (HeroList2)
            var hengYangShi = new[]
            {
                (rank: 1, name: "风华绝世", score: 8520),
                (rank: 2, name: "月影婵娟", score: 8245),
                (rank: 3, name: "剑气如虹", score: 8150),
                (rank: 4, name: "浪迹天涯", score: 8070),
                (rank: 5, name: "梦幻少年", score: 7950),
                (rank: 6, name: "不朽名将", score: 7920),
                (rank: 7, name: "白河烟雪", score: 7850),
                (rank: 8, name: "星辰碎影", score: 7780),
            };

            foreach (var data in hengYangShi)
            {
                HeroList2.Add(
                    new HeroLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                    }
                );
            }

            // 湖南省 (HeroList3)
            var huNanSheng = new[]
            {
                (rank: 1, name: "醉卧云霄", score: 8480),
                (rank: 2, name: "红尘一笑", score: 8210),
                (rank: 3, name: "清风揽月", score: 8140),
                (rank: 4, name: "十年剑心", score: 8050),
                (rank: 5, name: "岁月轮回", score: 7930),
                (rank: 6, name: "梦幻仙踪", score: 7900),
                (rank: 7, name: "无双剑圣", score: 7820),
                (rank: 8, name: "天选之人", score: 7750),
            };

            foreach (var data in huNanSheng)
            {
                HeroList3.Add(
                    new HeroLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                    }
                );
            }

            // 全服 (HeroList4)
            var quanFu = new[]
            {
                (rank: 1, name: "绝世高手", score: 8610),
                (rank: 2, name: "永劫之王", score: 8280),
                (rank: 3, name: "天下一剑", score: 8200),
                (rank: 4, name: "诗意年华", score: 8120),
                (rank: 5, name: "九霄剑心", score: 8010),
                (rank: 6, name: "凌云壮志", score: 7960),
                (rank: 7, name: "剑影天涯", score: 7880),
                (rank: 8, name: "星河璀璨", score: 7820),
            };

            foreach (var data in quanFu)
            {
                HeroList4.Add(
                    new HeroLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                    }
                );
            }
        }

        private ImageSource GetRandomAvatar(Random random)
        {
            if (_avatarSources.Count == 0)
                return null;

            return _avatarSources[random.Next(_avatarSources.Count)];
        }
    }

    internal class HeroLeaderboardRowViewModel
    {
        public int Rank { get; set; }
        public ImageSource Avatar { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
