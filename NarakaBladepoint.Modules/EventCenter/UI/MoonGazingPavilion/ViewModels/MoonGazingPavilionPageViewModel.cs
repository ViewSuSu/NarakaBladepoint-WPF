using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Framework.Core.Infrastructure;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.EventCenter.UI.MoonGazingPavilion.ViewModels
{
    internal class MoonGazingPavilionPageViewModel : CanRemoveMainContentRegionViewModelBase
    {
        private IEnumerable<BitmapImage> _moonGazingPavilionImages;
        private ObservableCollection<string> _categoryItems;
        private string _selectedCategory;

        public IEnumerable<BitmapImage> MoonGazingPavilionImages
        {
            get => _moonGazingPavilionImages;
            set => SetProperty(ref _moonGazingPavilionImages, value);
        }

        public ObservableCollection<string> CategoryItems
        {
            get => _categoryItems;
            set => SetProperty(ref _categoryItems, value);
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public MoonGazingPavilionPageViewModel()
        {
            LoadMoonGazingPavilionImages();
            InitializeCategories();
        }

        private void LoadMoonGazingPavilionImages()
        {
            var images = ResourceImageReader.GetAllMoonGazingPavilionImages();
            MoonGazingPavilionImages = images.Cast<BitmapImage>().ToList();
        }

        private void InitializeCategories()
        {
            CategoryItems = new ObservableCollection<string>
            {
                "全部种类",
                "时装",
                "武器皮肤",
                "挂饰",
                "发型",
                "商城道具",
                "气泡",
                "背景",
                "姿势",
                "底座",
                "击败播报",
                "救援特效",
                "落物堆皮肤",
                "语音包"
            };

            SelectedCategory = CategoryItems.FirstOrDefault();
        }
    }
}
