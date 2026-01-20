using System.Windows;
using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Modules.SocialTag.UI.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.Social.UI.Friend.UI.ViewModels
{
    internal class FriendPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly ITipMessageService tipMessageService;

        public List<FriendDataItem> Friends { get; set; } = [];

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
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInformationProvider,
            ITipMessageService tipMessageService
        )
            : base(containerProvider)
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

        private DelegateCommand _sayHelloCommand;

        public DelegateCommand SayHelloCommand =>
            _sayHelloCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync("已打招呼！");
            });

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand =>
            _searchCommand ??= new DelegateCommand<string>(keyword =>
            {
                Friends = currentUserInformationProvider.GetFriendsAsync(keyword).Result;
            });

        private DelegateCommand _copyIdCommand;

        public DelegateCommand CopyIdCommand =>
            _copyIdCommand ??= new DelegateCommand(async () =>
            {
                try
                {
                    Clipboard.SetText(CurrentUserInfoModel.Id.ToString());
                    await tipMessageService.ShowTipMessageAsync("已复制");
                }
                catch
                {
                    await tipMessageService.ShowTipMessageAsync("复制失败");
                }
            });
    }
}
