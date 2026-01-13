namespace NarakaBladepoint.Modules.PersonalInformation.UI.TippingRecord.ViewModels
{
    internal class TippingRecordPageViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;

        public UserInformationData CurrentUserModel { get; }

        public TippingRecordPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInfoProvider
        )
            : base(containerProvider)
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.CurrentUserModel = currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
        }
    }
}
