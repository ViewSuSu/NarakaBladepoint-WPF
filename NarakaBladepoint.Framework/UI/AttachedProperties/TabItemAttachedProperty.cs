using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// TabItem 附加属性
    /// 
    /// 用途：为 TabItem 的选中状态变化添加命令支持
    /// 
    /// 内存安全：使用 WeakEventManager 弱引用事件管理
    /// </summary>
    public static class TabItemAttachedProperty
    {
        #region Dependency Properties

        /// <summary>
        /// 标记是否已附加弱事件监听器
        /// </summary>
        private static readonly DependencyProperty WeakEventAttachedProperty =
            DependencyProperty.RegisterAttached(
                "WeakEventAttached",
                typeof(bool),
                typeof(TabItemAttachedProperty),
                new PropertyMetadata(false)
            );

        private static bool GetWeakEventAttached(DependencyObject obj) =>
            (bool)obj.GetValue(WeakEventAttachedProperty);

        private static void SetWeakEventAttached(DependencyObject obj, bool value) =>
            obj.SetValue(WeakEventAttachedProperty, value);

        #endregion

        #region SelectedCommand 附加属性

        public static readonly DependencyProperty SelectedCommandProperty =
            DependencyProperty.RegisterAttached(
                "SelectedCommand",
                typeof(ICommand),
                typeof(TabItemAttachedProperty),
                new PropertyMetadata(null, OnSelectedCommandChanged)
            );

        public static ICommand GetSelectedCommand(TabItem tabItem)
        {
            return (ICommand)tabItem.GetValue(SelectedCommandProperty);
        }

        public static void SetSelectedCommand(TabItem tabItem, ICommand value)
        {
            tabItem.SetValue(SelectedCommandProperty, value);
        }

        private static void OnSelectedCommandChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is TabItem tabItem && e.NewValue != null)
            {
                // 附加弱事件监听器（只需一次）
                if (!GetWeakEventAttached(tabItem))
                {
                    SetWeakEventAttached(tabItem, true);
                    AttachWeakEventListeners(tabItem);
                }

                // 立即检查当前状态
                if (tabItem.IsSelected)
                {
                    ExecuteCommand(tabItem);
                }
            }
        }

        private static void AttachWeakEventListeners(TabItem tabItem)
        {
            // 使用弱引用监听 Loaded 事件
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(
                tabItem,
                nameof(FrameworkElement.Loaded),
                OnTabItemLoaded
            );

            // 使用弱引用监听 LayoutUpdated 事件（IsSelected 变化时会触发）
            WeakEventManager<UIElement, EventArgs>.AddHandler(
                tabItem,
                nameof(UIElement.LayoutUpdated),
                OnTabItemLayoutUpdated
            );
        }

        private static void OnTabItemLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem tabItem && tabItem.IsSelected)
            {
                ExecuteCommand(tabItem);
            }
        }

        private static void OnTabItemLayoutUpdated(object sender, EventArgs e)
        {
            if (sender is TabItem tabItem && tabItem.IsSelected)
            {
                ExecuteCommand(tabItem);
            }
        }

        private static void ExecuteCommand(TabItem tabItem)
        {
            var command = GetSelectedCommand(tabItem);
            var parameter = GetSelectedCommandParameter(tabItem);

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }

        #endregion SelectedCommand 附加属性

        #region SelectedCommandParameter 附加属性

        public static readonly DependencyProperty SelectedCommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "SelectedCommandParameter",
                typeof(object),
                typeof(TabItemAttachedProperty),
                new PropertyMetadata(null)
            );

        public static object GetSelectedCommandParameter(TabItem tabItem)
        {
            return tabItem.GetValue(SelectedCommandParameterProperty);
        }

        public static void SetSelectedCommandParameter(TabItem tabItem, object value)
        {
            tabItem.SetValue(SelectedCommandParameterProperty, value);
        }

        #endregion SelectedCommandParameter 附加属性
    }
}
