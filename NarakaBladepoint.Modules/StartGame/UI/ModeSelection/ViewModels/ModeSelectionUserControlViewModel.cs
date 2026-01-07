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
        private readonly IServerInformation serverInformation;
        private readonly ICurrentUserInformationProvider currentUserInformationProvider;
        private readonly IHeroInfomation heroInfomation;
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

        public int SelectedMapCount { get; private set; }

        public ModeSelectionUserControlViewModel(
            IContainerProvider containerProvider,
            IServerInformation serverInformation,
            ICurrentUserInformationProvider currentUserInformationProvider,
            IHeroInfomation heroInfomation,
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
            this.ServerInfos = serverInformation.GetServerInformationAsync().Result;
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
