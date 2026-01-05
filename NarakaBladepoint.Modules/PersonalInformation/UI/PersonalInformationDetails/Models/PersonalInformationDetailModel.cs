using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Shared.Services.Models;

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
                if (SetProperty(ref _seasonType, value))
                {
                    RaisePropertyChanged(nameof(Season));
                }
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
            set => SetProperty(ref _gameTime, value);
        }

        private int _totalMatches;

        /// <summary>
        /// 总场次
        /// </summary>
        public int TotalMatches
        {
            get => _totalMatches;
            set => SetProperty(ref _totalMatches, value);
        }

        private int _totalDefeats;

        /// <summary>
        /// 总击败
        /// </summary>
        public int TotalDefeats
        {
            get => _totalDefeats;
            set => SetProperty(ref _totalDefeats, value);
        }

        private int _rankedMatches;

        /// <summary>
        /// 排位赛场次
        /// </summary>
        public int RankedMatches
        {
            get => _rankedMatches;
            set => SetProperty(ref _rankedMatches, value);
        }

        private int _endlessTrialMatches;

        /// <summary>
        /// 无尽试炼场次
        /// </summary>
        public int EndlessTrialMatches
        {
            get => _endlessTrialMatches;
            set => SetProperty(ref _endlessTrialMatches, value);
        }

        private int _quickMatchMatches;

        /// <summary>
        /// 快速比赛场次
        /// </summary>
        public int QuickMatchMatches
        {
            get => _quickMatchMatches;
            set => SetProperty(ref _quickMatchMatches, value);
        }

        private int _darkDomainMatches;

        /// <summary>
        /// 暗域狂潮场次
        /// </summary>
        public int DarkDomainMatches
        {
            get => _darkDomainMatches;
            set => SetProperty(ref _darkDomainMatches, value);
        }

        private int _pvEMatches;

        /// <summary>
        /// 人机试炼场次
        /// </summary>
        public int PvEMatches
        {
            get => _pvEMatches;
            set => SetProperty(ref _pvEMatches, value);
        }

        private int _leyLineWarMatches;

        /// <summary>
        /// 地脉之战场次
        /// </summary>
        public int LeyLineWarMatches
        {
            get => _leyLineWarMatches;
            set => SetProperty(ref _leyLineWarMatches, value);
        }

        private int _journeyToGodMatches;

        /// <summary>
        /// 征神之路场次
        /// </summary>
        public int JourneyToGodMatches
        {
            get => _journeyToGodMatches;
            set => SetProperty(ref _journeyToGodMatches, value);
        }

        private int _hexagramCalamityMatches;

        /// <summary>
        /// 六芒劫场次
        /// </summary>
        public int HexagramCalamityMatches
        {
            get => _hexagramCalamityMatches;
            set => SetProperty(ref _hexagramCalamityMatches, value);
        }

        private int _infiniteEscortMatches;

        /// <summary>
        /// 无间镖客场次
        /// </summary>
        public int InfiniteEscortMatches
        {
            get => _infiniteEscortMatches;
            set => SetProperty(ref _infiniteEscortMatches, value);
        }

        private int _fourFiendsCalamityMatches;

        /// <summary>
        /// 四煞劫场次
        /// </summary>
        public int FourFiendsCalamityMatches
        {
            get => _fourFiendsCalamityMatches;
            set => SetProperty(ref _fourFiendsCalamityMatches, value);
        }
    }
}
