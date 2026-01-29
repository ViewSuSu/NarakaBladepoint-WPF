namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models
{
    internal class PersonalInformationDetailModel : BindableBase
    {
        private SeasonType _seasonType;

        /// <summary>
        /// 赛季类型
        /// </summary>
        public SeasonType SeasonType
        {
            get => _seasonType;
            set
            {
                _seasonType = value;
                RaisePropertyChanged(); // 通知 SeasonType 自己
                RaisePropertyChanged(nameof(Season)); // 额外通知派生属性
            }
        }

        /// <summary>
        /// 赛季类型描述
        /// </summary>
        public string Season => SeasonType.GetDescription();

        private int _gameTime;

        /// <summary>
        /// 游戏时间
        /// </summary>
        public int GameTime
        {
            get => _gameTime;
            set
            {
                _gameTime = value;
                RaisePropertyChanged(); // 通知 GameTime
            }
        }

        private int _totalMatches;

        /// <summary>
        /// 总场次
        /// </summary>
        public int TotalMatches
        {
            get => _totalMatches;
            set
            {
                _totalMatches = value;
                RaisePropertyChanged(); // 通知 TotalMatches
            }
        }

        private int _totalDefeats;

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalDefeats
        {
            get => _totalDefeats;
            set
            {
                _totalDefeats = value;
                RaisePropertyChanged(); // 通知 TotalDefeats
            }
        }

        private int _rankedMatches;

        /// <summary>
        /// 排位赛场次
        /// </summary>
        public int RankedMatches
        {
            get => _rankedMatches;
            set
            {
                _rankedMatches = value;
                RaisePropertyChanged(); // 通知 RankedMatches
            }
        }

        private int _endlessTrialMatches;

        /// <summary>
        /// 无尽试炼场次
        /// </summary>
        public int EndlessTrialMatches
        {
            get => _endlessTrialMatches;
            set
            {
                _endlessTrialMatches = value;
                RaisePropertyChanged(); // 通知 EndlessTrialMatches
            }
        }

        private int _quickMatchMatches;

        /// <summary>
        /// 快速比赛场次
        /// </summary>
        public int QuickMatchMatches
        {
            get => _quickMatchMatches;
            set
            {
                _quickMatchMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _darkDomainMatches;

        /// <summary>
        /// 暗域狂潮场次
        /// </summary>
        public int DarkDomainMatches
        {
            get => _darkDomainMatches;
            set
            {
                _darkDomainMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _pvEMatches;

        /// <summary>
        /// 人机试炼场次
        /// </summary>
        public int PvEMatches
        {
            get => _pvEMatches;
            set
            {
                _pvEMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _leyLineWarMatches;

        /// <summary>
        /// 地脉之战场次
        /// </summary>
        public int LeyLineWarMatches
        {
            get => _leyLineWarMatches;
            set
            {
                _leyLineWarMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _journeyToGodMatches;

        /// <summary>
        /// 征神之路场次
        /// </summary>
        public int JourneyToGodMatches
        {
            get => _journeyToGodMatches;
            set
            {
                _journeyToGodMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _hexagramCalamityMatches;

        /// <summary>
        /// 六芒劫场次
        /// </summary>
        public int HexagramCalamityMatches
        {
            get => _hexagramCalamityMatches;
            set
            {
                _hexagramCalamityMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _infiniteEscortMatches;

        /// <summary>
        /// 无间镖客场次
        /// </summary>
        public int InfiniteEscortMatches
        {
            get => _infiniteEscortMatches;
            set
            {
                _infiniteEscortMatches = value;
                RaisePropertyChanged();
            }
        }

        private int _fourFiendsCalamityMatches;

        /// <summary>
        /// 四煞劫场次
        /// </summary>
        public int FourFiendsCalamityMatches
        {
            get => _fourFiendsCalamityMatches;
            set
            {
                _fourFiendsCalamityMatches = value;
                RaisePropertyChanged();
            }
        }
    }
}
