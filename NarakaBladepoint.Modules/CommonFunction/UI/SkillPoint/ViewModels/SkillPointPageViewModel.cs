using System;
using System.Collections.ObjectModel;
using System.Linq;
using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Utilities;
using Prism.Commands;

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
        private int _remainingPoints;
        private bool _isSkillPointsEnabled;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftDown;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightDown;
        private string _currentSelectedSkillType;
        private Uri _f1VideoSource;
        private Uri _f2VideoSource;
        private Uri _v1VideoSource;
        private Uri _v2VideoSource;

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

        public DelegateCommand<SkillPointItemViewModel> LearnSkillCommand { get; set; }
        public DelegateCommand<SkillPointItemViewModel> UnlearnSkillCommand { get; set; }
        public DelegateCommand AutoAssignCommand { get; set; }
        public DelegateCommand ResetAllCommand { get; set; }

        private const int TOTAL_SKILL_POINTS = 20;
        private const int FIRST_CIRCLE_MAX = 4;
        private const int SECOND_CIRCLE_MAX = 4;
        private const int THIRD_CIRCLE_MAX = 2;

        public SkillPointPageViewModel()
        {
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

            InitializeSkillPoints();
        }

        private void InitializeSkillPoints()
        {
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
            foreach (var collection in new[] { SkillPointsLeftUp, SkillPointsLeftDown, SkillPointsRightUp, SkillPointsRightDown })
            {
                foreach (var skillPoint in collection)
                {
                    UpdateSiblingLearnableStatus(skillPoint);
                }
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

            // 更新同级技能点的IsLearnable状态
            UpdateSiblingLearnableStatus(skillPoint);

            // 更新子圈技能点的IsLearnable状态（如果当前是第1或第2圈）
            if (skillPoint.CircleLevel < 3)
            {
                UpdateChildCircleLearnableStatus(skillPoint);
            }

            UpdateSkillPointsEnabled();
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();
        }

        private void UnlearnSkill(SkillPointItemViewModel skillPoint)
        {
            if (!CanUnlearnSkill(skillPoint))
                return;

            skillPoint.CurrentLearned--;
            RemainingPoints++;

            // 更新同级技能点的IsLearnable状态
            UpdateSiblingLearnableStatus(skillPoint);

            // 更新子圈技能点的IsLearnable状态（如果当前是第1或第2圈）
            if (skillPoint.CircleLevel < 3)
            {
                UpdateChildCircleLearnableStatus(skillPoint);
            }

            UpdateSkillPointsEnabled();
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();
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
                return true; // First circle can always be learned

            int previousCircleLevel = skillPoint.CircleLevel - 1;

            // Get the appropriate collection based on position
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // Find all skills in the previous circle
            var previousCircleSkills = positionCollection.Where(s => 
                s.CircleLevel == previousCircleLevel).ToList();

            // Calculate total learned in the previous circle
            int totalLearnedInPreviousCircle = previousCircleSkills.Sum(s => s.CurrentLearned);

            // Get the shared limit for the previous circle
            int previousCircleLimit = previousCircleLevel == 3 ? THIRD_CIRCLE_MAX : 
                                     previousCircleLevel == 2 ? SECOND_CIRCLE_MAX : 
                                     FIRST_CIRCLE_MAX;

            // The previous circle must be fully learned (total must reach the shared limit)
            return totalLearnedInPreviousCircle >= previousCircleLimit;
        }

        private bool CanLearnWithinSharedLimit(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // Determine the pair index and shared limit based on circle level
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = skillPoint.CircleLevel == 3 ? THIRD_CIRCLE_MAX : skillPoint.CircleLevel == 2 ? SECOND_CIRCLE_MAX : FIRST_CIRCLE_MAX;

            // Get the two skills in this pair
            var skillsInPair = positionCollection
                .Where(s => s.CircleLevel == skillPoint.CircleLevel && 
                           (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                .ToList();

            if (skillsInPair.Count != 2)
                return true; // Safety check

            // Calculate total learned in this pair
            int totalLearnedInPair = skillsInPair.Sum(s => s.CurrentLearned);

            // Check if adding one more would exceed the shared limit
            return totalLearnedInPair < sharedLimit;
        }

        private bool CanLearnWithinSiblingLimit(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // Determine the pair index and shared limit based on circle level
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = skillPoint.CircleLevel == 3 ? THIRD_CIRCLE_MAX : skillPoint.CircleLevel == 2 ? SECOND_CIRCLE_MAX : FIRST_CIRCLE_MAX;

            // Get the two skills in this pair (sibling skills)
            var siblingSKills = positionCollection
                .Where(s => s.CircleLevel == skillPoint.CircleLevel && 
                           (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                .ToList();

            if (siblingSKills.Count != 2)
                return true; // Safety check

            // Calculate total learned in this pair
            int totalLearnedInPair = siblingSKills.Sum(s => s.CurrentLearned);

            // If current skill is not yet learned and the limit is already reached, cannot learn
            // If current skill is already learned, allow to continue learning
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
            // 第3圈没有子圈，可以撤销
            if (skillPoint.CircleLevel == 3)
                return true;

            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int childCircleLevel = skillPoint.CircleLevel + 1;

            // 获取子圈的所有技能点
            var childSkills = positionCollection
                .Where(s => s.CircleLevel == childCircleLevel)
                .ToList();

            // 子圈的所有技能点都必须是 0（完全未学）
            foreach (var childSkill in childSkills)
            {
                if (childSkill.CurrentLearned > 0)
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
                _ => new ObservableCollection<SkillPointItemViewModel>()
            };
        }

        private void UpdateSkillPointsEnabled()
        {
            IsSkillPointsEnabled = RemainingPoints > 0;
        }

        private void UpdateSiblingLearnableStatus(SkillPointItemViewModel skillPoint)
        {
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int pairIndex = (skillPoint.CircleLevel - 1) * 2;
            int sharedLimit = skillPoint.CircleLevel == 3 ? THIRD_CIRCLE_MAX : 
                             skillPoint.CircleLevel == 2 ? SECOND_CIRCLE_MAX : 
                             FIRST_CIRCLE_MAX;

            // 获取同级技能点
            var siblingSkills = positionCollection
                .Where(s => s.CircleLevel == skillPoint.CircleLevel && 
                           (s.Index % 6 == pairIndex || s.Index % 6 == pairIndex + 1))
                .ToList();

            if (siblingSkills.Count != 2)
                return;

            // 计算该对中已学的总点数
            int totalLearnedInPair = siblingSkills.Sum(s => s.CurrentLearned);

            // 检查父圈条件（对这一对的所有技能点都相同）
            bool isParentCircleReady = IsCircleReadyToLearn(skillPoint);

            // 检查该对是否已经满级
            bool isPairFullyLearned = totalLearnedInPair >= sharedLimit;

            // 更新同级技能点的IsLearnable状态
            foreach (var sibling in siblingSkills)
            {
                if (isPairFullyLearned)
                {
                    // 该对已满级，不能再学（无论已学还是未学）
                    sibling.IsLearnable = false;
                }
                else if (!isParentCircleReady)
                {
                    // 父圈条件不满足，不能学
                    sibling.IsLearnable = false;
                }
                else
                {
                    // 该对未满级且父圈条件满足，可以学
                    sibling.IsLearnable = true;
                }
            }
        }

        private void UpdateChildCircleLearnableStatus(SkillPointItemViewModel skillPoint)
        {
            // 只有第1圈和第2圈有子圈
            if (skillPoint.CircleLevel >= 3)
                return;

            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);
            int childCircleLevel = skillPoint.CircleLevel + 1;

            // 获取子圈的所有技能点
            var childSkills = positionCollection
                .Where(s => s.CircleLevel == childCircleLevel)
                .ToList();

            // 更新子圈技能点的IsLearnable状态
            foreach (var childSkill in childSkills)
            {
                UpdateSiblingLearnableStatus(childSkill);
            }
        }

        /// <summary>
        /// 自动装配技能点
        /// SkillPointsLeftUp[0] -> 4点
        /// SkillPointsLeftUp[2] -> 4点
        /// SkillPointsLeftUp[5] -> 2点
        /// SkillPointsRightUp[1] -> 4点
        /// SkillPointsRightUp[3] -> 4点
        /// SkillPointsRightUp[5] -> 2点
        /// </summary>
        private void AutoAssignSkills()
        {
            // 先重置所有技能
            ResetAllSkills();

            // 分配LeftUp的技能
            AutoLearnSkill(SkillPointsLeftUp[0], 4);  // Index 0: 4 points
            AutoLearnSkill(SkillPointsLeftUp[2], 4);  // Index 2: 4 points
            AutoLearnSkill(SkillPointsLeftUp[5], 2);  // Index 5: 2 points

            // 分配RightUp的技能
            AutoLearnSkill(SkillPointsRightUp[1], 4); // Index 1: 4 points
            AutoLearnSkill(SkillPointsRightUp[3], 4); // Index 3: 4 points
            AutoLearnSkill(SkillPointsRightUp[5], 2); // Index 5: 2 points
        }

        /// <summary>
        /// 为指定的技能点自动学习指定数量的点数
        /// </summary>
        private void AutoLearnSkill(SkillPointItemViewModel skillPoint, int pointsToLearn)
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
        /// 重置所有技能点
        /// </summary>
        private void ResetAllSkills()
        {
            var allSkillPoints = new[] { SkillPointsLeftUp, SkillPointsLeftDown, SkillPointsRightUp, SkillPointsRightDown }
                .SelectMany(collection => collection)
                .ToList();

            foreach (var skillPoint in allSkillPoints)
            {
                skillPoint.CurrentLearned = 0;
            }

            RemainingPoints = TOTAL_SKILL_POINTS;
            UpdateSkillPointsEnabled();

            // 重新初始化所有技能点的IsLearnable状态
            InitializeLearnableStatus();

            // 触发Command的CanExecuteChanged事件
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();
        }
    }
}
