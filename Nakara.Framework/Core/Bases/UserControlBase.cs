using System.Windows.Controls;

namespace Nakara.Framework.Core.Bases
{
    public abstract class UserControlBase : UserControl
    {
        public UserControlBase()
        {
            ViewModelLocator.SetAutoWireViewModel(this, true);
        }
    }
}
