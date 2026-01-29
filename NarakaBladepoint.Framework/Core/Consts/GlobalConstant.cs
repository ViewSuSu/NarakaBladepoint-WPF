namespace NarakaBladepoint.Framework.Core.Consts
{
    public static class GlobalConstant
    {
        /// <summary>
        /// 首页视觉导航区域 - 用于堆叠式界面层（弹窗、详情页等）
        /// 支持 4 层堆叠，从下至上依次为 Region1 -> Region2 -> Region3 -> Region4
        /// </summary>
        public const string HomePageRegion1 = nameof(HomePageRegion1);

        public const string HomePageRegion2 = nameof(HomePageRegion2);
        public const string HomePageRegion3 = nameof(HomePageRegion3);
        public const string HomePageRegion4 = nameof(HomePageRegion4);

        /// <summary>
        /// 自动首页区域 - 保留区域
        /// </summary>
        public const string AutoHomePageRegion = nameof(AutoHomePageRegion);

        /// <summary>
        /// 主内容区域 - 用于显示主要内容（如列表页、详情页等主内容）
        /// 相比 HomePageRegion，MainContentRegion 用于替换整个内容区域而非堆叠显示
        /// </summary>
        public const string MainContentRegion = nameof(MainContentRegion);
    }
}
