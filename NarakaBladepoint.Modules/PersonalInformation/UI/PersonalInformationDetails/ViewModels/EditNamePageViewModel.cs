namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    /// <summary>
    /// 编辑名字页面ViewModel
    /// </summary>
    public class EditNamePageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private string _newName;

        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        private DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand => _submitCommand ??= new DelegateCommand(OnSubmit);

        private DelegateCommand _randomNameCommand;
        public DelegateCommand RandomNameCommand =>
            _randomNameCommand ??= new DelegateCommand(OnRandomName);

        public EditNamePageViewModel() { }

        private void OnRandomName()
        {
            var names = new[]
            {
                "宁红夜",
                "特木尔",
                "迦南",
                "季沧海",
                "胡桃",
                "天海",
                "妖刀姬",
                "崔三娘",
                "岳山",
                "无尘",
                "顾清寒",
                "武田信忠",
                "殷紫萍",
                "沈妙",
                "胡为",
                "季莹莹",
                "玉玲珑",
                "哈迪",
                "魏轻",
                "刘炼",
            };
            var random = new Random();
            NewName = names[random.Next(names.Length)] + random.Next(100, 999);
        }

        private void OnSubmit()
        {
            // TODO: 实现确认修改名字的逻辑
        }

        private void OnCancel()
        {
            // TODO: 实现取消修改的逻辑
        }
    }
}
