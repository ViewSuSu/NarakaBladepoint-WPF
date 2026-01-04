using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    public class ImageTextBlock : Control
    {
        static ImageTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ImageTextBlock),
                new FrameworkPropertyMetadata(typeof(ImageTextBlock))
            );
        }

        /// <summary>
        /// 图片源
        /// </summary>
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            nameof(Source),
            typeof(ImageSource),
            typeof(ImageTextBlock),
            new PropertyMetadata(null)
        );

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ImageTextBlock),
            new PropertyMetadata(string.Empty)
        );

        /// <summary>
        /// 鼠标左键点击命令
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(ImageTextBlock),
                new PropertyMetadata(null)
            );

        /// <summary>
        /// 鼠标左键事件绑定
        /// </summary>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(null);
            }
        }
    }
}
