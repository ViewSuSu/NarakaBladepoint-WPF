using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Leaderboard.ViewModels
{
    internal class TournamentChampionPageViewModel : ViewModelBase
    {
        public ObservableCollection<TournamentChampionRowViewModel> ChampionList { get; } = new();

        public TournamentChampionPageViewModel()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            // 获取所有冠军图片
            var championImages = ResourceImageReader.GetAllTournamentChampionImages();
            
            foreach (var image in championImages)
            {
                var fileName = image.GetFileName();
                ChampionList.Add(
                    new TournamentChampionRowViewModel
                    {
                        ChampionImage = image,
                        ChampionName = fileName ?? "Unknown Champion",
                    }
                );
            }
        }
    }

    internal class TournamentChampionRowViewModel
    {
        public ImageSource ChampionImage { get; set; }
        public string ChampionName { get; set; }
    }
}
