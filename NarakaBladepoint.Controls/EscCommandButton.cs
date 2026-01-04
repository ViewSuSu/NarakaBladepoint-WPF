using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NarakaBladepoint.Controls
{
    public class EscBackControl : Control
    {
        static EscBackControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(EscBackControl),
                new FrameworkPropertyMetadata(typeof(EscBackControl))
            );
        }

        public EscBackControl()
        {
            Focusable = true;

            MouseLeftButtonUp += OnMouseLeftButtonUp;
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            if (Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }
        }

        #region Command

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(EscBackControl)
        );

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(EscBackControl)
            );

        #endregion Command

        #region EscKeyText

        public string EscKeyText
        {
            get => (string)GetValue(EscKeyTextProperty);
            set => SetValue(EscKeyTextProperty, value);
        }

        public static readonly DependencyProperty EscKeyTextProperty = DependencyProperty.Register(
            nameof(EscKeyText),
            typeof(string),
            typeof(EscBackControl),
            new PropertyMetadata("Esc")
        );

        #endregion EscKeyText

        #region BackText

        public string BackText
        {
            get => (string)GetValue(BackTextProperty);
            set => SetValue(BackTextProperty, value);
        }

        public static readonly DependencyProperty BackTextProperty = DependencyProperty.Register(
            nameof(BackText),
            typeof(string),
            typeof(EscBackControl),
            new PropertyMetadata("返回")
        ); // 设置默认值
        #endregion BackText
    }
}
