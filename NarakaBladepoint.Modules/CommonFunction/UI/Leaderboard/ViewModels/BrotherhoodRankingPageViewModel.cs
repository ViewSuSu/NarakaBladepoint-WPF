using System.Collections.ObjectModel;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class BrotherhoodRankingPageViewModel : ViewModelBase
    {
        public ObservableCollection<BrotherhoodRowViewModel> BrotherhoodList { get; } = new();

        public BrotherhoodRankingPageViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            var brotherhoodData = new[]
            {
                (rank: 1, name: "like", realmName: "义火烧原"),
                (rank: 2, name: "神与", realmName: "义火烧原"),
                (rank: 3, name: "Steam转网易", realmName: "义火烧原"),
                (rank: 4, name: "hello", realmName: "义火烧原"),
                (rank: 5, name: "浪缘", realmName: "义火烧原"),
                (rank: 6, name: "73集团", realmName: "义火烧原"),
                (rank: 7, name: "Eevee", realmName: "义火烧原"),
                (rank: 8, name: "小雨天气", realmName: "义火烧原"),
                (rank: 9, name: "森林里的小鹿", realmName: "义火烧原"),
            };

            var random = new Random();
            foreach (var data in brotherhoodData)
            {
                BrotherhoodList.Add(
                    new BrotherhoodRowViewModel
                    {
                        Rank = data.rank,
                        Name = data.name,
                        RealmName = data.realmName,
                        Score = 1263417 - (data.rank - 1) * 50000 + random.Next(-10000, 10000),
                    }
                );
            }
        }
    }

    internal class BrotherhoodRowViewModel
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public string RealmName { get; set; }
        public int Score { get; set; }
    }
}
