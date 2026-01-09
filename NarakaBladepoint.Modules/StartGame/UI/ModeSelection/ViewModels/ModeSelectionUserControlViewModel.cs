using System.Windows.Media;
using NarakaBladepoint.Modules.StartGame.UI.HeroChose.Views;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.Views;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.StartGame.UI.ModeSelection.ViewModels
{
    internal class ModeSelectionUserControlViewModel : CanRemoveHomePageRegionViewModelBase
    {
        public List<ServerInformationModel> ServerInfos { get; private set; }

        private ServerInformationModel selectedItem;
        private readonly IServerInfoProvider serverInformation;
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly IHeroInfoProvider heroInfomation;
        private readonly IMapProvider mapProvider;

        public ServerInformationModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _firstHeroAvatar;
        private ImageSource _secondHeroAvatar;
        private ImageSource _thirdHeroAvatar;
        public ImageSource FirstHeroAvatar
        {
            get => _firstHeroAvatar;
            private set
            {
                if (_firstHeroAvatar != value)
                {
                    _firstHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ImageSource SecondHeroAvatar
        {
            get => _secondHeroAvatar;
            private set
            {
                if (_secondHeroAvatar != value)
                {
                    _secondHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ImageSource ThirdHeroAvatar
        {
            get => _thirdHeroAvatar;
            private set
            {
                if (_thirdHeroAvatar != value)
                {
                    _thirdHeroAvatar = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int _selectedMapCount;
        public int SelectedMapCount
        {
            get { return _selectedMapCount; }
            set
            {
                _selectedMapCount = value;
                RaisePropertyChanged();
            }
        }

        private bool isSoloMode;

        /// <summary>
        /// 是否是单排
        /// </summary>
        public bool IsSoloMode
        {
            get { return isSoloMode; }
            set
            {
                isSoloMode = value;
                RaisePropertyChanged();
            }
        }

        public ModeSelectionUserControlViewModel(
            IContainerProvider containerProvider,
            IServerInfoProvider serverInformation,
            ICurrentUserInfoProvider currentUserInformationProvider,
            IHeroInfoProvider heroInfomation,
            IMapProvider mapProvider
        )
            : base(containerProvider)
        {
            this.serverInformation = serverInformation;
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.heroInfomation = heroInfomation;
            this.mapProvider = mapProvider;
            ChoseMapCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(nameof(MapChosePage));
            });
            ChoseHeroCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(nameof(HeroChoseUserControl));
            });
        }

        public DelegateCommand ChoseHeroCommand { get; set; }
        public DelegateCommand ChoseMapCommand { get; set; }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            this.ServerInfos = serverInformation.GeAlltServerInfosAsync().Result;
            SelectedItem = ServerInfos.FirstOrDefault();

            var userModel = currentUserInformationProvider.GetCurrentUserInfoAsync().Result;
            this.FirstHeroAvatar =
                userModel.FirstPickHeroIndex != -1
                    ? heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.FirstPickHeroIndex)
                        .Result.Avatar
                    : null;
            this.SecondHeroAvatar =
                userModel.SecondPickHeroIndex != -1
                    ? heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.SecondPickHeroIndex)
                        .Result.Avatar
                    : null;
            this.ThirdHeroAvatar =
                userModel.ThridPickHeroIndex != -1
                    ? heroInfomation
                        .GetHeroAvatarModelByIdAsync(userModel.ThridPickHeroIndex)
                        .Result.Avatar
                    : null;
            this.SelectedMapCount = mapProvider.GetSelectedMapCountAsync().Result;
        }
    }
}
