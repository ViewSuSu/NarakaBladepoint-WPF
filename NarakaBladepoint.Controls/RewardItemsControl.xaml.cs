using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NarakaBladepoint.Controls
{
    /// <summary>
    /// RewardItemsControl.xaml 的交互逻辑
    /// </summary>
    public partial class RewardItemsControl : UserControl
    {
        #region ItemsSource

        public IEnumerable<RewardViewModel> ItemsSource
        {
            get => (IEnumerable<RewardViewModel>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable<RewardViewModel>),
                typeof(RewardItemsControl),
                new PropertyMetadata(null)
            );

        #endregion ItemsSource

        #region ProgressbarValue

        public int ProgressbarValue
        {
            get => (int)GetValue(ProgressbarValueProperty);
            set => SetValue(ProgressbarValueProperty, value);
        }

        public static readonly DependencyProperty ProgressbarValueProperty =
            DependencyProperty.Register(
                "ProgressbarValue",
                typeof(int),
                typeof(RewardItemsControl),
                new PropertyMetadata(0)
            );

        #endregion ProgressbarValue

        #region ProgressbarMaximum

        public int ProgressbarMaximum
        {
            get => (int)GetValue(ProgressbarMaximumProperty);
            set => SetValue(ProgressbarMaximumProperty, value);
        }

        public static readonly DependencyProperty ProgressbarMaximumProperty =
            DependencyProperty.Register(
                "ProgressbarMaximum",
                typeof(int),
                typeof(RewardItemsControl),
                new PropertyMetadata(0)
            );

        #endregion ProgressbarMaximum

        #region ImageWidth

        public double ImageWidth
        {
            get => (double)GetValue(ImageWidthProperty);
            set => SetValue(ImageWidthProperty, value);
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register(
                "ImageWidth",
                typeof(double),
                typeof(RewardItemsControl),
                new PropertyMetadata(60.0)
            );

        #endregion ImageWidth

        #region ImageHeight

        public double ImageHeight
        {
            get => (double)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register(
                "ImageHeight",
                typeof(double),
                typeof(RewardItemsControl),
                new PropertyMetadata(60.0)
            );

        #endregion ImageHeight

        #region CountTextForeground

        public Brush CountTextForeground
        {
            get => (Brush)GetValue(CountTextForegroundProperty);
            set => SetValue(CountTextForegroundProperty, value);
        }

        public static readonly DependencyProperty CountTextForegroundProperty =
            DependencyProperty.Register(
                "CountTextForeground",
                typeof(Brush),
                typeof(RewardItemsControl),
                new PropertyMetadata(new SolidColorBrush(Colors.White))
            );

        #endregion CountTextForeground

        #region CountTextFontSize

        public double CountTextFontSize
        {
            get => (double)GetValue(CountTextFontSizeProperty);
            set => SetValue(CountTextFontSizeProperty, value);
        }

        public static readonly DependencyProperty CountTextFontSizeProperty =
            DependencyProperty.Register(
                "CountTextFontSize",
                typeof(double),
                typeof(RewardItemsControl),
                new PropertyMetadata(12.0)
            );

        #endregion CountTextFontSize

        #region ValueTextForeground

        public Brush ValueTextForeground
        {
            get => (Brush)GetValue(ValueTextForegroundProperty);
            set => SetValue(ValueTextForegroundProperty, value);
        }

        public static readonly DependencyProperty ValueTextForegroundProperty =
            DependencyProperty.Register(
                "ValueTextForeground",
                typeof(Brush),
                typeof(RewardItemsControl),
                new PropertyMetadata(new SolidColorBrush(Colors.White))
            );

        #endregion ValueTextForeground

        #region CountUnitText

        public string CountUnitText
        {
            get => (string)GetValue(CountUnitTextProperty);
            set => SetValue(CountUnitTextProperty, value);
        }

        public static readonly DependencyProperty CountUnitTextProperty =
            DependencyProperty.Register(
                "CountUnitText",
                typeof(string),
                typeof(RewardItemsControl),
                new PropertyMetadata("件")
            );

        #endregion CountUnitText

        public RewardItemsControl()
        {
            InitializeComponent();
        }
    }

    /// <summary>
    /// RewardViewModel - 奖励项数据模型
    /// </summary>
    public class RewardViewModel
    {
        /// <summary>
        /// 奖励项的图片源
        /// </summary>
        public ImageSource ImageSource { get; set; }

        /// <summary>
        /// 奖励项的价值/数值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 奖励项的数量
        /// </summary>
        public int Count { get; set; }
    }
}
