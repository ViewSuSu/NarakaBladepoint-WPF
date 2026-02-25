using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace NarakaBladepoint.Modules.CommonFunction.UI.SkillPoint.Controls
{
    /// <summary>
    /// 自定义Border控件，支持选中时显示内偏移白色边框
    /// </summary>
    public class SelectiveBorder : Border
    {
        public SelectiveBorder()
        {
            this.Loaded += SelectiveBorder_Loaded;
        }

        /// <summary>
        /// 是否显示白色边框
        /// </summary>
        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(
                nameof(IsHighlighted),
                typeof(bool),
                typeof(SelectiveBorder),
                new PropertyMetadata(false, OnIsHighlightedChanged));

        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
        }

        /// <summary>
        /// 白色边框的宽度（内偏移）
        /// </summary>
        public static readonly DependencyProperty HighlightBorderThicknessProperty =
            DependencyProperty.Register(
                nameof(HighlightBorderThickness),
                typeof(double),
                typeof(SelectiveBorder),
                new PropertyMetadata(1.0, OnHighlightBorderThicknessChanged));

        public double HighlightBorderThickness
        {
            get { return (double)GetValue(HighlightBorderThicknessProperty); }
            set { SetValue(HighlightBorderThicknessProperty, value); }
        }

        /// <summary>
        /// 白色边框的颜色
        /// </summary>
        public static readonly DependencyProperty HighlightBorderBrushProperty =
            DependencyProperty.Register(
                nameof(HighlightBorderBrush),
                typeof(Brush),
                typeof(SelectiveBorder),
                new PropertyMetadata(Brushes.White, OnHighlightBorderBrushChanged));

        public Brush HighlightBorderBrush
        {
            get { return (Brush)GetValue(HighlightBorderBrushProperty); }
            set { SetValue(HighlightBorderBrushProperty, value); }
        }

        private static void OnIsHighlightedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SelectiveBorder)d).InvalidateVisual();
        }

        private static void OnHighlightBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SelectiveBorder)d).InvalidateVisual();
        }

        private static void OnHighlightBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SelectiveBorder)d).InvalidateVisual();
        }

        /// <summary>
        /// Loaded事件，用于订阅子元素的IsSelected属性变化
        /// </summary>
        private void SelectiveBorder_Loaded(object sender, RoutedEventArgs e)
        {
            AttachPropertyChangeListeners();
            // 初始化时立即更新高亮状态
            UpdateHighlightState();
        }

        /// <summary>
        /// 附加属性变化监听器到所有子TalentGrid
        /// </summary>
        private void AttachPropertyChangeListeners()
        {
            var talentGrids = FindAllTalentGrids(this);
            foreach (var grid in talentGrids)
            {
                var descriptor = DependencyPropertyDescriptor.FromProperty(
                    TalentGrid.IsSelectedProperty,
                    typeof(TalentGrid));

                if (descriptor != null)
                {
                    descriptor.AddValueChanged(grid, TalentGrid_IsSelectedChanged);
                }
            }
        }

        /// <summary>
        /// TalentGrid的IsSelected属性变化事件处理
        /// </summary>
        private void TalentGrid_IsSelectedChanged(object sender, System.EventArgs e)
        {
            UpdateHighlightState();
        }

        /// <summary>
        /// 更新Border的高亮状态
        /// </summary>
        private void UpdateHighlightState()
        {
            var talentGrids = FindAllTalentGrids(this);
            bool anySelected = false;

            foreach (var grid in talentGrids)
            {
                if (grid.IsSelected)
                {
                    anySelected = true;
                    break;
                }
            }

            // 如果这个Border需要高亮
            if (anySelected)
            {
                this.IsHighlighted = true;
                // 取消其他兄弟Border的高亮
                CancelSiblingHighlight();
            }
            else
            {
                this.IsHighlighted = false;
            }
        }

        /// <summary>
        /// 取消兄弟Border的高亮（互斥逻辑）
        /// </summary>
        private void CancelSiblingHighlight()
        {
            var parent = VisualTreeHelper.GetParent(this);
            if (parent != null)
            {
                var childCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is SelectiveBorder sibling && sibling != this)
                    {
                        sibling.IsHighlighted = false;
                    }
                }
            }
        }

        /// <summary>
        /// 在树中查找所有TalentGrid控件
        /// </summary>
        private static System.Collections.Generic.List<TalentGrid> FindAllTalentGrids(DependencyObject parent)
        {
            var talentGrids = new System.Collections.Generic.List<TalentGrid>();
            FindTalentGridsRecursive(parent, talentGrids);
            return talentGrids;
        }

        /// <summary>
        /// 递归查找TalentGrid
        /// </summary>
        private static void FindTalentGridsRecursive(DependencyObject parent, System.Collections.Generic.List<TalentGrid> list)
        {
            var childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TalentGrid talentGrid)
                {
                    list.Add(talentGrid);
                }

                // 继续递归查找
                FindTalentGridsRecursive(child, list);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // 如果不显示高亮，直接返回
            if (!IsHighlighted)
                return;

            // 获取Border的实际大小
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            double thickness = HighlightBorderThickness;

            // 创建内偏移的矩形
            var rect = new Rect(
                thickness,
                thickness,
                width - thickness * 2,
                height - thickness * 2);

            // 绘制白色边框（朝内偏移）
            drawingContext.DrawRectangle(
                null,
                new Pen(HighlightBorderBrush, thickness),
                rect);
        }
    }
}
