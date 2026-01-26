using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NarakaBladepoint.Shared.Jsons;
using NarakaBladepoint.Shared.Services.Models;
using Prism.Common;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecordDetail.ViewModels
{
    internal class HistoryMatchRecordDetailPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly ITipMessageService tipMessageService;

        public UserInformationData CurrentUserInfoModel { get; }

        private MatchDataItem _currentMatchDataItem;
        private ObservableCollection<MatchTeamMemberItem> _matchRecords;
        private ObservableCollection<ImageSource> _currentUserImageSouces;
        private DelegateCommand<MatchTeamMemberItem> _addFriendCommand;
        private DelegateCommand<MatchTeamMemberItem> _reportCommand;
        private int _selectedIndex;

        public MatchDataItem CurrentMatchDataItem
        {
            get => _currentMatchDataItem;
            set
            {
                _currentMatchDataItem = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<MatchTeamMemberItem> MatchRecords
        {
            get => _matchRecords;
            set
            {
                _matchRecords = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ImageSource> CurrentUserImageSouces
        {
            get => _currentUserImageSouces;
            set
            {
                _currentUserImageSouces = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<MatchTeamMemberItem> AddFriendCommand
        {
            get
            {
                if (_addFriendCommand == null)
                {
                    _addFriendCommand = new DelegateCommand<MatchTeamMemberItem>(ExecuteAddFriend);
                }
                return _addFriendCommand;
            }
        }

        public DelegateCommand<MatchTeamMemberItem> ReportCommand
        {
            get
            {
                if (_reportCommand == null)
                {
                    _reportCommand = new DelegateCommand<MatchTeamMemberItem>(ExecuteReport);
                }
                return _reportCommand;
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        public HistoryMatchRecordDetailPageViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider,
            ITipMessageService tipMessageService
        )
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.tipMessageService = tipMessageService;
            this.CurrentUserInfoModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
            MatchRecords = new ObservableCollection<MatchTeamMemberItem>();
            CurrentUserImageSouces = new ObservableCollection<ImageSource>();
        }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            if (
                navigationContext.Parameters.TryGetValue<MatchDataItem>(
                    nameof(MatchDataItem),
                    out var matchDataItem
                )
            )
            {
                CurrentMatchDataItem = matchDataItem;

                // 加载当前用户的成就图片
                LoadCurrentUserAchievements();

                // 加载当前队伍的成员（通过服务方法）
                LoadMatchTeamMembers();
            }
        }

        private void LoadCurrentUserAchievements()
        {
            CurrentUserImageSouces.Clear();

            if (CurrentMatchDataItem?.Achievements != null)
            {
                foreach (var achievement in CurrentMatchDataItem.Achievements)
                {
                    CurrentUserImageSouces.Add(achievement);
                }
            }
        }

        private void LoadMatchTeamMembers()
        {
            MatchRecords.Clear();

            // 构建当前用户的对局信息
            var currentUserMatch = new MatchTeamMemberItem
            {
                Name = CurrentUserInfoModel.Name,
                Avatar = CurrentUserInfoModel.Avatar,
                Titles = CurrentMatchDataItem?.Achievements ?? new List<ImageSource>(),
                SurvivalTime = FormatSurvivalTime(Random.Shared.Next(0, 20 * 60 + 1)),
                TeamKills = CurrentMatchDataItem?.KillNumber ?? 0,
                TotalDamage = CurrentMatchDataItem?.Damage ?? 0,
                TotalHealing = Random.Shared.Next(0, 2000),
                AwardedTeams = Random.Shared.Next(0, 5),
                Experience = Random.Shared.Next(0, 100),
                IsCurrentUser = true,
            };

            MatchRecords.Add(currentUserMatch);

            // 构建队伍中的其他成员信息（假数据）
            var teamMemberNames = new[] { "成员A", "成员B" };
            foreach (var memberName in teamMemberNames)
            {
                var teamMember = new MatchTeamMemberItem
                {
                    Name = memberName,
                    Avatar = CurrentUserInfoModel.Avatar,
                    Titles = new List<ImageSource>(),
                    SurvivalTime = FormatSurvivalTime(Random.Shared.Next(60, 3600)),
                    TeamKills = Random.Shared.Next(0, 15),
                    TotalDamage = Random.Shared.Next(100, 5000),
                    TotalHealing = Random.Shared.Next(0, 2000),
                    AwardedTeams = Random.Shared.Next(0, 5),
                    Experience = Random.Shared.Next(0, 100),
                    IsCurrentUser = false,
                };
                MatchRecords.Add(teamMember);
            }

            // 设置当前用户行为选中状态
            SelectedIndex = 0;
        }

        /// <summary>
        /// 将秒数格式化为 MM:SS 格式
        /// </summary>
        private string FormatSurvivalTime(int seconds)
        {
            var minutes = seconds / 60;
            var secs = seconds % 60;
            return $"{minutes:D2}:{secs:D2}";
        }

        /// <summary>
        /// 执行添加好友命令
        /// </summary>
        private async void ExecuteAddFriend(MatchTeamMemberItem member)
        {
            if (member != null)
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs(
                        $"已向 {member.Name} 发送好友请求",
                        [member.Name]
                    )
                );
            }
        }

        /// <summary>
        /// 执行举报命令
        /// </summary>
        private void ExecuteReport(MatchTeamMemberItem member)
        {
            if (member != null)
            {
                // TODO: 实现举报逻辑
            }
        }
    }
}
