using System.Windows;
using NarakaBladepoint.Modules.SocialTag.UI.Views;
using NarakaBladepoint.Resources;
using NarakaBladepoint.Shared.Datas;

namespace NarakaBladepoint.Modules.Social.UI.Friend.ViewModels
{
    internal class FriendPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly ITipMessageService tipMessageService;

        private List<FriendDataItem> _friends = [];

        public List<FriendDataItem> Friends
        {
            get { return _friends; }
            set
            {
                _friends = value;
                RaisePropertyChanged();
            }
        }

        private List<TeammateDataItem> _teammates = [];

        public List<TeammateDataItem> Teammates
        {
            get { return _teammates; }
            set
            {
                _teammates = value;
                RaisePropertyChanged();
            }
        }

        private bool _isHaveVilidTag;

        public bool IsHaveVilidTag
        {
            get { return _isHaveVilidTag; }
            set
            {
                _isHaveVilidTag = value;
                RaisePropertyChanged();
            }
        }

        public FriendPageViewModel(
            ICurrentUserInfoProvider currentUserInformationProvider,
            ITipMessageService tipMessageService
        )
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.tipMessageService = tipMessageService;
            Init();
        }

        private void Init()
        {
            Friends = currentUserInformationProvider.GetFriendsAsync().Result;
            this.CurrentUserInfoModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
            this.IsHaveVilidTag = CurrentUserInfoModel.IsExsitAnyValidSocialTag;
            InitTeammates();
        }

        private void InitTeammates()
        {
            var random = new Random();
            int avatarCount = ResourceImageReader.AvatarCount;

            Teammates = new List<TeammateDataItem>
            {
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = "轩墨麟泉",
                    Description = "排位赛三排",
                    Tags = new() { "排位赛三排", "精通刘娆", "乐于沟通" }
                },
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = "鸮鸶鸟",
                    Description = "精通刘娆",
                    Tags = new() { "精通刘娆", "乐于沟通" }
                },
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = "秦了oxo",
                    Description = "乐于沟通",
                    Tags = new() { "乐于沟通", "精通魅轻", "掌控全场" }
                },
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = "王不言王不语",
                    Description = "救援达人",
                    Tags = new() { "救援达人", "乐于沟通", "社交达人" }
                },
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = ".Diva.",
                    Description = "社交达人",
                    Tags = new() { "社交达人", "擅长英雄互补", "段位相近" }
                },
                new()
                {
                    AvatarIndex = random.Next(0, avatarCount),
                    Name = "郭清",
                    Description = "活跃玩家",
                    Tags = new() { "活跃玩家", "擅长英雄互补", "乐于沟通" }
                }
            };
        }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            Init();
        }

        public UserInformationData CurrentUserInfoModel { get; private set; }

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand =>
            _closeCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });

        private DelegateCommand _settingTagCommand;

        public DelegateCommand SettingTagCommand =>
            _settingTagCommand ??= new DelegateCommand(() =>
            {
                ReturnCommand.Execute();
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(SocialTagPage)));
            });

        private DelegateCommand<FriendDataItem> _sayHelloCommand;

        public DelegateCommand<FriendDataItem> SayHelloCommand =>
            _sayHelloCommand ??= new DelegateCommand<FriendDataItem>(
                async (selectedItem) =>
                {
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs(
                            $"您已问候:{selectedItem.Name},亲密度+10",
                            [selectedItem.Name, "+10"]
                        )
                    );
                }
            );

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand =>
            _searchCommand ??= new DelegateCommand<string>(async keyword =>
            {
                Friends = await currentUserInformationProvider.GetFriendsAsync(keyword);
            });

        private DelegateCommand _copyIdCommand;

        public DelegateCommand CopyIdCommand =>
            _copyIdCommand ??= new DelegateCommand(async () =>
            {
                try
                {
                    Clipboard.SetText(CurrentUserInfoModel.Id.ToString());
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs("复制成功")
                    );
                }
                catch
                {
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs("复制失败")
                    );
                }
            });

        private DelegateCommand<TeammateDataItem> _blockCommand;

        public DelegateCommand<TeammateDataItem> BlockCommand =>
            _blockCommand ??= new DelegateCommand<TeammateDataItem>(
                async (teammate) =>
                {
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs($"已屏蔽 {teammate.Name}")
                    );
                }
            );

        private DelegateCommand<TeammateDataItem> _recruitCommand;

        public DelegateCommand<TeammateDataItem> RecruitCommand =>
            _recruitCommand ??= new DelegateCommand<TeammateDataItem>(
                async (teammate) =>
                {
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs($"已向 {teammate.Name} 发送招募邀请")
                    );
                }
            );
    }
}
