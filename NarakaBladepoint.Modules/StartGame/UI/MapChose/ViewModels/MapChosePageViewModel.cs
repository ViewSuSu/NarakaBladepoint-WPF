using System.ComponentModel;
using System.Windows.Media;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.Models;

namespace NarakaBladepoint.Modules.StartGame.UI.MapChose.ViewModels
{
    internal class MapChosePageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly IConfiguration configuration;

        public BindingList<MapChoseItemModel> MapItems { get; }

        private ImageSource _currentGifImageSource;

        public ImageSource CurrentGifImageSource
        {
            get { return _currentGifImageSource; }
            set
            {
                _currentGifImageSource = value;
                RaisePropertyChanged();
            }
        }

        private string descprition;

        public string Descprition
        {
            get { return descprition; }
            set
            {
                descprition = value;
                RaisePropertyChanged();
            }
        }

        public int SelectedCount => MapItems.Count(x => x.IsSelected);

        public MapChosePageViewModel(
            ICurrentUserInfoProvider currentUserInformationProvider,
            IMapProvider mapProvider,
            IConfiguration configuration
        )
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.configuration = configuration;
            var userModel = this.currentUserInformationProvider.GetCurrentUserInfoAsync().Result;
            TimeoutDefaultAllMap = userModel.TimeoutDefaultAllMap;
            this.MapItems = new BindingList<MapChoseItemModel>(
                mapProvider
                    .GetMapItemDatasAsync()
                    .Result.Select(x => new MapChoseItemModel(x) { IsSelected = x.IsSelected })
                    .ToList()
            );
            MapItems.ListChanged += (o, e) =>
            {
                RaisePropertyChanged(nameof(SelectedCount));
            };
            ClickMapCommand.Execute(MapItems.First());
        }

        private void Save()
        {
            var datas = MapItems.Select(x => x.MapItemData).ToList();
            for (int i = 0; i < MapItems.Count; i++)
            {
                datas[i].IsSelected = MapItems[i].IsSelected;
            }
            configuration.SaveAllAsyn(datas);
            ReturnCommand.Execute();
        }

        public bool TimeoutDefaultAllMap { get; set; }

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);

        private DelegateCommand<bool?> _checkAllMapCommand;

        public DelegateCommand<bool?> CheckAllMapCommand =>
            _checkAllMapCommand ??= new DelegateCommand<bool?>(
                (isCheckAll) =>
                {
                    foreach (var item in MapItems)
                    {
                        item.IsSelected = true;
                    }
                }
            );

        private DelegateCommand<bool?> _unCheckAllMapCommand;

        public DelegateCommand<bool?> UnCheckAllMapCommand =>
            _unCheckAllMapCommand ??= new DelegateCommand<bool?>(
                (isCheckAll) =>
                {
                    foreach (var item in MapItems)
                    {
                        item.IsSelected = false;
                    }
                }
            );

        private DelegateCommand<MapChoseItemModel> _clickMapCommand;

        public DelegateCommand<MapChoseItemModel> ClickMapCommand =>
            _clickMapCommand ??= new DelegateCommand<MapChoseItemModel>(item =>
            {
                CurrentGifImageSource = item.MapItemData.MapGif;
                Descprition = item.MapItemData.Description;
            });
    }
}
