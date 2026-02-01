using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class EditGenderPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private bool _isSelectedMale;
        private bool _isSelectedFemale;
        private bool _isHideGender;

        public bool IsSelectedMale
        {
            get => _isSelectedMale;
            set => SetProperty(ref _isSelectedMale, value);
        }

        public bool IsSelectedFemale
        {
            get => _isSelectedFemale;
            set => SetProperty(ref _isSelectedFemale, value);
        }

        public bool IsHideGender
        {
            get => _isHideGender;
            set => SetProperty(ref _isHideGender, value);
        }

        private DelegateCommand _selectMaleCommand;
        public DelegateCommand SelectMaleCommand =>
            _selectMaleCommand ??= new DelegateCommand(() =>
            {
                IsSelectedMale = true;
                IsSelectedFemale = false;
            });

        private DelegateCommand _selectFemaleCommand;
        public DelegateCommand SelectFemaleCommand =>
            _selectFemaleCommand ??= new DelegateCommand(() =>
            {
                IsSelectedMale = false;
                IsSelectedFemale = true;
            });

        private DelegateCommand _submitCommand;
        public DelegateCommand SubmitCommand => _submitCommand ??= new DelegateCommand(OnSubmit);

        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly IConfiguration configuration;

        public EditGenderPageViewModel(ICurrentUserInfoProvider currentUserInfoProvider, IConfiguration configuration)
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.configuration = configuration;
            Init();
        }

        private void Init()
        {
            var currentUserModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
            IsSelectedMale = currentUserModel.Gender == 1;
            IsSelectedFemale = currentUserModel.Gender == 2;
            IsHideGender = currentUserModel.IsHideGender;
        }

        private async void OnSubmit()
        {
            var currentUserModel = await currentUserInfoProvider.GetCurrentUserInfoAsync();
            currentUserModel.Gender = IsSelectedMale ? 1 : (IsSelectedFemale ? 2 : 0);
            currentUserModel.IsHideGender = IsHideGender;
            var result = await configuration.SaveAsync(currentUserModel);
            if (result)
            {
                eventAggregator.GetEvent<SaveGenderSuccessEvent>().Publish();
            }
            ReturnCommand.Execute();
        }
    }
}
