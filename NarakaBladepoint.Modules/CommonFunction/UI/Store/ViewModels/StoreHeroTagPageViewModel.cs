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
        private ObservableCollection<string> _qualityList;
        private int _selectedQualityIndex = 0;
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
                if (_selectedTagIndex != value)
                {
                    _selectedTagIndex = value;
                    RaisePropertyChanged();
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
            set
            {
                if (_selectedTagName != value)
                {
                    _selectedTagName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<ImageSource> CurrentTagImages
        {
            get => _currentTagImages;
            set
            {
                if (_currentTagImages != value)
                {
                    _currentTagImages = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// 品质列表（全部品质、良品、优品、极品、神品）
        /// </summary>
        public ObservableCollection<string> QualityList
        {
            get
            {
                if (_qualityList == null)
                {
                    _qualityList = new ObservableCollection<string>
                    {
                        "全部品质",
                        "良品",
                        "优品",
                        "极品",
                        "神品"
                    };
                }
                return _qualityList;
            }
        }

        /// <summary>
        /// 选中的品质索引
        /// </summary>
        public int SelectedQualityIndex
        {
            get => _selectedQualityIndex;
            set
            {
                if (_selectedQualityIndex != value)
                {
                    _selectedQualityIndex = value;
                    RaisePropertyChanged();
                }
            }
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
