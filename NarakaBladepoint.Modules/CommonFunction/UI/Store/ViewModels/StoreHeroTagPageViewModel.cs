using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Commands;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Store.ViewModels
{
    internal class StoreHeroTagPageViewModel : ViewModelBase
    {
        private int _selectedTagIndex = 1;
        private string _selectedTagName = "壹·青空阁";
        private ObservableCollection<ImageSource> _currentTagImages;
        private DelegateCommand<string> _selectTagCommand;

        // 标签索引与名称的映射
        private static readonly Dictionary<int, string> TagNameMap = new()
        {
            { 1, "壹·青空阁" },
            { 2, "贰·蓝蔚阁" },
            { 3, "叁·碧天阁" },
            { 4, "肆·赤霞阁" },
            { 5, "伍·橙云阁" },
            { 6, "陆·紫冥阁" }
        };

        public int SelectedTagIndex
        {
            get => _selectedTagIndex;
            set
            {
                if (SetProperty(ref _selectedTagIndex, value))
                {
                    LoadTagImages(_selectedTagIndex);
                    // 更新选中的标签名称
                    if (TagNameMap.TryGetValue(value, out var tagName))
                    {
                        SelectedTagName = tagName;
                    }
                }
            }
        }

        public string SelectedTagName
        {
            get => _selectedTagName;
            set => SetProperty(ref _selectedTagName, value);
        }

        public ObservableCollection<ImageSource> CurrentTagImages
        {
            get => _currentTagImages;
            set => SetProperty(ref _currentTagImages, value);
        }

        public DelegateCommand<string> SelectTagCommand =>
            _selectTagCommand ??= new DelegateCommand<string>(tagIndexStr =>
            {
                if (int.TryParse(tagIndexStr, out var tagIndex))
                {
                    SelectedTagIndex = tagIndex;
                }
            });

        public StoreHeroTagPageViewModel()
        {
            LoadTagImages(1);
        }

        private void LoadTagImages(int tagIndex)
        {
            var images = ResourceImageReader.GetStoreHeroTagImages(tagIndex);
            CurrentTagImages = new ObservableCollection<ImageSource>(images);
        }
    }
}
