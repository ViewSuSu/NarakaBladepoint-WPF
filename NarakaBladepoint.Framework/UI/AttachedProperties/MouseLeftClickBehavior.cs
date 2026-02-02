using System.Windows;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// 处理鼠标左键点击的附加行为
    /// </summary>
    public static class MouseLeftClickBehavior
    {
        /// <summary>
        /// 点击命令
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseLeftClickBehavior),
                new PropertyMetadata(null, OnCommandChanged)
            );

        /// <summary>
        /// 命令参数
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(MouseLeftClickBehavior),
                new PropertyMetadata(null)
            );

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static object GetCommandParameter(DependencyObject obj) =>
            obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value) =>
            obj.SetValue(CommandParameterProperty, value);

        private static void OnCommandChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is FrameworkElement element)
            {
                if (e.NewValue != null)
                {
                    WeakEventManager<FrameworkElement, MouseButtonEventArgs>.AddHandler(
                        element,
                        "MouseLeftButtonDown",
                        OnMouseLeftButtonDown
                    );
                }
                else
                {
                    WeakEventManager<FrameworkElement, MouseButtonEventArgs>.RemoveHandler(
                        element,
                        "MouseLeftButtonDown",
                        OnMouseLeftButtonDown
                    );
                }
            }
        }

        private static void OnMouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e
        )
        {
            if (sender is FrameworkElement element)
            {
                var command = GetCommand(element);
                var parameter = GetCommandParameter(element);

                if (command?.CanExecute(parameter) == true)
                {
                    command.Execute(parameter);
                }
            }
        }
    }
}
