using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Resources;
using Prism.Commands;

namespace NarakaBladepoint.Modules.EventCenter.UI.TimeLimitedEvent.ViewModels
{
    internal class TimeLimitedEventPageOneViewModel : ViewModelBase
    {
        private ObservableCollection<RewardItemViewModel> _rewardItems;
        private ObservableCollection<TaskItemViewModel> _taskItems;
        private ObservableCollection<ImageSource> _timeLimitedEventImages2;
        private ObservableCollection<ExchangeItemViewModel> _exchangeItems;
        private ImageSource _backgroundImage;
        private int _selectedEventIndex = 0;

        public ObservableCollection<RewardItemViewModel> RewardItems
        {
            get => _rewardItems;
            set 
            { 
                _rewardItems = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 活动任务项集合
        /// </summary>
        public ObservableCollection<TaskItemViewModel> TaskItems
        {
            get => _taskItems;
            set 
            { 
                _taskItems = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 兑换物品集合
        /// </summary>
        public ObservableCollection<ExchangeItemViewModel> ExchangeItems
        {
            get => _exchangeItems;
            set 
            { 
                _exchangeItems = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 用户控件背景图片
        /// </summary>
        public ImageSource BackgroundImage
        {
            get => _backgroundImage;
            set 
            { 
                _backgroundImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 选中的事件索引
        /// </summary>
        public int SelectedEventIndex
        {
            get => _selectedEventIndex;
            set 
            { 
                _selectedEventIndex = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 时间限制事件 Images2 图片集合
        /// </summary>
        public ObservableCollection<ImageSource> TimeLimitedEventImages2
        {
            get => _timeLimitedEventImages2;
            set 
            { 
                _timeLimitedEventImages2 = value;
                RaisePropertyChanged();
            }
        }

        public TimeLimitedEventPageOneViewModel()
        {
            InitializeRewardItems();
            InitializeTaskItems();
            InitializeTimeLimitedEventImages2();
            InitializeExchangeItems();
            // 默认选中第一项（飞羽离弦），背景设置为默认背景
            SelectEvent("飞羽离弦");
        }

        private void InitializeRewardItems()
        {
            RewardItems = new ObservableCollection<RewardItemViewModel>();

            // 从 ResourceImageReader 读取所有时间限制事件奖励图片
            var rewardImages = ResourceImageReader.GetAllTimeLimitedEventRewardImages();

            foreach (var imageSource in rewardImages)
            {
                RewardItems.Add(new RewardItemViewModel
                {
                    ImageSource = imageSource,
                    ImageWidth = 50,
                    ButtonText = "20",
                    RewardCommand = new DelegateCommand(() => HandleRewardClick())
                });
            }
        }

        private void InitializeTaskItems()
        {
            TaskItems = new ObservableCollection<TaskItemViewModel>();

            // 创建7个任务项，前3个已完成，后4个未完成
            for (int i = 0; i < 7; i++)
            {
                bool isLocked = i>=1;

                TaskItems.Add(new TaskItemViewModel
                {
                    Index = i + 1,
                    IsLocked = isLocked
                });
            }
        }

        private void HandleRewardClick()
        {
            // 处理领取奖励的逻辑
        }

        /// <summary>
        /// 初始化时间限制事件 Images2 图片集合
        /// </summary>
        private void InitializeTimeLimitedEventImages2()
        {
            TimeLimitedEventImages2 = new ObservableCollection<ImageSource>();

            // 从 ResourceImageReader 读取所有时间限制事件 Images2 图片
            var images2 = ResourceImageReader.GetAllTimeLimitedEventImages2();

            foreach (var imageSource in images2)
            {
                TimeLimitedEventImages2.Add(imageSource);
            }
        }

        /// <summary>
        /// 初始化兑换物品集合
        /// </summary>
        private void InitializeExchangeItems()
        {
            ExchangeItems = new ObservableCollection<ExchangeItemViewModel>();

            var images3 = ResourceImageReader.GetAllTimeLimitedEventImages3();

            // 按照图片顺序创建11个兑换物品项，无重复
            var exchangeData = new[]
            {
                new { Title = "拥有死神·岳山摔绳包全部外观", CurrentCount = 0, TotalCount = 3, HighlightText = "全部", ImageIndex = 0 },
                new { Title = "拥有源氏·武田信忠摔绳包全部外观", CurrentCount = 0, TotalCount = 3, HighlightText = "全部", ImageIndex = 1 },
                new { Title = "拥有守望系列全部摔绳包外观", CurrentCount = 0, TotalCount = 16, HighlightText = "全部", ImageIndex = 2 },
                new { Title = "拥有D.Va·沈妙摔绳包全部外观", CurrentCount = 0, TotalCount = 3, HighlightText = "全部", ImageIndex = 3 },
                new { Title = "拥有天使·席拉摔绳包全部外观", CurrentCount = 0, TotalCount = 4, HighlightText = "全部", ImageIndex = 4 },
                new { Title = "拥有黑百合·宁红夜摔绳包全部外观", CurrentCount = 0, TotalCount = 3, HighlightText = "全部", ImageIndex = 5 },
                new { Title = "拥有守望系列任意1款直售时装", CurrentCount = 0, TotalCount = 1, HighlightText = "1款", ImageIndex = 6 },
                new { Title = "拥有守望系列任意2款直售时装", CurrentCount = 0, TotalCount = 2, HighlightText = "2款", ImageIndex = 6 },
                new { Title = "拥有守望系列任意4款直售时装", CurrentCount = 0, TotalCount = 4, HighlightText = "4款", ImageIndex = 6 },
                new { Title = "拥有守望系列任意7款直售外观", CurrentCount = 0, TotalCount = 7, HighlightText = "7款", ImageIndex = 6 },
                new { Title = "拥有守望系列摔绳包内任意13款直售外观", CurrentCount = 0, TotalCount = 13, HighlightText = "13款", ImageIndex = 6 }
            };

            foreach (var data in exchangeData)
            {
                var exchangeItem = new ExchangeItemViewModel
                {
                    Title = data.Title,
                    CurrentCount = data.CurrentCount,
                    TotalCount = data.TotalCount,
                    HighlightText = data.HighlightText,
                    ItemImage = data.ImageIndex >= 0 && data.ImageIndex < images3.Count 
                        ? images3[data.ImageIndex] 
                        : null
                };

                ExchangeItems.Add(exchangeItem);
            }
        }

        /// <summary>
        /// 选中事件项
        /// </summary>
        public DelegateCommand<string> SelectEventCommand =>
            new DelegateCommand<string>(eventName =>
            {
                if (!string.IsNullOrEmpty(eventName))
                {
                    SelectEvent(eventName);
                }
            });

        /// <summary>
        /// 处理事件选中逻辑
        /// </summary>
        private void SelectEvent(string eventName)
        {
            // 根据选中的事件名称设置相应的信息
            // 这里可以根据 eventName 加载不同的事件数据
        }
    }

    /// <summary>
    /// 时间限制事件奖励项视图模型
    /// </summary>
    internal class RewardItemViewModel : ViewModelBase
    {
        private ImageSource _imageSource;
        private double _imageWidth;
        private string _buttonText;
        private DelegateCommand _rewardCommand;

        /// <summary>
        /// 奖励图片源
        /// </summary>
        public ImageSource ImageSource
        {
            get => _imageSource;
            set 
            { 
                _imageSource = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 奖励图片宽度
        /// </summary>
        public double ImageWidth
        {
            get => _imageWidth;
            set 
            { 
                _imageWidth = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 按钮文本
        /// </summary>
        public string ButtonText
        {
            get => _buttonText;
            set 
            { 
                _buttonText = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 领取奖励命令
        /// </summary>
        public DelegateCommand RewardCommand
        {
            get => _rewardCommand;
            set 
            { 
                _rewardCommand = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    /// 活动任务项视图模型
    /// </summary>
    internal class TaskItemViewModel : ViewModelBase
    {
        private int _index;
        private bool _isLocked;

        /// <summary>
        /// 任务索引（显示用）
        /// </summary>
        public int Index
        {
            get => _index;
            set 
            { 
                _index = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 是否锁定（未完成）
        /// </summary>
        public bool IsLocked
        {
            get => _isLocked;
            set 
            { 
                _isLocked = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    /// 兑换物品视图模型
    /// </summary>
    internal class ExchangeItemViewModel : ViewModelBase
    {
        private string _title;
        private int _currentCount;
        private int _totalCount;
        private ImageSource _itemImage;
        private string _highlightText; // 用于高亮的关键字

        /// <summary>
        /// 兑换物品标题
        /// </summary>
        public string Title
        {
            get => _title;
            set 
            { 
                _title = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 当前计数
        /// </summary>
        public int CurrentCount
        {
            get => _currentCount;
            set 
            { 
                _currentCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 总计数
        /// </summary>
        public int TotalCount
        {
            get => _totalCount;
            set 
            { 
                _totalCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 兑换物品图片
        /// </summary>
        public ImageSource ItemImage
        {
            get => _itemImage;
            set 
            { 
                _itemImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 高亮文本（用于高亮标题中的关键字）
        /// </summary>
        public string HighlightText
        {
            get => _highlightText;
            set 
            { 
                _highlightText = value;
                RaisePropertyChanged();
            }
        }
    }
}
