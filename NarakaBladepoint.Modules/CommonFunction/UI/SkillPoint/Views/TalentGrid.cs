using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views
{
    /// <summary>
    /// 天赋/技能/奥义选择Grid控件
    /// </summary>
    public class TalentGrid : Grid
    {
        public TalentGrid()
        {
            this.MouseLeftButtonDown += TalentGrid_MouseLeftButtonDown;
        }

        /// <summary>
        /// IsSelected 依赖属性
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(TalentGrid),
                new PropertyMetadata(false));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 处理点击事件
        /// </summary>
        private void TalentGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 获取父容器
            var parent = VisualTreeHelper.GetParent(this);
            if (parent is Panel parentPanel)
            {
                // 获取父容器中的所有子TalentGrid
                var childCount = VisualTreeHelper.GetChildrenCount(parentPanel);
                for (int i = 0; i < childCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parentPanel, i);
                    if (child is TalentGrid talentGrid)
                    {
                        // 更新IsSelected状态
                        talentGrid.IsSelected = (talentGrid == this);
                    }
                }
            }

            e.Handled = true;
        }
    }
}
