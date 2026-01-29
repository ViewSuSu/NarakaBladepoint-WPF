namespace NarakaBladepoint.Modules.PersonalInformation.UI.SeasonData.ViewModels
{
    internal class SeasonDataPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider _currentUserInfoProvider;
        private readonly ISeasonDataProvider _seasonDataProvider;

        public SeasonDataPageViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider,
            ISeasonDataProvider seasonDataProvider
        )
        {
            _currentUserInfoProvider = currentUserInfoProvider;
            _seasonDataProvider = seasonDataProvider;

            // 获取赛季数据列表
            this.SeasonDataModels = currentUserInfoProvider.GetPersonalSeasonsAsync().Result;
            SelectedSeasonDataModel = SeasonDataModels.FirstOrDefault();

            // 获取当前用户信息
            this.CurrentUserInfoModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;

            // 初始化默认值
            SelectedTeamSize = TeamSize.Trio;
            SelectedGameMode = GameMode.Ranked;

            // 加载赛季统计数据
            LoadSeasonData();
        }

        // 赛季数据列表
        public List<SeasonDataModel> SeasonDataModels { get; }

        // 选中的赛季
        private SeasonDataModel _selectedSeasonDataModel;

        public SeasonDataModel SelectedSeasonDataModel
        {
            get { return _selectedSeasonDataModel; }
            set
            {
                _selectedSeasonDataModel = value;
                RaisePropertyChanged();
                LoadSeasonData();
            }
        }

        // 当前用户信息
        public UserInformationData CurrentUserInfoModel { get; }

        // 赛季统计数据
        private SeasonStatisticsDataModel _seasonStatistics;

        public SeasonStatisticsDataModel SeasonStatistics
        {
            get { return _seasonStatistics; }
            set
            {
                _seasonStatistics = value;
                RaisePropertyChanged();
            }
        }

        // 选中的队伍规模
        private TeamSize _selectedTeamSize = TeamSize.Trio;

        public TeamSize SelectedTeamSize
        {
            get { return _selectedTeamSize; }
            set
            {
                _selectedTeamSize = value;
                RaisePropertyChanged();
                LoadSeasonData();
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
                LoadSeasonData();
            }
        }

        // 加载赛季数据
        private void LoadSeasonData()
        {
            // 获取选中的赛季类型
            var seasonType = SelectedSeasonDataModel?.SeasonType ?? SeasonType.All;

            // 从服务中获取赛季统计数据
            var statistics = _seasonDataProvider
                .GetSeasonStatisticsAsync(SelectedGameMode, SelectedTeamSize, seasonType)
                .Result;

            SeasonStatistics = statistics;
        }
    }
}
