using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Nakara.Controls
{
    public class ComboBoxToggleExtensions
    {
        #region HighlightIndex 依赖属性

        public static readonly DependencyProperty HighlightIndexProperty =
            DependencyProperty.RegisterAttached(
                "HighlightIndex",
                typeof(int),
                typeof(ComboBoxToggleExtensions),
                new FrameworkPropertyMetadata(
                    -1,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnHighlightIndexChanged
                )
            );

        public static int GetHighlightIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(HighlightIndexProperty);
        }

        public static void SetHighlightIndex(DependencyObject obj, int value)
        {
            obj.SetValue(HighlightIndexProperty, value);
        }

        private static void OnHighlightIndexChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            // 当HighlightIndex改变时，刷新ComboBox的Items
            if (d is System.Windows.Controls.ComboBox comboBox)
            {
                // 强制刷新ItemContainer样式
                comboBox.InvalidateProperty(ItemsControl.ItemContainerStyleProperty);
            }
        }

        #endregion

        #region HighlightBackground 依赖属性

        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.RegisterAttached(
                "HighlightBackground",
                typeof(Brush),
                typeof(ComboBoxToggleExtensions),
                new FrameworkPropertyMetadata(
                    Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.Inherits,
                    OnHighlightBackgroundChanged
                )
            );

        public static Brush GetHighlightBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(HighlightBackgroundProperty);
        }

        public static void SetHighlightBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(HighlightBackgroundProperty, value);
        }

        private static void OnHighlightBackgroundChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            // 当HighlightBackground改变时，刷新ComboBox的Items
            if (d is System.Windows.Controls.ComboBox comboBox)
            {
                comboBox.InvalidateProperty(ItemsControl.ItemContainerStyleProperty);
            }
        }

        #endregion

        #region IsHighlighted 只读依赖属性（用于ItemContainer）

        private static readonly DependencyPropertyKey IsHighlightedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsHighlighted",
                typeof(bool),
                typeof(ComboBoxToggleExtensions),
                new PropertyMetadata(false)
            );

        public static readonly DependencyProperty IsHighlightedProperty =
            IsHighlightedPropertyKey.DependencyProperty;

        public static bool GetIsHighlighted(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHighlightedProperty);
        }

        internal static void SetIsHighlighted(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHighlightedPropertyKey, value);
        }

        #endregion
    }
}
