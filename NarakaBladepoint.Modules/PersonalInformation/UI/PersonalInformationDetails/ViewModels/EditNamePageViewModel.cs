using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;

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
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IConfiguration configuration;

        public DelegateCommand RandomNameCommand =>
            _randomNameCommand ??= new DelegateCommand(OnRandomName);

        public EditNamePageViewModel(ICurrentUserInfoProvider currentUserInfoProvider, IConfiguration configuration)
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.configuration = configuration;
        }

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

        private async void OnSubmit()
        {
            var currentUserModel = await currentUserInfoProvider.GetCurrentUserInfoAsync();
            currentUserModel.Name = NewName;
            var result = await configuration.SaveAsync(currentUserModel);
            if (result)
            {
                eventAggregator.GetEvent<SaveNameSuccessEvent>().Publish();
            }
            ReturnCommand.Execute();
        }
    }
}
