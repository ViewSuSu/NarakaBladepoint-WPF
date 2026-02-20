using System;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.ViewModels
{
    internal class SkillPointItemViewModel : BindableBase
    {
        private int _currentLearned;
        private int _totalLearnable;

        public string SkillName { get; set; }
        public int CircleLevel { get; set; }  // 1, 2, or 3
        public string Position { get; set; }  // LeftUp, LeftDown, RightUp, RightDown
        public string Direction { get; set; } // TenOClock or ElevenOClock
        public int Index { get; set; }

        public int CurrentLearned
        {
            get { return _currentLearned; }
            set { SetProperty(ref _currentLearned, value); }
        }

        public int TotalLearnable
        {
            get { return _totalLearnable; }
            set { SetProperty(ref _totalLearnable, value); }
        }

        public SkillPointItemViewModel()
        {
            CurrentLearned = 0;
        }
    }

    internal class SkillPointPageViewModel : CanRemoveMainContentRegionViewModelBase
    {
        private int _remainingPoints;
        private bool _isSkillPointsEnabled;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsLeftDown;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightUp;
        private ObservableCollection<SkillPointItemViewModel> _skillPointsRightDown;

        public int RemainingPoints
        {
            get { return _remainingPoints; }
            set { SetProperty(ref _remainingPoints, value); }
        }

        public bool IsSkillPointsEnabled
        {
            get { return _isSkillPointsEnabled; }
            set { SetProperty(ref _isSkillPointsEnabled, value); }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsLeftUp
        {
            get { return _skillPointsLeftUp; }
            set { SetProperty(ref _skillPointsLeftUp, value); }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsLeftDown
        {
            get { return _skillPointsLeftDown; }
            set { SetProperty(ref _skillPointsLeftDown, value); }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsRightUp
        {
            get { return _skillPointsRightUp; }
            set { SetProperty(ref _skillPointsRightUp, value); }
        }

        public ObservableCollection<SkillPointItemViewModel> SkillPointsRightDown
        {
            get { return _skillPointsRightDown; }
            set { SetProperty(ref _skillPointsRightDown, value); }
        }

        public DelegateCommand<SkillPointItemViewModel> LearnSkillCommand { get; set; }
        public DelegateCommand<SkillPointItemViewModel> UnlearnSkillCommand { get; set; }

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

            LearnSkillCommand = new DelegateCommand<SkillPointItemViewModel>(LearnSkill, CanLearnSkill);
            UnlearnSkillCommand = new DelegateCommand<SkillPointItemViewModel>(UnlearnSkill, CanUnlearnSkill);

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
                new { Name = "He_TenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "TenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Ou_ElevenOClock_MirrorV", Circle = 2, Position = "LeftDown", Direction = "ElevenOClock", Max = SECOND_CIRCLE_MAX },
                new { Name = "Shi_TenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "TenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Xiong_ElevenOClock_MirrorV", Circle = 3, Position = "LeftDown", Direction = "ElevenOClock", Max = THIRD_CIRCLE_MAX },
                new { Name = "Ma_TenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "TenOClock", Max = FIRST_CIRCLE_MAX },
                new { Name = "Lu_ElevenOClock_MirrorV", Circle = 1, Position = "LeftDown", Direction = "ElevenOClock", Max = FIRST_CIRCLE_MAX },

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
        }

        private void LearnSkill(SkillPointItemViewModel skillPoint)
        {
            if (!CanLearnSkill(skillPoint))
                return;

            // Check if this is a conflict situation (10 and 11 o'clock in same circle)
            if (!CanLearnDueToConflict(skillPoint))
                return;

            // Check if previous circle is fully learned in this position
            if (!IsCircleReadyToLearn(skillPoint))
                return;

            skillPoint.CurrentLearned++;
            RemainingPoints--;

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

            UpdateSkillPointsEnabled();
            LearnSkillCommand.RaiseCanExecuteChanged();
            UnlearnSkillCommand.RaiseCanExecuteChanged();
        }

        private bool CanLearnSkill(SkillPointItemViewModel skillPoint)
        {
            return skillPoint != null &&
                   RemainingPoints > 0 &&
                   skillPoint.CurrentLearned < skillPoint.TotalLearnable &&
                   IsSkillPointsEnabled;
        }

        private bool CanUnlearnSkill(SkillPointItemViewModel skillPoint)
        {
            return skillPoint != null && skillPoint.CurrentLearned > 0;
        }

        private bool CanLearnDueToConflict(SkillPointItemViewModel skillPoint)
        {
            // Find the conflicting skill in the same corner and circle
            var conflicting = FindConflictingSkill(skillPoint);
            if (conflicting != null && conflicting.CurrentLearned > 0)
            {
                // If other direction is already learned, this one cannot be learned
                return false;
            }
            return true;
        }

        private SkillPointItemViewModel FindConflictingSkill(SkillPointItemViewModel skillPoint)
        {
            // Get the position collection
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // Find the conflicting skill in the same circle but different direction
            foreach (var skill in positionCollection)
            {
                if (skill != skillPoint &&
                    skill.CircleLevel == skillPoint.CircleLevel &&
                    skill.Direction != skillPoint.Direction)
                {
                    return skill;
                }
            }
            return null;
        }

        // Overloaded method that checks prerequisites properly
        private bool IsCircleReadyToLearn(SkillPointItemViewModel skillPoint)
        {
            if (skillPoint.CircleLevel == 1)
                return true; // First circle can always be learned

            int previousCircleLevel = skillPoint.CircleLevel - 1;

            // Get the appropriate collection based on position
            var positionCollection = GetSkillPointCollectionByPosition(skillPoint.Position);

            // Find both skills in the same position and previous circle
            var previousCircleSkills = positionCollection.Where(s => 
                s.CircleLevel == previousCircleLevel).ToList();

            // All skills in the previous circle of this position must be maxed out
            foreach (var previousSkill in previousCircleSkills)
            {
                if (previousSkill.CurrentLearned < previousSkill.TotalLearnable)
                {
                    return false; // Previous circle not fully learned
                }
            }

            return true; // All prerequisites are met
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
    }
}
