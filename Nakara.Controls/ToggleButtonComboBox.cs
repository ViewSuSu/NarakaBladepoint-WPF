using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Nakara.Controls
{
    /// <summary>
    /// 支持高亮显示特定项的自定义ComboBox控件
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
        /// 高亮索引依赖属性
        /// </summary>
        public static readonly DependencyProperty HighlightIndexProperty =
            DependencyProperty.Register(
                "HighlightIndex",
                typeof(int),
                typeof(ToggleButtonComboBox),
                new FrameworkPropertyMetadata(
                    -1,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    OnHighlightIndexChanged
                )
            );

        /// <summary>
        /// 获取或设置要高亮显示的项的索引
        /// </summary>
        public int HighlightIndex
        {
            get => (int)GetValue(HighlightIndexProperty);
            set => SetValue(HighlightIndexProperty, value);
        }

        /// <summary>
        /// 高亮背景依赖属性
        /// </summary>
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register(
                "HighlightBackground",
                typeof(Brush),
                typeof(ToggleButtonComboBox),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    OnHighlightBackgroundChanged
                )
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

        #region 私有方法

        private static void OnHighlightIndexChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var control = d as ToggleButtonComboBox;
            if (control != null)
            {
                control.OnHighlightIndexChanged();
            }
        }

        private static void OnHighlightBackgroundChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var control = d as ToggleButtonComboBox;
            if (control != null)
            {
                control.OnHighlightBackgroundChanged();
            }
        }

        private void OnHighlightIndexChanged()
        {
            // 通知所有项容器重新评估高亮状态
            RefreshHighlightState();
        }

        private void OnHighlightBackgroundChanged()
        {
            // 通知所有项容器重新评估高亮状态
            RefreshHighlightState();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ToggleButtonComboBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ToggleButtonComboBoxItem;
        }

        protected override void PrepareContainerForItemOverride(
            DependencyObject element,
            object item
        )
        {
            base.PrepareContainerForItemOverride(element, item);

            if (element is ToggleButtonComboBoxItem containerItem)
            {
                containerItem.UpdateHighlight();
            }
        }

        protected override void OnItemsChanged(
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e
        )
        {
            base.OnItemsChanged(e);

            // 当项集合改变时，需要更新高亮状态
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                RefreshHighlightState();
            }
        }

        /// <summary>
        /// 刷新所有项的高亮状态
        /// </summary>
        private void RefreshHighlightState()
        {
            if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                // 遍历所有已生成的容器
                foreach (object item in Items)
                {
                    var container =
                        ItemContainerGenerator.ContainerFromItem(item) as ToggleButtonComboBoxItem;
                    container?.UpdateHighlight();
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

        /// <summary>
        /// 获取父级ToggleButtonComboBox
        /// </summary>
        private ToggleButtonComboBox ParentComboBox =>
            ItemsControl.ItemsControlFromItemContainer(this) as ToggleButtonComboBox;

        /// <summary>
        /// 更新高亮状态
        /// </summary>
        public void UpdateHighlight()
        {
            var parent = ParentComboBox;
            if (parent != null)
            {
                try
                {
                    // 获取当前项在ComboBox中的索引
                    var index = parent.ItemContainerGenerator.IndexFromContainer(this);

                    // 检查是否是高亮项
                    IsHighlighted = index >= 0 && index == parent.HighlightIndex;
                }
                catch
                {
                    IsHighlighted = false;
                }
            }
        }

        #region IsHighlighted 依赖属性

        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(
                "IsHighlighted",
                typeof(bool),
                typeof(ToggleButtonComboBoxItem),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender)
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
