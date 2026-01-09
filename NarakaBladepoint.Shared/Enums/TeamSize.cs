using System.ComponentModel;
using NarakaBladepoint.Shared.Consts;

namespace NarakaBladepoint.Shared.Enums
{
    public enum TeamSize
    {
        /// <summary>
        /// 单排
        /// </summary>
        [Description(TeamSizeConstant.Solo)]
        Solo = 1,

        /// <summary>
        /// 双排
        /// </summary>
        [Description(TeamSizeConstant.Duo)]
        Duo = 2,

        /// <summary>
        /// 三排
        /// </summary>
        [Description(TeamSizeConstant.Trio)]
        Trio = 3,
    }
}
