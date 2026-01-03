using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nakara.Controls
{
    public class HighlightComboBoxItem : ComboBoxItem
    {
        static HighlightComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(HighlightComboBoxItem),
                new FrameworkPropertyMetadata(typeof(HighlightComboBoxItem))
            );
        }
    }
}
