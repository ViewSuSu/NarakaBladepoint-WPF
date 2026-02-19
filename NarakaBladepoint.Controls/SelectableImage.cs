using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NarakaBladepoint.Controls
{
    /// <summary>
    /// 英雄标签可选择的 Image 控件，根据 IsSelected 状态显示不同的图片
    /// </summary>
    public class HeroTagSelectableImage : Image
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(HeroTagSelectableImage),
                new PropertyMetadata(false, OnIsSelectedChanged));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HeroTagSelectableImage control)
            {
                control.UpdateSource();
            }
        }

        private void UpdateSource()
        {
            try
            {
                string resourcePath = IsSelected
                    ? "pack://application:,,,/NarakaBladepoint.Resources;component/Image/CustomControls/2.png"
                    : "pack://application:,,,/NarakaBladepoint.Resources;component/Image/CustomControls/1.png";

                Source = new BitmapImage(new Uri(resourcePath));
            }
            catch
            {
                // 如果加载失败，保持现有的 Source
            }
        }

        public HeroTagSelectableImage()
        {
            UpdateSource();
        }
    }
}
