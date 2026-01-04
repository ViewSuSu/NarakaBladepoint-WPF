using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Models
{
    internal class SkillPointItemModel : BindableBase
    {
        /// <summary>
        /// 天赋点名字
        /// </summary>
        public string Name { get; set; }

        private bool _isTaked;

        /// <summary>
        /// 是否点了该天赋
        /// </summary>
        public bool IsTaked
        {
            get { return _isTaked; }
            set
            {
                _isTaked = value;
                SetProperty(ref _isTaked, value);
            }
        }

        /// <summary>
        /// 当前点数
        /// </summary>
        public int CurrentCount { get; set; }

        /// <summary>
        /// 总点数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 天赋图标
        /// </summary>
        public ImageSource Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
