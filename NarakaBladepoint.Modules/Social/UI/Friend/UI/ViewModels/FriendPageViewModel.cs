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
    }
}
