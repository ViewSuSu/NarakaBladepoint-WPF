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
}
