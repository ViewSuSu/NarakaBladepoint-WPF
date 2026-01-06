using NarakaBladepoint.Modules.StartGame.UI.MapChose.Views;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.StartGame.UI.ModeSelection.ViewModels
{
    internal class ModeSelectionUserControlViewModel : CanRemoveHomePageRegionViewModelBase
    {
        public List<ServerInformationModel> ServerInfos { get; }

        private ServerInformationModel selectedItem;

        public ServerInformationModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged();
            }
        }

        public ModeSelectionUserControlViewModel(
            IContainerProvider containerProvider,
            IServerInformation serverInformation
        )
            : base(containerProvider)
        {
            this.ServerInfos = serverInformation.GetServerInformationAsync().Result;
            SelectedItem = ServerInfos.FirstOrDefault();
            ChoseMapCommand = new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<LoadHomePageRegionEvent>().Publish(nameof(MapChosePage));
            });
        }

        public DelegateCommand ChoseHeroCommand { get; set; }
        public DelegateCommand ChoseMapCommand { get; set; }
    }
}
