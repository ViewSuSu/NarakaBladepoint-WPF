using System.Windows.Controls;

namespace NarakaBladepoint.Framework.Core.Bases
{
    public abstract class UserControlBase : UserControl
    {
        public UserControlBase()
        {
            ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
