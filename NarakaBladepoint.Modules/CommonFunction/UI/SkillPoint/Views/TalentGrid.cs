using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Views
{
    /// <summary>
    /// 天赋/技能/奥义选择Grid控件
    /// </summary>
    public class TalentGrid : Grid
    {
        // IsSelectedChanged 事件
        public event EventHandler IsSelectedChanged;

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
                new PropertyMetadata(false, OnIsSelectedChanged));

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TalentGrid talentGrid && (bool)e.NewValue == true)
            {
                talentGrid.IsSelectedChanged?.Invoke(talentGrid, EventArgs.Empty);
            }
        }

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
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(this);
            if (parent is Panel parentPanel)
            {
                // 获取父容器中的所有子TalentGrid
                var childCount = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parentPanel);
                for (int i = 0; i < childCount; i++)
                {
                    var child = System.Windows.Media.VisualTreeHelper.GetChild(parentPanel, i);
                    if (child is TalentGrid talentGrid)
                    {
                        if (talentGrid == this)
                        {
                            // 如果再次点击已选中的Grid，先设置为false再设置为true
                            // 这样可以触发属性变化事件，更新Border的高亮状态
                            if (talentGrid.IsSelected)
                            {
                                talentGrid.IsSelected = false;
                                talentGrid.IsSelected = true;
                            }
                            else
                            {
                                talentGrid.IsSelected = true;
                            }
                        }
                        else
                        {
                            // 其他Grid设置为false
                            talentGrid.IsSelected = false;
                        }
                    }
                }
            }

            e.Handled = true;
        }
    }
}
