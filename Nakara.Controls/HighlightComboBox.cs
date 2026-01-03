using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nakara.Controls
{
    public class HighlightComboBox : ComboBox
    {
        static HighlightComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HighlightComboBox),
                new FrameworkPropertyMetadata(typeof(HighlightComboBox))
            );
        }

        public int HighlightIndex
        {
            get => (int)GetValue(HighlightIndexProperty);
            set => SetValue(HighlightIndexProperty, value);
        }

        public static readonly DependencyProperty HighlightIndexProperty =
            DependencyProperty.Register(
                nameof(HighlightIndex),
                typeof(int),
                typeof(HighlightComboBox),
                new PropertyMetadata(-1)
            );
    }
}
