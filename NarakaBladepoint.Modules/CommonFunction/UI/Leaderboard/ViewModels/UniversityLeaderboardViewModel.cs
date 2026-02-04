using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class UniversityLeaderboardViewModel : ViewModelBase
    {
        private readonly IAvatarProvider _avatarProvider;
        private List<ImageSource> _avatarSources = new();

        public ObservableCollection<UniversityLeaderboardRowViewModel> UniversityList1 { get; } = new();
        public ObservableCollection<UniversityLeaderboardRowViewModel> UniversityList2 { get; } = new();

        public UniversityLeaderboardViewModel(IAvatarProvider avatarProvider)
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

            // 浙江省高校 (UniversityList1) - 参考图片数据
            var zhejangUniversity = new[]
            {
                (rank: 1, name: "ejznn", university: "广州沙外经济职业技术学院", score: 8109),
                (rank: 2, name: "拾年叉何娇", university: "兰州博文科技学院", score: 7980),
                (rank: 3, name: "火罗国小师", university: "广西农业职业技术大学", score: 7431),
                (rank: 4, name: "0~夹日方长~0", university: "西安铁路职业技术学院", score: 7304),
                (rank: 5, name: "被撑成泡面渣", university: "北京信息科技大学", score: 7275),
                (rank: 6, name: "幸福逢耀拜骨汤", university: "海南师范大学", score: 7123),
                (rank: 7, name: "庆弄悲都雨天", university: "内蒙古北方职业技术学院", score: 7118),
                (rank: 8, name: "忠爱青眼", university: "青岛黄海学院", score: 7118),
                (rank: 9, name: "花雾荷", university: "扬州工业职业技术大学", score: 7106),
            };

            foreach (var data in zhejangUniversity)
            {
                UniversityList1.Add(
                    new UniversityLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        University = data.university,
                        Score = data.score,
                    }
                );
            }

            // 全服 (UniversityList2) - 扩展数据
            var quanFu = new[]
            {
                (rank: 1, name: "学霸达人", university: "清华大学", score: 8200),
                (rank: 2, name: "校园之星", university: "北京大学", score: 8150),
                (rank: 3, name: "综合能力强", university: "复旦大学", score: 8050),
                (rank: 4, name: "技术牛人", university: "浙江大学", score: 7950),
                (rank: 5, name: "全能选手", university: "上海交通大学", score: 7850),
                (rank: 6, name: "天才少年", university: "南京大学", score: 7750),
                (rank: 7, name: "精英学生", university: "武汉大学", score: 7650),
                (rank: 8, name: "优秀代表", university: "西安交通大学", score: 7550),
                (rank: 9, name: "学院之光", university: "四川大学", score: 7450),
            };

            foreach (var data in quanFu)
            {
                UniversityList2.Add(
                    new UniversityLeaderboardRowViewModel
                    {
                        Rank = data.rank,
                        Avatar = GetRandomAvatar(random),
                        Name = data.name,
                        University = data.university,
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

    internal class UniversityLeaderboardRowViewModel
    {
        public int Rank { get; set; }
        public ImageSource Avatar { get; set; }
        public string Name { get; set; }
        public string University { get; set; }
        public int Score { get; set; }
    }
}
