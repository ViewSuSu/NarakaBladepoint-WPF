namespace NarakaBladepoint.Shared.Datas
{
    /// <summary>
    /// 单个技能点数据
    /// </summary>
    public class SkillPointData
    {
        /// <summary>
        /// 技能点索引
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName { get; set; }

        /// <summary>
        /// 圆形级别（1, 2, 或 3）
        /// </summary>
        public int CircleLevel { get; set; }

        /// <summary>
        /// 位置（LeftUp, LeftDown, RightUp, RightDown）
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 方向（TenOClock 或 ElevenOClock）
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 当前已学习的等级
        /// </summary>
        public int CurrentLearned { get; set; }

        /// <summary>
        /// 最大可学习等级
        /// </summary>
        public int TotalLearnable { get; set; }

        /// <summary>
        /// 是否可学习
        /// </summary>
        public bool IsLearnable { get; set; }
    }

    /// <summary>
    /// 天赋技能点配置（对应一个天赋的所有技能点）
    /// </summary>
    public class TianfuSkillPointData
    {
        /// <summary>
        /// 天赋名称（tianfu1, tianfu2, tianfu3）
        /// </summary>
        public string TianfuName { get; set; }

        /// <summary>
        /// 剩余可分配的技能点数
        /// </summary>
        public int RemainingPoints { get; set; }

        /// <summary>
        /// 所有20个技能点的数据
        /// </summary>
        public List<SkillPointData> SkillPoints { get; set; } = new List<SkillPointData>();
    }
}
