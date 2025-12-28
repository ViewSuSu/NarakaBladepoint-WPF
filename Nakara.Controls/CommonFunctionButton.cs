using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Nakara.Controls
{
    public class CommonFunctionButton : Control
    {
        static CommonFunctionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CommonFunctionButton),
                new FrameworkPropertyMetadata(typeof(CommonFunctionButton))
            );
        }

        #region IsSelected

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            nameof(IsSelected),
            typeof(bool),
            typeof(CommonFunctionButton),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsSelectedChanged
            )
        );

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly RoutedEvent IsSelectedChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(IsSelectedChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<bool>),
                typeof(CommonFunctionButton)
            );

        public event RoutedPropertyChangedEventHandler<bool> IsSelectedChanged
        {
            add => AddHandler(IsSelectedChangedEvent, value);
            remove => RemoveHandler(IsSelectedChangedEvent, value);
        }

        private static void OnIsSelectedChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var control = (CommonFunctionButton)d;
            control.OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            var args = new RoutedPropertyChangedEventArgs<bool>(
                oldValue,
                newValue,
                IsSelectedChangedEvent
            );

            RaiseEvent(args);
        }

        #endregion

        #region Content

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(string),
            typeof(CommonFunctionButton),
            new PropertyMetadata(string.Empty)
        );

        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        #endregion

        #region Command

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(CommonFunctionButton)
        );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                nameof(CommandParameter),
                typeof(object),
                typeof(CommonFunctionButton)
            );

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region Input Handling

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (!IsEnabled)
                return;

            if (Command?.CanExecute(CommandParameter) == true)
            {
                Command.Execute(CommandParameter);
            }

            e.Handled = true;
        }

        #endregion
    }
}
