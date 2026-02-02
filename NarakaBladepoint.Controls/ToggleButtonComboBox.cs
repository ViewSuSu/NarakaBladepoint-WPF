using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    /// <summary>
    /// 支持自动高亮选中项的自定义ComboBox控件
    /// </summary>
    public class ToggleButtonComboBox : ComboBox
    {
        #region 静态构造函数

        static ToggleButtonComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ToggleButtonComboBox),
                new FrameworkPropertyMetadata(typeof(ToggleButtonComboBox))
            );
        }

        #endregion 静态构造函数

        #region 依赖属性

        /// <summary>
        /// 高亮背景依赖属性
        /// </summary>
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register(
                "HighlightBackground",
                typeof(Brush),
                typeof(ToggleButtonComboBox),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// 获取或设置高亮项的背景
        /// </summary>
        public Brush HighlightBackground
        {
            get => (Brush)GetValue(HighlightBackgroundProperty);
            set => SetValue(HighlightBackgroundProperty, value);
        }

        #endregion 依赖属性

        #region 重写方法

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ToggleButtonComboBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ToggleButtonComboBoxItem;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            // 通知所有项容器重新评估高亮状态
            RefreshHighlightState();
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            base.OnDropDownOpened(e);

            // 当下拉框打开时，刷新高亮状态
            RefreshHighlightState();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);

            // 当下拉框关闭时，清除所有高亮
            ClearAllHighlights();
        }

        protected override void PrepareContainerForItemOverride(
            DependencyObject element,
            object item
        )
        {
            base.PrepareContainerForItemOverride(element, item);

            if (element is ToggleButtonComboBoxItem containerItem)
            {
                // 设置父ComboBox引用
                containerItem.ParentComboBox = this;

                // 设置项的高度与ComboBox的高度相同
                if (!double.IsNaN(this.Height) && this.Height > 0)
                {
                    containerItem.Height = this.Height;
                }
            }
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            if (element is ToggleButtonComboBoxItem containerItem)
            {
                // 清理父ComboBox引用
                containerItem.ParentComboBox = null;
            }

            base.ClearContainerForItemOverride(element, item);
        }

        #endregion 重写方法

        #region 私有方法

        /// <summary>
        /// 刷新所有项的高亮状态
        /// </summary>
        private void RefreshHighlightState()
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                // 遍历所有已生成的容器
                for (int i = 0; i < Items.Count; i++)
                {
                    var container =
                        ItemContainerGenerator.ContainerFromIndex(i) as ToggleButtonComboBoxItem;
                    if (container != null)
                    {
                        // 更新高亮状态：选中的项并且下拉框打开时高亮
                        container.IsHighlighted = (IsDropDownOpen && i == SelectedIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有高亮状态
        /// </summary>
        private void ClearAllHighlights()
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                // 遍历所有已生成的容器
                for (int i = 0; i < Items.Count; i++)
                {
                    var container =
                        ItemContainerGenerator.ContainerFromIndex(i) as ToggleButtonComboBoxItem;
                    if (container != null)
                    {
                        container.IsHighlighted = false;
                    }
                }
            }
        }

        #endregion 私有方法
    }

    /// <summary>
    /// ToggleButtonComboBox的项容器
    /// </summary>
    public class ToggleButtonComboBoxItem : ComboBoxItem
    {
        static ToggleButtonComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ToggleButtonComboBoxItem),
                new FrameworkPropertyMetadata(typeof(ToggleButtonComboBoxItem))
            );
        }

        #region 私有字段

        private ToggleButtonComboBox _parentComboBox;

        #endregion 私有字段

        #region 属性

        /// <summary>
        /// 获取或设置父级ToggleButtonComboBox
        /// </summary>
        public ToggleButtonComboBox ParentComboBox
        {
            get => _parentComboBox;
            set
            {
                if (_parentComboBox != value)
                {
                    _parentComboBox = value;
                }
            }
        }

        #endregion 属性

        #region IsHighlighted 依赖属性

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(
                "IsHighlighted",
                typeof(bool),
                typeof(ToggleButtonComboBoxItem),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender)
            );

        /// <summary>
        /// 获取或设置是否高亮显示此项
        /// </summary>
        public bool IsHighlighted
        {
            get => (bool)GetValue(IsHighlightedProperty);
            set => SetValue(IsHighlightedProperty, value);
        }

        #endregion IsHighlighted 依赖属性
    }
}
