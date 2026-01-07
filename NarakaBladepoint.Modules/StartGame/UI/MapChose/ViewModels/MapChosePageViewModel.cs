using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.StartGame.UI.MapChose.Models;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.StartGame.UI.MapChose.ViewModels
{
    internal class MapChosePageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInformationProvider currentUserInformationProvider;
        private readonly IMapProvider mapProvider;
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

        public int SelectedCount => MapItems.Where(x => x.IsSelected).Count();

        public MapChosePageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInformationProvider currentUserInformationProvider,
            IMapProvider mapProvider,
            IConfiguration configuration
        )
            : base(containerProvider)
        {
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.mapProvider = mapProvider;
            this.configuration = configuration;
            this.MapItems = new BindingList<MapChoseItemModel>(
                mapProvider
                    .GetMapItemDatasAsync()
                    .Result.Select(x => new MapChoseItemModel(x))
                    .ToList()
            );
            MapItems.ListChanged += (o, e) =>
            {
                RaisePropertyChanged(nameof(SelectedCount));
            };
            SaveCommand = new DelegateCommand(Save);
            CheckAllMapCommand = new DelegateCommand<bool?>(
                (isCheckAll) =>
                {
                    foreach (var item in MapItems)
                    {
                        item.IsSelected = true;
                    }
                }
            );
            UnCheckAllMapCommand = new DelegateCommand<bool?>(
                (isCheckAll) =>
                {
                    foreach (var item in MapItems)
                    {
                        item.IsSelected = false;
                    }
                }
            );
            ClickMapCommand = new DelegateCommand<MapChoseItemModel>(item =>
            {
                CurrentGifImageSource = item.MapItemData.MapGif;
                Descprition = item.MapItemData.Description;
            });
            ClickMapCommand.Execute(MapItems.First());
        }

        private void Save()
        {
            var datas = MapItems.Select(x => x.MapItemData).ToList();
            for (int i = 0; i < MapItems.Count; i++)
            {
                datas[i].IsSelected = MapItems[i].IsSelected;
            }
            configuration.SaveAll(datas);
            ReturnCommand.Execute();
        }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            var userModel = this.currentUserInformationProvider.GetCurrentUserInfoAsync().Result;
            TimeoutDefaultAllMap = userModel.TimeoutDefaultAllMap;
        }

        public bool TimeoutDefaultAllMap { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand<bool?> CheckAllMapCommand { get; set; }
        public DelegateCommand<bool?> UnCheckAllMapCommand { get; set; }
        public DelegateCommand<MapChoseItemModel> ClickMapCommand { get; set; }
    }
}
