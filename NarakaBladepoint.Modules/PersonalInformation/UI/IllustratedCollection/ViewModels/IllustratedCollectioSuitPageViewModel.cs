using System.Collections.ObjectModel;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;

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
                    ProgressText = "收集进度: 3/7",
                    ScoreText = "累计加分: 200",
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel()
                    }
                },
                new SuitCollectionViewModel
                {
                    Name = "黑猫警长系列",
                    ProgressText = "收集进度: 0/10",
                    ScoreText = "累计加分: 0",
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel()
                    }
                },
                new SuitCollectionViewModel
                {
                    Name = "古龙江湖",
                    ProgressText = "收集进度: 0/6",
                    ScoreText = "累计加分: 0",
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel()
                    }
                },
                new SuitCollectionViewModel
                {
                    Name = "我在精神病院学斩神",
                    ProgressText = "收集进度: 4/21",
                    ScoreText = "累计加分: 0",
                    Rewards = new ObservableCollection<RewardViewModel>
                    {
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel(),
                        new RewardViewModel()
                    }
                }
            };
        }
    }

    public class SuitCollectionViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string ProgressText { get; set; }
        public string ScoreText { get; set; }
        public ObservableCollection<RewardViewModel> Rewards { get; set; }
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
    }
}
