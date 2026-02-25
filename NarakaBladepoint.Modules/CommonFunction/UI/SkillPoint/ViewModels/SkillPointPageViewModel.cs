using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Utilities;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;
using Prism.Commands;
using System.Threading;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.ViewModels
{
    internal class SkillPointItemViewModel : BindableBase
    {
        private int _currentLearned;
        private int _totalLearnable;
        private bool _isLearnable;

        public string SkillName { get; set; }
        public int CircleLevel { get; set; }  // 1, 2, or 3
        public string Position { get; set; }  // LeftUp, LeftDown, RightUp, RightDown
        public string Direction { get; set; } // TenOClock or ElevenOClock
        public int Index { get; set; }

        public int CurrentLearned
        {
            get { return _currentLearned; }
            set
            {
                if (_currentLearned != value)
                {
                    _currentLearned = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int TotalLearnable
        {
            get { return _totalLearnable; }
            set
            {
                if (_totalLearnable != value)
                {
                    _totalLearnable = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsLearnable
        {
            get { return _isLearnable; }
            set
            {
                if (_isLearnable != value)
                {
                    _isLearnable = value;
                    RaisePropertyChanged();
                }
            }
        }

        public SkillPointItemViewModel()
        {
            CurrentLearned = 0;
            IsLearnable = true;
        }
    }

    internal class SkillPointPageViewModel : CommonFunctionPageViewModelBase
    {
        private readonly ITianfuSkillPointProvider _tianfuSkillPointProvider;
        private int _remainingPoints;
        private bool _isSkillPointsEnabled;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftDown;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightDown;
        private string _currentSelectedSkillType;
        private string _currentSelectedTianfu;
        private Uri _f1VideoSource;
        private Uri _f2VideoSource;
        private Uri _v1VideoSource;
        private Uri _v2VideoSource;
        private CancellationTokenSource _saveCancellationTokenSource;
        private bool _isSaving;

        // 缓存三个天赋的数据，避免重复加载
        private readonly Dictionary<string, (List<SkillPointItemViewModel> leftUp, List<SkillPointItemViewModel> leftDown, List<SkillPointItemViewModel> rightUp, List<SkillPointItemViewModel> rightDown, int remainingPoints)> _tianfuDataCache;

        // 为每个天赋独立维护 RemainingPoints，避免互相影响
        private readonly Dictionary<string, int> _tianfuRemainingPointsMap = new();

        public string CurrentSelectedSkillType
        {
            get { return _currentSelectedSkillType; }
            set
            {
                if (_currentSelectedSkillType != value)
                {
                    _currentSelectedSkillType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string CurrentSelectedTianfu
        {
            get { return _currentSelectedTianfu; }
            set
            {
                if (_currentSelectedTianfu != value)
                {
                    _currentSelectedTianfu = value;
                    RaisePropertyChanged();
                    // 同步切换，直接使用缓存数据
                    SwitchTianfuData(value);
                }
            }
        }

        public int RemainingPoints
        {
            get { return _remainingPoints; }
            set
            {
                if (_remainingPoints != value)
                {
                    _remainingPoints = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsSkillPointsEnabled
        {
            get { return _isSkillPointsEnabled; }
            set
            {
                if (_isSkillPointsEnabled != value)
                {
                    _isSkillPointsEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsLeftUp
        {
            get { return _skillPointsLeftUp; }
            set
            {
                if (_skillPointsLeftUp != value)
                {
                    _skillPointsLeftUp = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsLeftDown
        {
            get { return _skillPointsLeftDown; }
            set
            {
                if (_skillPointsLeftDown != value)
                {
                    _skillPointsLeftDown = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsRightUp
        {
            get { return _skillPointsRightUp; }
            set
            {
                if (_skillPointsRightUp != value)
                {
                    _skillPointsRightUp = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsRightDown
        {
            get { return _skillPointsRightDown; }
            set
            {
                if (_skillPointsRightDown != value)
                {
                    _skillPointsRightDown = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Uri F1VideoSource
        {
            get { return _f1VideoSource; }
            set
            {
                if (_f1VideoSource != value)
                {
                    _f1VideoSource = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Uri F2VideoSource
        {
            get { return _f2VideoSource; }
            set
            {
                if (_f2VideoSource != value)
                {
                    _f2VideoSource = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Uri V1VideoSource
        {
            get { return _v1VideoSource; }
            set
            {
                if (_v1VideoSource != value)
                {
                    _v1VideoSource = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Uri V2VideoSource
        {
            get { return _v2VideoSource; }
            set
            {
                if (_v2VideoSource != value)
                {
                    _v2VideoSource = value;
                    RaisePropertyChanged();
                }
            }
        }

        private const int TOTAL_SKILL_POINTS = 20;
        private const int FIRST_CIRCLE_MAX = 4;
        private const int SECOND_CIRCLE_MAX = 4;
        private const int THIRD_CIRCLE_MAX = 2;

        public DelegateCommand<SkillPointItemViewModel> LearnSkillCommand { get; set; }
        public DelegateCommand<SkillPointItemViewModel> UnlearnSkillCommand { get; set; }
        public DelegateCommand AutoAssignCommand { get; set; }
        public DelegateCommand ResetAllCommand { get; set; }

        public SkillPointPageViewModel(ITianfuSkillPointProvider tianfuSkillPointProvider = null)
        {
            _tianfuSkillPointProvider = tianfuSkillPointProvider;
            _saveCancellationTokenSource = new CancellationTokenSource();
            _isSaving = false;
            _tianfuDataCache = new Dictionary<string, (List<SkillPointItemViewModel>, List<SkillPointItemViewModel>, List<SkillPointItemViewModel>, List<SkillPointItemViewModel>, int)>();

            RemainingPoints = TOTAL_SKILL_POINTS;
            IsSkillPointsEnabled = true;

            SkillPointsLeftUp = new ObservableCollection<SkillPointItemViewModel>();
            SkillPointsLeftDown = new ObservableCollection<SkillPointItemViewModel>();
            SkillPointsRightUp = new ObservableCollection<SkillPointItemViewModel>();
            SkillPointsRightDown = new ObservableCollection<SkillPointItemViewModel>();

            // Initialize video sources using reflection to get assembly path
            try
            {
                F1VideoSource = VideoPathResolver.GetVideoUri("Image/SkillPoints/Gif/F1.Mp4");
                F2VideoSource = VideoPathResolver.GetVideoUri("Image/SkillPoints/Gif/F2.Mp4");
                V1VideoSource = VideoPathResolver.GetVideoUri("Image/SkillPoints/Gif/V1.Mp4");
                V2VideoSource = VideoPathResolver.GetVideoUri("Image/SkillPoints/Gif/V2.Mp4");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load video sources: {ex.Message}");
            }

            LearnSkillCommand = new DelegateCommand<SkillPointItemViewModel>(LearnSkill, CanLearnSkill);
            UnlearnSkillCommand = new DelegateCommand<SkillPointItemViewModel>(UnlearnSkill, CanUnlearnSkill);
            AutoAssignCommand = new DelegateCommand(AutoAssignSkills);
            ResetAllCommand = new DelegateCommand(ResetAllSkills);

            // 异步预加载所有天赋数据
            _ = PreloadAllTianfuDataAsync();
        }

        /// <summary>
        /// 预加载所有三个天赋的数据到缓存中
        /// </summary>
        private async Task PreloadAllTianfuDataAsync()
        {
            if (_tianfuSkillPointProvider == null)
            {
                // 如果没有provider，初始化默认数据
                InitializeDefaultTianfuData();
                CurrentSelectedTianfu = "tianfu1";
                return;
            }

            try
            {
                // 并行加载三个天赋的数据
                var tasks = new[]
                {
                    LoadAndCacheTianfuDataAsync("tianfu1"),
                    LoadAndCacheTianfuDataAsync("tianfu2"),
                    LoadAndCacheTianfuDataAsync("tianfu3")
                };

                await Task.WhenAll(tasks);

                // 加载完成后，设置默认选择
                CurrentSelectedTianfu = "tianfu1";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to preload tianfu data: {ex.Message}");
                InitializeDefaultTianfuData();
                CurrentSelectedTianfu = "tianfu1";
            }
        }

        /// <summary>
        /// 加载并缓存单个天赋的数据
        /// </summary>
        private async Task LoadAndCacheTianfuDataAsync(string tianfuName)
        {
            try
            {
                var tianfuData = await _tianfuSkillPointProvider.GetTianfuSkillPointAsync(tianfuName);

                // 在后台线程中构建数据
                var result = await Task.Run(() =>
                {
                    var leftUp = new List<SkillPointItemViewModel>();
                    var leftDown = new List<SkillPointItemViewModel>();
                    var rightUp = new List<SkillPointItemViewModel>();
                    var rightDown = new List<SkillPointItemViewModel>();

                    foreach (var skillPointData in tianfuData.SkillPoints)
                    {
                        var skillPoint = new SkillPointItemViewModel
                        {
                            Index = skillPointData.Index,
                            SkillName = skillPointData.SkillName,
                            CircleLevel = skillPointData.CircleLevel,
                            Position = skillPointData.Position,
                            Direction = skillPointData.Direction,
                            TotalLearnable = skillPointData.TotalLearnable,
                            CurrentLearned = skillPointData.CurrentLearned,
                            IsLearnable = skillPointData.IsLearnable
                        };

                        switch (skillPointData.Position)
                        {
                            case "LeftUp":
                                leftUp.Add(skillPoint);
                                break;
                            case "LeftDown":
                                leftDown.Add(skillPoint);
                                break;
                            case "RightUp":
                                rightUp.Add(skillPoint);
                                break;
                            case "RightDown":
                                rightDown.Add(skillPoint);
                                break;
                        }
                    }

                    return (leftUp, leftDown, rightUp, rightDown, tianfuData.RemainingPoints);
                });

                _tianfuDataCache[tianfuName] = result;
                // 为该天赋独立维护 RemainingPoints
                _tianfuRemainingPointsMap[tianfuName] = result.Item5;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load tianfu {tianfuName} data: {ex.Message}");
            }
        }

        /// <summary>
        /// 初始化默认天赋数据
        /// </summary>
        private void InitializeDefaultTianfuData()
        {
            var (leftUp, leftDown, rightUp, rightDown) = GenerateDefaultSkillPoints();

            _tianfuDataCache["tianfu1"] = (leftUp, leftDown, rightUp, rightDown, TOTAL_SKILL_POINTS);
            _tianfuRemainingPointsMap["tianfu1"] = TOTAL_SKILL_POINTS;

            _tianfuDataCache["tianfu2"] = (
                leftUp.Select(CloneSkillPoint).ToList(),
                leftDown.Select(CloneSkillPoint).ToList(),
                rightUp.Select(CloneSkillPoint).ToList(),
                rightDown.Select(CloneSkillPoint).ToList(),
                TOTAL_SKILL_POINTS
            );
            _tianfuRemainingPointsMap["tianfu2"] = TOTAL_SKILL_POINTS;

            _tianfuDataCache["tianfu3"] = (
                leftUp.Select(CloneSkillPoint).ToList(),
                leftDown.Select(CloneSkillPoint).ToList(),
                rightUp.Select(CloneSkillPoint).ToList(),
                rightDown.Select(CloneSkillPoint).ToList(),
                TOTAL_SKILL_POINTS
            );
            _tianfuRemainingPointsMap["tianfu3"] = TOTAL_SKILL_POINTS;
        }

        /// <summary>
        /// 生成默认的技能点数据
        /// </summary>
        private (List<SkillPointItemViewModel>, List<SkillPointItemViewModel>, List<SkillPointItemViewModel>, List<SkillPointItemViewModel>) GenerateDefaultSkillPoints()
        {
            var leftUp = new List<SkillPointItemViewModel>();
            var leftDown = new List<SkillPointItemViewModel>();
            var rightUp = new List<SkillPointItemViewModel>();
            var rightDown = new List<SkillPointItemViewModel>();

            var skillPointConfigs = new[]
            {
                // LeftUp corner
                new { Name = "Lu_TenOClock", Circle = 1, Position = "LeftUp", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock", Circle = 1, Position = "LeftUp", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock", Circle = 2, Position = "LeftUp", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock", Circle = 2, Position = "LeftUp", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock", Circle = 3, Position = "LeftUp", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock", Circle = 3, Position = "LeftUp", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                new { Name = "Lu_TenOClock_MirrorH", Circle = 1, Position = "RightUp", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorH", Circle = 1, Position = "RightUp", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorH", Circle = 2, Position = "RightUp", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorH", Circle = 2, Position = "RightUp", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorH", Circle = 3, Position = "RightUp", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorH", Circle = 3, Position = "RightUp", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                new { Name = "Lu_TenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                new { Name = "Lu_TenOClock_MirrorHV", Circle = 1, Position = "RightDown", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorHV", Circle = 1, Position = "RightDown", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorHV", Circle = 2, Position = "RightDown", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorHV", Circle = 2, Position = "RightDown", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorHV", Circle = 3, Position = "RightDown", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorHV", Circle = 3, Position = "RightDown", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },
            };

            int index = 0;
            foreach (var config in skillPointConfigs)
            {
                var skillPoint = new SkillPointItemViewModel
                {
                    Index = index,
                    SkillName = config.Name,
                    CircleLevel = config.Circle,
                    Position = config.Position,
                    Direction = config.Direction,
                    TotalLearnable = config.Max
                };

                switch (config.Position)
                {
                    case "LeftUp":
                        leftUp.Add(skillPoint);
                        break;
                    case "LeftDown":
                        leftDown.Add(skillPoint);
                        break;
                    case "RightUp":
                        rightUp.Add(skillPoint);
                        break;
                    case "RightDown":
                        rightDown.Add(skillPoint);
                        break;
                }

                index++;
            }

            return (leftUp, leftDown, rightUp, rightDown);
        }

        /// <summary>
        /// 克隆技能点对象
        /// </summary>
        private SkillPointItemViewModel CloneSkillPoint(SkillPointItemViewModel original)
        {
            return new SkillPointItemViewModel
            {
                Index = original.Index,
                SkillName = original.SkillName,
                CircleLevel = original.CircleLevel,
                Position = original.Position,
                Direction = original.Direction,
                TotalLearnable = original.TotalLearnable,
                CurrentLearned = original.CurrentLearned,
                IsLearnable = original.IsLearnable
            };
        }

        /// <summary>
        /// 快速切换天赋数据（同步操作，直接使用缓存）
        /// </summary>
        private void SwitchTianfuData(string tianfuName)
        {
            if (!_tianfuDataCache.TryGetValue(tianfuName, out var data))
            {
                return;
            }

            // 直接赋值集合，一次完成，无需遍历逐项添加
            SkillPointsLeftUp = new ObservableCollection<SkillPointItemViewModel>(data.leftUp);
            SkillPointsLeftDown = new ObservableCollection<SkillPointItemViewModel>(data.leftDown);
            SkillPointsRightUp = new ObservableCollection<SkillPointItemViewModel>(data.rightUp);
            SkillPointsRightDown = new ObservableCollection<SkillPointItemViewModel>(data.rightDown);

            // 从独立维护的字典中读取该天赋的 RemainingPoints（避免天赋间互相影响）
            if (_tianfuRemainingPointsMap.TryGetValue(tianfuName, out var remainingPoints))
            {
                RemainingPoints = remainingPoints;
            }
            else
            {
                RemainingPoints = TOTAL_SKILL_POINTS;
            }

            // 重新初始化IsLearnable状态
            InitializeLearnableStatus();
        }

        /// <summary>
        /// 从缓存中加载天赋的剩余点数
        /// </summary>
        private void LoadTianfuRemainingPoints(string tianfuName)
        {
            if (_tianfuSkillPointProvider == null)
            {
                RemainingPoints = TOTAL_SKILL_POINTS;
                return;
            }

            // 异步加载剩余点数，不阻塞UI
            _ = Task.Run(async () =>
            {
                try
                {
                    var tianfuData = await _tianfuSkillPointProvider.GetTianfuSkillPointAsync(tianfuName);
                    RemainingPoints = tianfuData.RemainingPoints;
                }
                catch
                {
                    RemainingPoints = TOTAL_SKILL_POINTS;
                }
            });
        }

        /// <summary>
        /// 当选择天赋时调用（已过时，保留向后兼容）
        /// </summary>
        private async Task OnTianfuSelectedAsync(string tianfuName)
        {
            if (string.IsNullOrEmpty(tianfuName) || _tianfuSkillPointProvider == null)
            {
                InitializeSkillPoints();
                return;
            }

            try
            {
                var tianfuData = await _tianfuSkillPointProvider.GetTianfuSkillPointAsync(tianfuName);
                await LoadTianfuSkillPointsAsync(tianfuData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load tianfu data: {ex.Message}");
                InitializeSkillPoints();
            }
        }

        /// <summary>
        /// 根据天赋数据异步加载技能点（已过时，保留向后兼容）
        /// </summary>
        private async Task LoadTianfuSkillPointsAsync(TianfuSkillPointData tianfuData)
        {
            var result = await Task.Run(() =>
            {
                var leftUp = new List<SkillPointItemViewModel>();
                var leftDown = new List<SkillPointItemViewModel>();
                var rightUp = new List<SkillPointItemViewModel>();
                var rightDown = new List<SkillPointItemViewModel>();

                foreach (var skillPointData in tianfuData.SkillPoints)
                {
                    var skillPoint = new SkillPointItemViewModel
                    {
                        Index = skillPointData.Index,
                        SkillName = skillPointData.SkillName,
                        CircleLevel = skillPointData.CircleLevel,
                        Position = skillPointData.Position,
                        Direction = skillPointData.Direction,
                        TotalLearnable = skillPointData.TotalLearnable,
                        CurrentLearned = skillPointData.CurrentLearned,
                        IsLearnable = skillPointData.IsLearnable
                    };

                    switch (skillPointData.Position)
                    {
                        case "LeftUp":
                            leftUp.Add(skillPoint);
                            break;
                        case "LeftDown":
                            leftDown.Add(skillPoint);
                            break;
                        case "RightUp":
                            rightUp.Add(skillPoint);
                            break;
                        case "RightDown":
                            rightDown.Add(skillPoint);
                            break;
                    }
                }

                return (leftUp, leftDown, rightUp, rightDown);
            });

            // 优化：直接赋值集合，避免Clear + foreach逐项添加
            SkillPointsLeftUp = new ObservableCollection<SkillPointItemViewModel>(result.leftUp);
            SkillPointsLeftDown = new ObservableCollection<SkillPointItemViewModel>(result.leftDown);
            SkillPointsRightUp = new ObservableCollection<SkillPointItemViewModel>(result.rightUp);
            SkillPointsRightDown = new ObservableCollection<SkillPointItemViewModel>(result.rightDown);

            RemainingPoints = tianfuData.RemainingPoints;

            await Task.Run(() => InitializeLearnableStatus());
        }

        private void InitializeSkillPoints()
        {
            // 清空所有集合
            SkillPointsLeftUp.Clear();
            SkillPointsLeftDown.Clear();
            SkillPointsRightUp.Clear();
            SkillPointsRightDown.Clear();

            // Initialize all 24 skill points (4 corners × 6 points each)
            var skillPointConfigs = new[]
            {
                // LeftUp corner
                new { Name = "Lu_TenOClock", Circle = 1, Position = "LeftUp", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock", Circle = 1, Position = "LeftUp", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock", Circle = 2, Position = "LeftUp", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock", Circle = 2, Position = "LeftUp", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock", Circle = 3, Position = "LeftUp", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock", Circle = 3, Position = "LeftUp", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                // RightUp corner (mirror of LeftUp)
                new { Name = "Lu_TenOClock_MirrorH", Circle = 1, Position = "RightUp", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorH", Circle = 1, Position = "RightUp", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorH", Circle = 2, Position = "RightUp", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorH", Circle = 2, Position = "RightUp", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorH", Circle = 3, Position = "RightUp", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorH", Circle = 3, Position = "RightUp", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                // LeftDown corner
                new { Name = "Lu_TenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },

                // RightDown corner
                new { Name = "Lu_TenOClock_MirrorHV", Circle = 1, Position = "RightDown", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Ma_ElevenOClock_MirrorHV", Circle = 1, Position = "RightDown", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "He_TenOClock_MirrorHV", Circle = 2, Position = "RightDown", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorHV", Circle = 2, Position = "RightDown", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Xiong_TenOClock_MirrorHV", Circle = 3, Position = "RightDown", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Shi_ElevenOClock_MirrorHV", Circle = 3, Position = "RightDown", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },
            };

            int index = 0;
            foreach (var config in skillPointConfigs)
            {
                var skillPoint = new SkillPointItemViewModel
                {
                    Index = index,
                    SkillName = config.Name,
                    CircleLevel = config.Circle,
                    Position = config.Position,
                    Direction = config.Direction,
                    TotalLearnable = config.Max
                };

                // Add to position-specific collection
                switch (config.Position)
                {
                    case "LeftUp":
                        SkillPointsLeftUp.Add(skillPoint);
                        break;
                    case "LeftDown":
                        SkillPointsLeftDown.Add(skillPoint);
                        break;
                    case "RightUp":
                        SkillPointsRightUp.Add(skillPoint);
                        break;
                    case "RightDown":
                        SkillPointsRightDown.Add(skillPoint);
                        break;
                }

                index++;
            }

            // 初始化所有技能点的IsLearnable状态
            InitializeLearnableStatus();
        }

        private void InitializeLearnableStatus()
        {
            // 优化：直接遍历初始化，避免构建未使用的查询字典
            UpdateSiblingLearnableStatus(SkillPointsLeftUp);
            UpdateSiblingLearnableStatus(SkillPointsLeftDown);
            UpdateSiblingLearnableStatus(SkillPointsRightUp);
            UpdateSiblingLearnableStatus(SkillPointsRightDown);
        }

        /// <summary>
        /// 批量更新集合中所有技能点的IsLearnable状态
        /// </summary>
        private void UpdateSiblingLearnableStatus(ObservableCollection<SkillPointItemViewModel> positionCollection)
        {
            foreach (var skillPoint in positionCollection)
            {
                UpdateSiblingLearnableStatus(skillPoint);
            }
        }

        private void LearnSkill(SkillPointItemViewModel skillPoint)
        {
            if (!CanLearnSkill(skillPoint))
                return;

            // Check if previous circle is fully learned in this position
            if (!IsCircleReadyToLearn(skillPoint))
                return;

            // Check if the shared limit for this pair is not exceeded
            if (!CanLearnWithinSharedLimit(skillPoint))
                return;

            skillPoint.CurrentLearned++;
            RemainingPoints--;

            // 同步更新当前天赋的 RemainingPoints 映射
            if (_tianfuRemainingPointsMap.ContainsKey(CurrentSelectedTianfu))
            {
                _tianfuRemainingPointsMap[CurrentSelectedTianfu] = RemainingPoints;
            }

            // 批量更新状态，而不是实时更新
            UpdateSiblingLearnableStatus(skillPoint);

            // 更新子圈技能点的IsLearnable状态（如果当前是第1或第2圈）
            if (skillPoint.CircleLevel < 3)
            {
                UpdateChildCircleLearnableStatus(skillPoint);
            }

            UpdateSkillPointsEnabled();
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();

            // 使用防抖机制保存数据，避免频繁I/O操作
            DebouncedSaveCurrentTianfuDataAsync();
        }

        private void UnlearnSkill(SkillPointItemViewModel skillPoint)
        {
            if (!CanUnlearnSkill(skillPoint))
                return;

            skillPoint.CurrentLearned--;
            RemainingPoints++;

            // 同步更新当前天赋的 RemainingPoints 映射
            if (_tianfuRemainingPointsMap.ContainsKey(CurrentSelectedTianfu))
            {
                _tianfuRemainingPointsMap[CurrentSelectedTianfu] = RemainingPoints;
            }

            // 批量更新状态，而不是实时更新
            UpdateSiblingLearnableStatus(skillPoint);

            // 更新子圈技能点的IsLearnable状态（如果当前是第1或第2圈）
            if (skillPoint.CircleLevel < 3)
            {
                UpdateChildCircleLearnableStatus(skillPoint);
            }

            UpdateSkillPointsEnabled();
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();

            // 使用防抖机制保存数据，避免频繁I/O操作
            DebouncedSaveCurrentTianfuDataAsync();
        }

        private bool CanLearnSkill(SkillPointItemViewModel skillPoint)
        {
            // Basic checks
            if (skillPoint == null || RemainingPoints <= 0 || 
                skillPoint.CurrentLearned >= skillPoint.TotalLearnable || 
                !IsSkillPointsEnabled)
                return false;

            // Check if sibling skills have reached the shared limit
            if (!CanLearnWithinSiblingLimit(skillPoint))
                return false;

            // Check parent circle prerequisite
            return IsCircleReadyToLearn(skillPoint);
        }

        private bool CanUnlearnSkill(SkillPointItemViewModel skillPoint)
        {
            if (skillPoint == null || skillPoint.CurrentLearned <= 0)
                return false;

            // 检查子圈是否全部未学
            // 要撤销当前技能点，它的所有子圈技能点都必须是 0（未学）
            if (!AreChildSkillsFullyUnlearned(skillPoint))
                return false;

            return true;
        }

        private bool IsCircleReadyToLearn(SkillPointItemViewModel skillPoint)
        {
            if (skillPoint.CircleLevel == 1)
                return true;

            int previousCircleLevel = skillPoint.CircleLevel - 1;
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // 优化：只计算总和，不需要ToList()
            int totalLearnedInPreviousCircle = 0;
            int previousCircleLimit = GetCircleMaxPoints(previousCircleLevel);

            foreach (var s in positionCollection)
            {
                if (s.CircleLevel == previousCircleLevel)
                    totalLearnedInPreviousCircle += s.CurrentLearned;
            }

            return totalLearnedInPreviousCircle >= previousCircleLimit;
        }

        private bool CanLearnWithinSharedLimit(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = GetCircleMaxPoints(skillPoint.CircleLevel);

            // 优化：缓存计算，避免重复
            int totalLearnedInPair = 0;
            int pairCount = 0;

            foreach (var s in positionCollection)
            {
                if (s.CircleLevel == skillPoint.CircleLevel && 
                   (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                {
                    totalLearnedInPair += s.CurrentLearned;
                    pairCount++;
                }
            }

            if (pairCount != 2)
                return true;

            return totalLearnedInPair < sharedLimit;
        }

        private bool CanLearnWithinSiblingLimit(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = GetCircleMaxPoints(skillPoint.CircleLevel);

            // 优化：单次遍历计算总和
            int totalLearnedInPair = 0;
            int pairCount = 0;

            foreach (var s in positionCollection)
            {
                if (s.CircleLevel == skillPoint.CircleLevel && 
                   (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                {
                    totalLearnedInPair += s.CurrentLearned;
                    pairCount++;
                }
            }

            if (pairCount != 2)
                return true;

            if (skillPoint.CurrentLearned == 0 && totalLearnedInPair >= sharedLimit)
                return false;

            return true;
        }

        private bool AreChildSkillsFullyUnlearned(SkillPointItemViewModel skillPoint)
        {
            // 调用真实的实现（检查子圈是否都未学）
            return AreParentSkillsFullyUnlearned(skillPoint);
        }

        private bool AreParentSkillsFullyUnlearned(SkillPointItemViewModel skillPoint)
        {
            if (skillPoint.CircleLevel == 3)
                return true;

            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int childCircleLevel = skillPoint.CircleLevel + 1;

            // 优化：直接遍历，找到第一个已学的就返回false
            foreach (var s in positionCollection)
            {
                if (s.CircleLevel == childCircleLevel && s.CurrentLearned > 0)
                    return false;
            }

            return true;
        }

        private ObservableCollection<SkillPointItemViewModel> GetSkillPointCollectionByPosition(string position)
        {
            return position switch
            {
                "LeftUp" => SkillPointsLeftUp,
                "LeftDown" => SkillPointsLeftDown,
                "RightUp" => SkillPointsRightUp,
                "RightDown" => SkillPointsRightDown,
                _ => SkillPointsLeftUp  // 默认返回LeftUp，而不是创建新集合
            };
        }

        private void UpdateSkillPointsEnabled()
        {
            IsSkillPointsEnabled = RemainingPoints > 0;
        }

        /// <summary>
        /// 获取指定圈数允许的最大技能点数
        /// </summary>
        private int GetCircleMaxPoints(int circleLevel)
        {
            return circleLevel == 3 ? THIRD_CIRCLE_MAX : 
                   circleLevel == 2 ? SECOND_CIRCLE_MAX : 
                   FIRST_CIRCLE_MAX;
        }

        private void UpdateSiblingLearnableStatus(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = GetCircleMaxPoints(skillPoint.CircleLevel);

            // 优化：单次遍历获取同级技能和总和
            var siblingSkills = new List<SkillPointItemViewModel>();
            int totalLearnedInPair = 0;

            foreach (var s in positionCollection)
            {
                if (s.CircleLevel == skillPoint.CircleLevel && 
                   (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                {
                    siblingSkills.Add(s);
                    totalLearnedInPair += s.CurrentLearned;
                }
            }

            if (siblingSkills.Count != 2)
                return;

            // 检查父圈条件一次
            bool isParentCircleReady = IsCircleReadyToLearn(skillPoint);
            bool isPairFullyLearned = totalLearnedInPair >= sharedLimit;

            // 一次更新所有同级技能点
            foreach (var sibling in siblingSkills)
            {
                sibling.IsLearnable = !isPairFullyLearned && isParentCircleReady;
            }
        }

        private void UpdateChildCircleLearnableStatus(SkillPointItemViewModel skillPoint)
        {
            if (skillPoint.CircleLevel >= 3)
                return;

            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int childCircleLevel = skillPoint.CircleLevel + 1;

            // 优化：直接遍历更新，避免LINQ
            foreach (var childSkill in positionCollection)
            {
                if (childSkill.CircleLevel == childCircleLevel)
                {
                    UpdateSiblingLearnableStatus(childSkill);
                }
            }
        }

        /// <summary>
        /// 自动装配技能点 - 异步版本
        /// SkillPointsLeftUp[0] -> 4点
        /// SkillPointsLeftUp[2] -> 4点
        /// SkillPointsLeftUp[5] -> 2点
        /// SkillPointsRightUp[1] -> 4点
        /// SkillPointsRightUp[3] -> 4点
        /// SkillPointsRightUp[5] -> 2点
        /// </summary>
        private async void AutoAssignSkills()
        {
            IsSkillPointsEnabled = false;

            try
            {
                await AutoAssignSkillsAsync();
            }
            finally
            {
                UpdateSkillPointsEnabled();
            }
        }

        private async Task AutoAssignSkillsAsync()
        {
            // 在后台线程执行耗时操作
            await Task.Run(() =>
            {
                // 先重置所有技能
                ResetAllSkillsSync();

                // 分配LeftUp的技能
                AutoLearnSkillSync(SkillPointsLeftUp[0], 4);  // Index 0: 4 points
                AutoLearnSkillSync(SkillPointsLeftUp[2], 4);  // Index 2: 4 points
                AutoLearnSkillSync(SkillPointsLeftUp[5], 2);  // Index 5: 2 points

                // 分配RightUp的技能
                AutoLearnSkillSync(SkillPointsRightUp[1], 4); // Index 1: 4 points
                AutoLearnSkillSync(SkillPointsRightUp[3], 4); // Index 3: 4 points
                AutoLearnSkillSync(SkillPointsRightUp[5], 2); // Index 5: 2 points
            });

            // UI线程上更新状态
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();

            // 异步保存
            await SaveCurrentTianfuDataAsync();
        }

        /// <summary>
        /// 重置所有技能点 - 异步版本
        /// </summary>
        private async void ResetAllSkills()
        {
            IsSkillPointsEnabled = false;

            try
            {
                await ResetAllSkillsAsync();
            }
            finally
            {
                UpdateSkillPointsEnabled();
            }
        }

        private async Task ResetAllSkillsAsync()
        {
            // 在后台线程执行
            await Task.Run(() => ResetAllSkillsSync());

            // UI线程上更新状态
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();

            // 异步保存
            await SaveCurrentTianfuDataAsync();
        }

        /// <summary>
        /// 同步重置所有技能点（在后台线程中调用）
        /// </summary>
        private void ResetAllSkillsSync()
        {
            // 优化：直接遍历四个集合，避免SelectMany和ToList
            foreach (var skillPoint in SkillPointsLeftUp)
                skillPoint.CurrentLearned = 0;

            foreach (var skillPoint in SkillPointsLeftDown)
                skillPoint.CurrentLearned = 0;

            foreach (var skillPoint in SkillPointsRightUp)
                skillPoint.CurrentLearned = 0;

            foreach (var skillPoint in SkillPointsRightDown)
                skillPoint.CurrentLearned = 0;

            RemainingPoints = TOTAL_SKILL_POINTS;

            // 同步更新当前天赋的 RemainingPoints 映射
            if (_tianfuRemainingPointsMap.ContainsKey(CurrentSelectedTianfu))
            {
                _tianfuRemainingPointsMap[CurrentSelectedTianfu] = RemainingPoints;
            }

            // 重新初始化所有技能点的IsLearnable状态
            InitializeLearnableStatus();
        }

        /// <summary>
        /// 为指定的技能点同步学习指定数量的点数（在后台线程中调用）
        /// </summary>
        private void AutoLearnSkillSync(SkillPointItemViewModel skillPoint, int pointsToLearn)
        {
            for (int i = 0; i < pointsToLearn && skillPoint.CurrentLearned < skillPoint.TotalLearnable; i++)
            {
                if (CanLearnSkill(skillPoint))
                {
                    skillPoint.CurrentLearned++;
                    RemainingPoints--;

                    // 更新状态
                    UpdateSiblingLearnableStatus(skillPoint);
                    if (skillPoint.CircleLevel < 3)
                    {
                        UpdateChildCircleLearnableStatus(skillPoint);
                    }
                }
            }
        }

        /// <summary>
        /// 防抖的保存方法 - 避免频繁I/O操作
        /// </summary>
        private void DebouncedSaveCurrentTianfuDataAsync()
        {
            // 取消前一次的保存操作
            _saveCancellationTokenSource?.Cancel();
            _saveCancellationTokenSource = new CancellationTokenSource();

            // 延迟500ms后执行保存，期间如果有新的修改会取消之前的操作
            _ = Task.Delay(500, _saveCancellationTokenSource.Token)
                .ContinueWith(async _ =>
                {
                    if (!_saveCancellationTokenSource.Token.IsCancellationRequested)
                    {
                        await SaveCurrentTianfuDataAsync();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// 保存当前天赋的数据到本地 - 异步版本
        /// </summary>
        private async Task SaveCurrentTianfuDataAsync()
        {
            if (_isSaving || _tianfuSkillPointProvider == null || string.IsNullOrEmpty(CurrentSelectedTianfu))
            {
                return;
            }

            _isSaving = true;

            try
            {
                // 在后台线程中进行数据构建
                var tianfuData = await Task.Run(() =>
                {
                    var skillPointsList = new List<SkillPointData>();

                    // 优化：直接遍历四个集合，避免SelectMany和OrderBy
                    var allCollections = new[] { SkillPointsLeftUp, SkillPointsLeftDown, SkillPointsRightUp, SkillPointsRightDown };

                    foreach (var collection in allCollections)
                    {
                        foreach (var skillPoint in collection)
                        {
                            skillPointsList.Add(new SkillPointData
                            {
                                Index = skillPoint.Index,
                                SkillName = skillPoint.SkillName,
                                CircleLevel = skillPoint.CircleLevel,
                                Position = skillPoint.Position,
                                Direction = skillPoint.Direction,
                                CurrentLearned = skillPoint.CurrentLearned,
                                TotalLearnable = skillPoint.TotalLearnable,
                                IsLearnable = skillPoint.IsLearnable
                            });
                        }
                    }

                    // 按Index排序（保证数据一致性）
                    skillPointsList.Sort((a, b) => a.Index.CompareTo(b.Index));

                    return new TianfuSkillPointData
                    {
                        TianfuName = CurrentSelectedTianfu,
                        RemainingPoints = RemainingPoints,
                        SkillPoints = skillPointsList
                    };
                });

                // 异步保存数据
                await _tianfuSkillPointProvider.SaveTianfuSkillPointAsync(tianfuData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save tianfu data: {ex.Message}");
            }
            finally
            {
                _isSaving = false;
            }
        }
    }
}
