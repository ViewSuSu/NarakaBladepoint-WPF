using System.ComponentModel;

namespace NarakaBladepoint.Shared.Enums
{
    public enum SeasonType
    {
        [Description("所有赛季")]
        All,

        [Description("当前赛季")]
        Current,

        [Description("第18赛季：侠风")]
        Last,

        [Description("第17赛季：裂变")]
        TheOneBeforeLast,
    }
}
