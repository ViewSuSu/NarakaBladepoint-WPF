using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.ViewModels
{
    internal class HeroDataPageViewModel : ViewModelBase
    {
        public HeroDataPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.SeasonDataModels = currentUserInformationProvider.GetPersonalSeasonsAsync().Result;
            SelectedSeasonDataModel = SeasonDataModels.FirstOrDefault();
            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public List<SeasonDataModel> SeasonDataModels { get; }

        private SeasonDataModel _selectedSeasonDataModel;

        public SeasonDataModel SelectedSeasonDataModel
        {
            get { return _selectedSeasonDataModel; }
            set
            {
                _selectedSeasonDataModel = value;
                RaisePropertyChanged();
            }
        }

        public UserInformationData CurrentUserInformationModel { get; }
    }
}
