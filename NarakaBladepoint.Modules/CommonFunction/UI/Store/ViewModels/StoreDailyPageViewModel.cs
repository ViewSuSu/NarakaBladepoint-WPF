using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Resources;
using Prism.Commands;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Store.ViewModels
{
    internal class StoreDailyPageViewModel : ViewModelBase
    {
        private ObservableCollection<ImageSource> _propImages;
        private ObservableCollection<ImageSource> _huanSiImages;
        private ObservableCollection<ImageSource> _giftImages;
        private ObservableCollection<ImageSource> _currentImages;
        private DelegateCommand _selectPropCommand;
        private DelegateCommand _selectHuanSiCommand;
        private DelegateCommand _selectGiftCommand;

        public ObservableCollection<ImageSource> PropImages
        {
            get
            {
                if (_propImages == null)
                {
                    _propImages = new ObservableCollection<ImageSource>(ResourceImageReader.GetAllStoreDailyPropImages());
                }
                return _propImages;
            }
        }

        public ObservableCollection<ImageSource> HuanSiImages
        {
            get
            {
                if (_huanSiImages == null)
                {
                    _huanSiImages = new ObservableCollection<ImageSource>(ResourceImageReader.GetAllStoreDailyHuanSiImages());
                }
                return _huanSiImages;
            }
        }

        public ObservableCollection<ImageSource> GiftImages
        {
            get
            {
                if (_giftImages == null)
                {
                    _giftImages = new ObservableCollection<ImageSource>(ResourceImageReader.GetAllStoreDailyGiftImages());
                }
                return _giftImages;
            }
        }

        public ObservableCollection<ImageSource> CurrentImages
        {
            get
            {
                if (_currentImages == null)
                {
                    _currentImages = PropImages;
                }
                return _currentImages;
            }
            set
            {
                SetProperty(ref _currentImages, value);
            }
        }

        public DelegateCommand SelectPropCommand =>
            _selectPropCommand ??= new DelegateCommand(SelectProp);

        public DelegateCommand SelectHuanSiCommand =>
            _selectHuanSiCommand ??= new DelegateCommand(SelectHuanSi);

        public DelegateCommand SelectGiftCommand =>
            _selectGiftCommand ??= new DelegateCommand(SelectGift);

        public void SelectProp()
        {
            CurrentImages = PropImages;
        }

        public void SelectHuanSi()
        {
            CurrentImages = HuanSiImages;
        }

        public void SelectGift()
        {
            CurrentImages = GiftImages;
        }
    }
}
