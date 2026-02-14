using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Resources;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Controls;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.IllustratedCollection.ViewModels
{
    public class IllustratedCollectioSuitPageViewModel : ViewModelBase
    {
        public ObservableCollection<SuitCollectionViewModel> SuitCollections { get; set; }

        public IllustratedCollectioSuitPageViewModel()
        {
            // 初始化套装集合：从资源读取 IllustratedCollection 根图片，每个图片作为一个套装的封面
            SuitCollections = new ObservableCollection<SuitCollectionViewModel>();

            var imgs = ResourceImageReader.GetAllIllustratedCollectionRootImages();
            if (imgs != null && imgs.Count > 0)
            {
                for (int i = 0; i < imgs.Count; i++)
                {
                    var img = imgs[i];
                    // 根据截图及资源，按索引设置不同的进度与奖励点位
                    SuitCollectionViewModel model;

                    switch (i)
                    {
                        // 第一行：收集进度 3/17，累计加分 200，奖励点位 1/6/12/17
                        case 0:
                            model = new SuitCollectionViewModel
                            {
                                Name = img.GetFileName(),
                                ProgressbarValue = 3,
                                ProgressbarMaximum = 17,
                                ScoreText = 200,
                                CoverImage = img,
                                Rewards = new ObservableCollection<RewardViewModel>
                                {
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_3, Count = 1, Value = 200 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 6, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 12, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_5, Count = 17, Value = 1500 },
                                }
                            };
                            break;

                        // 第二行：收集进度 0/10，累计加分 0，奖励点位 1/5/10
                        case 1:
                            model = new SuitCollectionViewModel
                            {
                                Name = img.GetFileName(),
                                ProgressbarValue = 3,
                                ProgressbarMaximum = 10,
                                ScoreText = 0,
                                CoverImage = img,
                                Rewards = new ObservableCollection<RewardViewModel>
                                {
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_3, Count = 1, Value = 200 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 5, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 10, Value = 1 },
                                }
                            };
                            break;

                        // 第三行：收集进度 0/6，累计加分 0，奖励点位 1/3/6
                        case 2:
                            model = new SuitCollectionViewModel
                            {
                                Name = img.GetFileName(),
                                ProgressbarValue = 2,
                                ProgressbarMaximum = 6,
                                ScoreText = 0,
                                CoverImage = img,
                                Rewards = new ObservableCollection<RewardViewModel>
                                {
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_3, Count = 1, Value = 200 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 3, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 6, Value = 1 },
                                }
                            };
                            break;

                        // 第四行（及其他默认）：收集进度 4/21，累计加分 200，奖励点位 1/6/11/21
                        default:
                            model = new SuitCollectionViewModel
                            {
                                Name = img.GetFileName(),
                                ProgressbarValue = 15,
                                ProgressbarMaximum = 21,
                                ScoreText = 200,
                                CoverImage = img,
                                Rewards = new ObservableCollection<RewardViewModel>
                                {
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_3, Count = 1, Value = 200 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 6, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_4, Count = 11, Value = 1 },
                                    new RewardViewModel { ImageSource = ResourceImageReader.IllustratedCollectionImage_5, Count = 21, Value = 1500 },
                                }
                            };
                            break;
                    }

                    SuitCollections.Add(model);
                }
            }
        }
    }

    public class SuitCollectionViewModel : ViewModelBase
    {
        public string Name { get; set; } = string.Empty;

        // current and total as integers
        public int ProgressbarValue { get; set; }
        public int ProgressbarMaximum { get; set; }

        // score as integer
        public int ScoreText { get; set; }
        public ObservableCollection<RewardViewModel> Rewards { get; set; } =
            new ObservableCollection<RewardViewModel>();
        // cover image for the suit
        public ImageSource CoverImage { get; set; }
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
}
