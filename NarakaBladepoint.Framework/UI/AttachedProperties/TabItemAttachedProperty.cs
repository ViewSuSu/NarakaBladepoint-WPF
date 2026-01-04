using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    public static class TabItemAttachedProperty
    {
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
            if (d is TabItem tabItem)
            {
                // 清理旧的事件处理
                CleanUp(tabItem);

                // 设置新的事件处理
                if (e.NewValue != null)
                {
                    Setup(tabItem);
                }
            }
        }

        private static void Setup(TabItem tabItem)
        {
            // 监听IsSelected属性变化
            var binding = new Binding("IsSelected") { Source = tabItem, Mode = BindingMode.OneWay };

            // 使用DependencyPropertyDescriptor来监听属性变化
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(
                TabItem.IsSelectedProperty,
                typeof(TabItem)
            );

            if (descriptor != null)
            {
                descriptor.AddValueChanged(tabItem, OnIsSelectedChanged);
            }

            // 保存引用以便清理
            tabItem.Tag = descriptor;

            // 立即检查当前状态
            if (tabItem.IsSelected)
            {
                ExecuteCommand(tabItem);
            }
        }

        private static void CleanUp(TabItem tabItem)
        {
            if (tabItem.Tag is System.ComponentModel.DependencyPropertyDescriptor descriptor)
            {
                descriptor.RemoveValueChanged(tabItem, OnIsSelectedChanged);
                tabItem.Tag = null;
            }
        }

        private static void OnIsSelectedChanged(object sender, System.EventArgs e)
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

        #region CleanupOnUnload 附加属性（可选）

        public static readonly DependencyProperty CleanupOnUnloadProperty =
            DependencyProperty.RegisterAttached(
                "CleanupOnUnload",
                typeof(bool),
                typeof(TabItemAttachedProperty),
                new PropertyMetadata(true, OnCleanupOnUnloadChanged)
            );

        public static bool GetCleanupOnUnload(TabItem tabItem)
        {
            return (bool)tabItem.GetValue(CleanupOnUnloadProperty);
        }

        public static void SetCleanupOnUnload(TabItem tabItem, bool value)
        {
            tabItem.SetValue(CleanupOnUnloadProperty, value);
        }

        private static void OnCleanupOnUnloadChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is TabItem tabItem)
            {
                if ((bool)e.NewValue)
                {
                    tabItem.Unloaded += OnTabItemUnloaded;
                }
                else
                {
                    tabItem.Unloaded -= OnTabItemUnloaded;
                }
            }
        }

        private static void OnTabItemUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is TabItem tabItem)
            {
                CleanUp(tabItem);
                tabItem.Unloaded -= OnTabItemUnloaded;
            }
        }

        #endregion CleanupOnUnload 附加属性（可选）
    }
}
