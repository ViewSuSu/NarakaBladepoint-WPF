using NarakaBladepoint.Framework.Core.Bases;
using Prism.Commands;
using Prism.Events;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    /// <summary>
    /// 编辑名字页面ViewModel
    /// </summary>
    public class EditNamePageViewModel : ViewModelBase
    {
        private string _newName;
        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        public DelegateCommand ConfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public EditNamePageViewModel()
        {
            ConfirmCommand = new DelegateCommand(OnConfirm);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private void OnConfirm()
        {
            // TODO: 实现确认修改名字的逻辑
        }

        private void OnCancel()
        {
            // TODO: 实现取消修改的逻辑
        }
    }
}
