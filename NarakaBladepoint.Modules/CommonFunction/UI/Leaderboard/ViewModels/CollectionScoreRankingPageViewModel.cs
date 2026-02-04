using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class CollectionScoreRankingPageViewModel : ViewModelBase
    {
        private readonly IAvatarProvider _avatarProvider;
        private List<ImageSource> _avatarSources = new();

        public ObservableCollection<CollectionScoreRowViewModel> PlayersSolo { get; } = new();
        public ObservableCollection<CollectionScoreRowViewModel> PlayersDuo { get; } = new();
        public ObservableCollection<CollectionScoreRowViewModel> PlayersTrio { get; } = new();

        public CollectionScoreRankingPageViewModel(IAvatarProvider avatarProvider)
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

            // 衡阳市排行
            var hengyangData = new[]
            {
                (name: "剑影翎澜", score: 1196701, excellentAppearance: 57, superiorAppearance: 1936),
                (name: "爱哭难咪", score: 1187020, excellentAppearance: 60, superiorAppearance: 1759),
                (name: "小朝doll", score: 1142740, excellentAppearance: 64, superiorAppearance: 1660),
                (name: "我就小藏带带", score: 1080540, excellentAppearance: 65, superiorAppearance: 1285),
                (name: "宗德桃子大王", score: 1069216, excellentAppearance: 46, superiorAppearance: 1843),
                (name: "鸿雀智行16888", score: 1029995, excellentAppearance: 60, superiorAppearance: 1306),
                (name: "一者无成", score: 1023060, excellentAppearance: 49, superiorAppearance: 1762),
                (name: "焖熊街垂琴", score: 1006812, excellentAppearance: 60, superiorAppearance: 1265),
                (name: "谁提盘啥了", score: 997226, excellentAppearance: 51, superiorAppearance: 1626),
            };

            for (int i = 0; i < hengyangData.Length; i++)
            {
                var data = hengyangData[i];
                PlayersSolo.Add(
                    new CollectionScoreRowViewModel
                    {
                        Rank = i + 1,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                        ExcellentAppearance = data.excellentAppearance,
                        SuperiorAppearance = data.superiorAppearance,
                    }
                );
            }

            // 湖南省排行
            var hunanData = new[]
            {
                (name: "星辰碎影", score: 1180000, excellentAppearance: 55, superiorAppearance: 1890),
                (name: "梦幻少年", score: 1150000, excellentAppearance: 62, superiorAppearance: 1740),
                (name: "白河烟雪", score: 1120000, excellentAppearance: 66, superiorAppearance: 1680),
                (name: "不朽名将", score: 1070000, excellentAppearance: 48, superiorAppearance: 1820),
                (name: "风华绝世", score: 1050000, excellentAppearance: 58, superiorAppearance: 1350),
                (name: "剑气如虹", score: 1010000, excellentAppearance: 63, superiorAppearance: 1290),
                (name: "浪迹天涯", score: 995000, excellentAppearance: 52, superiorAppearance: 1720),
                (name: "月影婵娟", score: 978000, excellentAppearance: 61, superiorAppearance: 1230),
                (name: "醉卧云霄", score: 965000, excellentAppearance: 50, superiorAppearance: 1580),
            };

            for (int i = 0; i < hunanData.Length; i++)
            {
                var data = hunanData[i];
                PlayersDuo.Add(
                    new CollectionScoreRowViewModel
                    {
                        Rank = i + 1,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                        ExcellentAppearance = data.excellentAppearance,
                        SuperiorAppearance = data.superiorAppearance,
                    }
                );
            }

            // 全服排行
            var globalData = new[]
            {
                (name: "绝世高手", score: 1210000, excellentAppearance: 59, superiorAppearance: 1950),
                (name: "天选之人", score: 1195000, excellentAppearance: 63, superiorAppearance: 1800),
                (name: "永劫之王", score: 1165000, excellentAppearance: 67, superiorAppearance: 1710),
                (name: "十年剑心", score: 1090000, excellentAppearance: 50, superiorAppearance: 1850),
                (name: "清风揽月", score: 1075000, excellentAppearance: 56, superiorAppearance: 1400),
                (name: "无双剑圣", score: 1025000, excellentAppearance: 64, superiorAppearance: 1310),
                (name: "岁月轮回", score: 1015000, excellentAppearance: 51, superiorAppearance: 1780),
                (name: "梦幻仙踪", score: 995000, excellentAppearance: 62, superiorAppearance: 1250),
                (name: "红尘一笑", score: 978000, excellentAppearance: 52, superiorAppearance: 1620),
            };

            for (int i = 0; i < globalData.Length; i++)
            {
                var data = globalData[i];
                PlayersTrio.Add(
                    new CollectionScoreRowViewModel
                    {
                        Rank = i + 1,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        Score = data.score,
                        ExcellentAppearance = data.excellentAppearance,
                        SuperiorAppearance = data.superiorAppearance,
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

    internal class CollectionScoreRowViewModel
    {
        public int Rank { get; set; }
        public ImageSource Avatar { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int ExcellentAppearance { get; set; }
        public int SuperiorAppearance { get; set; }
    }
}
