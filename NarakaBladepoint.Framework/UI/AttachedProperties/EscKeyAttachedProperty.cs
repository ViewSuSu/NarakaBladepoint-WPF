using System.Windows;
using System.Windows.Input;

namespace NarakaBladepoint.Framework.UI.AttachedProperties
{
    /// <summary>
    /// Esc键附加属性（最简单实现）
    /// </summary>
    public static class EscKeyAttachedProperty
    {
        #region 附加属性

        public static ICommand GetCommand(DependencyObject obj) =>
            (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value) =>
            obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(EscKeyAttachedProperty),
                new PropertyMetadata(null, OnCommandChanged)
            );

        public static object GetCommandParameter(DependencyObject obj) =>
            obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value) =>
            obj.SetValue(CommandParameterProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(EscKeyAttachedProperty),
                new PropertyMetadata(null)
            );

        #endregion

        #region 实现

        private static void OnCommandChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is UIElement element)
            {
                // 创建或更新InputBinding
                var command = e.NewValue as ICommand;
                if (command != null)
                {
                    // 为ESC键创建快捷键
                    var gesture = new KeyGesture(Key.Escape);
                    var binding = new InputBinding(command, gesture)
                    {
                        CommandParameter = GetCommandParameter(element),
                    };

                    element.InputBindings.Add(binding);
                }
            }
        }

        #endregion
    }
}
