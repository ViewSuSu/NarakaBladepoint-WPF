namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Models
{
    /// <summary>
    /// 技能点布局配置 - 所有坐标都是相对的
    /// </summary>
    public static class SkillPointLayoutConfig
    {
        // 画布中心（相对于1280x720的标准分辨率）
        public const double CENTER_X_RATIO = 0.5;  // 50% of canvas width
        public const double CENTER_Y_RATIO = 0.5;  // 50% of canvas height

        // 圆的参数
        public const double INNER_RADIUS = 150;    // 第一圈半径
        public const double MIDDLE_RADIUS = 250;   // 第二圈半径
        public const double OUTER_RADIUS = 350;    // 第三圈半径

        // 角度定义（相对于12点钟方向，顺时针）
        // 左上角
        public const double ANGLE_LU = 210;        // 睹 - 10点钟方向
        public const double ANGLE_MA = 210;        // 马 - 10点钟方向
        public const double ANGLE_HE = 240;        // 合 - 11点钟方向
        public const double ANGLE_OU = 240;        // 噢 - 11点钟方向
        public const double ANGLE_XIONG = 210;     // 凶 - 10点钟方向
        public const double ANGLE_SHI = 240;       // 市 - 11点钟方向

        // 技能点大小
        public const double SKILL_POINT_SIZE = 50;
        public const double CENTER_CIRCLE_SIZE = 80;
    }
}
