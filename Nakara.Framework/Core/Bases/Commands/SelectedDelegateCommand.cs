using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nakara.Framework.Core.Bases.Commands
{
    public class SelectedDelegateCommand : DelegateCommand
    {
        private bool isSelected;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                SetProperty(ref isSelected, value);
            }
        }

        public SelectedDelegateCommand(Action executeMethod)
            : base(executeMethod) { }

        public SelectedDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod) { }
    }
}
