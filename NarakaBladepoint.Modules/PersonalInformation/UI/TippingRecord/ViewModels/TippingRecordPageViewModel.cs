namespace NarakaBladepoint.Modules.PersonalInformation.UI.TippingRecord.ViewModels
{
    internal class TippingRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public UserInformationData CurrentUserModel { get; }

        public TippingRecordPageViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider
        )
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.CurrentUserModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
        }
    }
}
