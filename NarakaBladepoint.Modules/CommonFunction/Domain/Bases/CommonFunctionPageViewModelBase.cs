using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Modules.CommonFunction.Domain.Bases
{
    internal abstract class CommonFunctionPageViewModelBase : ViewModelBase
    {
        public CommonFunctionPageViewModelBase() { }

        private DelegateCommand _returnToHallCommand;

        /// <summary>
        /// 返回大厅
        /// </summary>
        public DelegateCommand ReturnToHallCommand =>
            _returnToHallCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
            });
    }
}
