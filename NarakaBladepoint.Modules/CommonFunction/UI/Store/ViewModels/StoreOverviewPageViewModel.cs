using System.Collections.ObjectModel;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Bases.ViewModels;
using NarakaBladepoint.Resources;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Store.ViewModels
{
    internal class StoreOverviewPageViewModel : ViewModelBase
    {
        private ObservableCollection<ImageSource> _overviewImages;
        private ObservableCollection<string> _qualityList;
        private ObservableCollection<string> _categoryList;
        private ObservableCollection<string> _sortList;
        private int _selectedQualityIndex = 0;
        private int _selectedCategoryIndex = 0;
        private int _selectedSortIndex = 0;

        public ObservableCollection<ImageSource> OverviewImages
        {
            get
            {
                if (_overviewImages == null)
                {
                    _overviewImages = new ObservableCollection<ImageSource>(ResourceImageReader.GetAllStoreOverviewImages());
                }
                return _overviewImages;
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
        /// 分类列表
        /// </summary>
        public ObservableCollection<string> CategoryList
        {
            get
            {
                if (_categoryList == null)
                {
                    _categoryList = new ObservableCollection<string>
                    {
                        "全部种类",
                        "时装",
                        "武器皮肤",
                        "抜缘包",
                        "挂饰",
                        "发型",
                        "商城道具",
                        "动作",
                        "气泡",
                        "背景",
                        "姿势",
                        "底座",
                        "头像",
                        "提脸部件",
                        "击败撤报",
                        "救援特效",
                        "战利标记",
                        "落物堆皮肤",
                        "烟花",
                        "语音包",
                        "技能奥义",
                        "英雄曲",
                        "赠礼"
                    };
                }
                return _categoryList;
            }
        }

        /// <summary>
        /// 排序列表
        /// </summary>
        public ObservableCollection<string> SortList
        {
            get
            {
                if (_sortList == null)
                {
                    _sortList = new ObservableCollection<string>
                    {
                        "默认排序",
                        "价格降序",
                        "价格升序"
                    };
                }
                return _sortList;
            }
        }

        /// <summary>
        /// 选中的品质索引
        /// </summary>
        public int SelectedQualityIndex
        {
            get => _selectedQualityIndex;
            set => SetProperty(ref _selectedQualityIndex, value);
        }

        /// <summary>
        /// 选中的分类索引
        /// </summary>
        public int SelectedCategoryIndex
        {
            get => _selectedCategoryIndex;
            set => SetProperty(ref _selectedCategoryIndex, value);
        }

        /// <summary>
        /// 选中的排序方式索引
        /// </summary>
        public int SelectedSortIndex
        {
            get => _selectedSortIndex;
            set => SetProperty(ref _selectedSortIndex, value);
        }
    }
}
