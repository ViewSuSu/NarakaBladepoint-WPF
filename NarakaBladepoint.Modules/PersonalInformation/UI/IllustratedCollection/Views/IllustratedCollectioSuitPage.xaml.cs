using System.Collections.ObjectModel;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.IllustratedCollection.Views
{
    /// <summary>
    /// IllustratedCollectioSuitPage.xaml 的交互逻辑
    /// </summary>
    public partial class IllustratedCollectioSuitPage : UserControlBase
    {
        public IllustratedCollectioSuitPage()
        {
            InitializeComponent();

            // provide sample data for design / testing
            var vm = new SampleViewModel
            {
                SuitCollections = new ObservableCollection<SuitModel>
                {
                    new SuitModel
                    {
                        Name = "共赴无间",
                        ProgressText1 = "3",
                        ProgressText2 = "6",
                        ScoreText = "200",
                        Rewards = new ObservableCollection<RewardModel>
                        {
                                    new RewardModel { ImageSource = null, Count = 1 },
                                    new RewardModel { ImageSource = null, Count = 2 },
                                    new RewardModel { ImageSource = null, Count = 3 }
                        }
                    },
                    new SuitModel
                    {
                        Name = "冷月",
                        ProgressText1 = "1",
                        ProgressText2 = "4",
                        ScoreText = "120",
                        Rewards = new ObservableCollection<RewardModel>
                        {
                                    new RewardModel { ImageSource = null, Count = 1 },
                                    new RewardModel { ImageSource = null, Count = 1 },
                                    new RewardModel { ImageSource = null, Count = 1 }
                        }
                    },
                    new SuitModel
                    {
                        Name = "苍穹",
                        ProgressText1 = "5",
                        ProgressText2 = "10",
                        ScoreText = "500",
                        Rewards = new ObservableCollection<RewardModel>
                        {
                                    new RewardModel { ImageSource = null, Count = 1 },
                                    new RewardModel { ImageSource = null, Count = 1 },
                                    new RewardModel { ImageSource = null, Count = 1 }
                        }
                    }
                }
            };

            DataContext = vm;
        }
    }

    internal class SampleViewModel
    {
        public ObservableCollection<SuitModel> SuitCollections { get; set; } = new ObservableCollection<SuitModel>();
    }

    internal class SuitModel
    {
        public string Name { get; set; } = string.Empty;
        public string ProgressText1 { get; set; } = "0";
        public string ProgressText2 { get; set; } = "1";
        public string ScoreText { get; set; } = "0";
        public ObservableCollection<RewardModel> Rewards { get; set; } = new ObservableCollection<RewardModel>();
    }

    internal class RewardModel
    {
        public string? ImageSource { get; set; }
        public int Count { get; set; } = 1;
    }
}
