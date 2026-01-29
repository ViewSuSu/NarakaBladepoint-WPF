using System.Reflection;

namespace NarakaBladepoint.Modules.EventCenter.Domain.EventParameters
{
    /// <summary>
    /// 导航到活动中心的参数
    /// </summary>
    internal class NavigatedToEventCenterParameter : NavigationParameters
    {
        /// <summary>
        /// 是否选中近期热点
        /// </summary>
        public bool IsSelectedLatestNews { get; set; }

        /// <summary>
        /// 是否选中限时活动
        /// </summary>
        public bool IsSelectedLimitedEvent { get; set; }

        /// <summary>
        /// 是否选中揽月阁
        /// </summary>
        public bool IsSelectedMoonGazingPavilion { get; set; }

        /// <summary>
        /// 是否选中白泽月卡
        /// </summary>
        public bool IsSelectedBaiZeCard { get; set; }

        /// <summary>
        /// 是否选中绑定有礼
        /// </summary>
        public bool IsSelectedBindReward { get; set; }

        /// <summary>
        /// 是否选中英雄归来
        /// </summary>
        public bool IsSelectedWelcomeBack { get; set; }

        /// <summary>
        /// 是否选中传火
        /// </summary>
        public bool IsSelectedPassingTheFlame { get; set; }

        /// <summary>
        /// 是否选中蓬莱指南
        /// </summary>
        public bool IsSelectedPenglaiGuide { get; set; }

        /// <summary>
        /// 是否选中更新公告
        /// </summary>
        public bool IsSelectedPatchNote { get; set; }

        /// <summary>
        /// 是否选中网易支付好礼
        /// </summary>
        public bool IsSelectedNetEasePayRewards { get; set; }

        /// <summary>
        /// 是否选中宝箱定向保底
        /// </summary>
        public bool IsSelectedTargetedChestGuarantee { get; set; }

        public NavigatedToEventCenterParameter(
            bool isSelectedLatestNews = false,
            bool isSelectedLimitedEvent = false,
            bool isSelectedMoonGazingPavilion = false,
            bool isSelectedBaiZeCard = false,
            bool isSelectedBindReward = false,
            bool isSelectedWelcomeBack = false,
            bool isSelectedPassingTheFlame = false,
            bool isSelectedPenglaiGuide = false,
            bool isSelectedPatchNote = false,
            bool isSelectedNetEasePayRewards = false,
            bool isSelectedTargetedChestGuarantee = false
        )
        {
            this.IsSelectedLatestNews = isSelectedLatestNews;
            this.IsSelectedLimitedEvent = isSelectedLimitedEvent;
            this.IsSelectedMoonGazingPavilion = isSelectedMoonGazingPavilion;
            this.IsSelectedBaiZeCard = isSelectedBaiZeCard;
            this.IsSelectedBindReward = isSelectedBindReward;
            this.IsSelectedWelcomeBack = isSelectedWelcomeBack;
            this.IsSelectedPassingTheFlame = isSelectedPassingTheFlame;
            this.IsSelectedPenglaiGuide = isSelectedPenglaiGuide;
            this.IsSelectedPatchNote = isSelectedPatchNote;
            this.IsSelectedNetEasePayRewards = isSelectedNetEasePayRewards;
            this.IsSelectedTargetedChestGuarantee = isSelectedTargetedChestGuarantee;
            AddParametersToNavigationParameters();
        }

        /// <summary>
        /// 通过反射将所有公共属性添加到导航参数字典中
        /// </summary>
        private void AddParametersToNavigationParameters()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

            foreach (var property in properties)
            {
                try
                {
                    var value = property.GetValue(this, null);
                    this.Add(property.Name, value);
                }
                catch (Exception ex)
                {
                    // 如果无法获取属性值，跳过该属性
                    System.Diagnostics.Debug.WriteLine(
                        $"无法添加属性 {property.Name} 到导航参数: {ex.Message}"
                    );
                }
            }
        }
    }
}
