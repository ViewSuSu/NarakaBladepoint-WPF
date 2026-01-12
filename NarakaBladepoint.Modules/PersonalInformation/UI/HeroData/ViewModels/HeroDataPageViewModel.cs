using System.Collections.Generic;
using System.Linq;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.Models;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Enums;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.ViewModels
{
    internal class HeroDataPageViewModel : ViewModelBase
    {
        private readonly IHeroDataProvider _heroDataProvider;
        private readonly ICurrentUserInfoProvider _currentUserInformationProvider;

        public HeroDataPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInformationProvider,
            IHeroDataProvider heroDataProvider
        )
            : base(containerProvider)
        {
            _currentUserInformationProvider = currentUserInformationProvider;
            _heroDataProvider = heroDataProvider;

            this.SeasonDataModels = currentUserInformationProvider.GetPersonalSeasonsAsync().Result;
            SelectedSeasonDataModel = SeasonDataModels.FirstOrDefault();
            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;

            // 初始化默认值
            SelectedTeamSize = TeamSize.Trio;
            SelectedGameMode = GameMode.Ranked;

            // 加载英雄数据
            LoadHeroData();
        }

        public List<SeasonDataModel> SeasonDataModels { get; }

        private SeasonDataModel _selectedSeasonDataModel;

        public SeasonDataModel SelectedSeasonDataModel
        {
            get { return _selectedSeasonDataModel; }
            set
            {
                _selectedSeasonDataModel = value;
                RaisePropertyChanged();
                LoadHeroData();
            }
        }

        public UserInformationData CurrentUserInformationModel { get; }

        // 英雄数据列表
        private List<Models.HeroDataItemModel> _heroDataList;
        public List<Models.HeroDataItemModel> HeroDataList
        {
            get { return _heroDataList; }
            set
            {
                _heroDataList = value;
                RaisePropertyChanged();
            }
        }

        // 选中的英雄数据
        private Models.HeroDataItemModel _selectedHeroData;
        public Models.HeroDataItemModel SelectedHeroData
        {
            get { return _selectedHeroData; }
            set
            {
                _selectedHeroData = value;
                RaisePropertyChanged();
            }
        }

        // 选中的排位模式
        private TeamSize _selectedTeamSize = TeamSize.Trio;
        public TeamSize SelectedTeamSize
        {
            get { return _selectedTeamSize; }
            set
            {
                _selectedTeamSize = value;
                RaisePropertyChanged();
                LoadHeroData();
            }
        }

        // 游戏模式列表
        public List<KeyValuePair<string, GameMode>> GameModeItems { get; } =
            Enum.GetValues<GameMode>()
                .Select(mode => new KeyValuePair<string, GameMode>(
                    mode.GetDescription() ?? mode.ToString(),
                    mode
                ))
                .ToList();

        // 选中的游戏模式
        private GameMode _selectedGameMode = GameMode.Ranked;
        public GameMode SelectedGameMode
        {
            get { return _selectedGameMode; }
            set
            {
                _selectedGameMode = value;
                RaisePropertyChanged();
                LoadHeroData();
            }
        }

        // 英雄对比排序类型列表
        public List<KeyValuePair<string, HeroCompareSortType>> HeroCompareSortTypeItems { get; } =
            Enum.GetValues<HeroCompareSortType>()
                .Select(sortType => new KeyValuePair<string, HeroCompareSortType>(
                    sortType.GetDescription() ?? sortType.ToString(),
                    sortType
                ))
                .ToList();

        // 选中的英雄对比排序类型
        private HeroCompareSortType _selectedHeroCompareSortType;
        public HeroCompareSortType SelectedHeroCompareSortType
        {
            get { return _selectedHeroCompareSortType; }
            set
            {
                _selectedHeroCompareSortType = value;
                RaisePropertyChanged();
                LoadHeroData();
            }
        }

        // 加载英雄数据
        private void LoadHeroData()
        {
            // 使用选中的赛季类型，而不是固定的All
            var selectedSeasonType = SelectedSeasonDataModel?.SeasonType ?? SeasonType.All;

            var sharedHeroDataList = _heroDataProvider
                .GetHeroDataListAsync(SelectedGameMode, SelectedTeamSize, selectedSeasonType)
                .Result;

            // 将共享数据模型转换为UI模型
            HeroDataList = sharedHeroDataList
                .Select(data =>
                    data.ConvertTo<Shared.Datas.HeroDataModel, Models.HeroDataItemModel>()
                )
                .ToList();

            SelectedHeroData = HeroDataList?.FirstOrDefault();
            int currentHeroIndex = SelectedHeroData?.HeroIndex ?? -1;

            // 尝试找到与之前选中相同索引的英雄数据
            var matchedHeroData = HeroDataList.FirstOrDefault(h => h.HeroIndex == currentHeroIndex);
            if (matchedHeroData != null)
            {
                SelectedHeroData = matchedHeroData;
            }
        }
    }
}
