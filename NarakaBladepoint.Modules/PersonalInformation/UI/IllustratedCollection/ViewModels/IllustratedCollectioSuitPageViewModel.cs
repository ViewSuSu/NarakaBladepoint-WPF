using System.Collections.ObjectModel;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.IllustratedCollection.ViewModels
{
    public class IllustratedCollectioSuitPageViewModel : ViewModelBase
    {
        public ObservableCollection<SuitCollectionViewModel> SuitCollections { get; set; }

        public IllustratedCollectioSuitPageViewModel()
        {
            // 初始化套装集合
            SuitCollections = new ObservableCollection<SuitCollectionViewModel>
            {
                new SuitCollectionViewModel
                {
                    Name = "共赴无间",
                    ProgressText1 = 3,
                    ProgressText2 = 7,
                    ScoreText = 200,
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                    },
                },
                new SuitCollectionViewModel
                {
                    Name = "黑猫警长系列",
                    ProgressText1 = 0,
                    ProgressText2 = 10,
                    ScoreText = 0,
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                    },
                },
                new SuitCollectionViewModel
                {
                    Name = "古龙江湖",
                    ProgressText1 = 0,
                    ProgressText2 = 6,
                    ScoreText = 0,
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                    },
                },
                new SuitCollectionViewModel
                {
                    Name = "我在精神病院学斩神",
                    ProgressText1 = 4,
                    ProgressText2 = 21,
                    ScoreText = 0,
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                        new RewardViewModel { ImageSource = null, Count = 1 },
                    },
                },
            };
        }
    }

    public class SuitCollectionViewModel : ViewModelBase
    {
        public string Name { get; set; } = string.Empty;

        // current and total as integers
        public int ProgressText1 { get; set; }
        public int ProgressText2 { get; set; }

        // score as integer
        public int ScoreText { get; set; }

        public ObservableCollection<RewardViewModel> Rewards { get; set; } =
            new ObservableCollection<RewardViewModel>();
        public DelegateCommand ViewDetailsCommand { get; set; }

        public SuitCollectionViewModel()
        {
            ViewDetailsCommand = new DelegateCommand(OnViewDetails);
        }

        private void OnViewDetails()
        {
            // 查看详情逻辑
        }
    }

    public class RewardViewModel : ViewModelBase
    {
        // 奖励相关属性
        // 图片源（可以是 pack uri 或 文件路径 / URL）
        public string ImageSource { get; set; }

        // 数量
        public int Count { get; set; }
    }
}
