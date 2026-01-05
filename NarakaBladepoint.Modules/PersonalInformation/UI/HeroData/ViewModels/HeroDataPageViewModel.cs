using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.ViewModels
{
    internal class HeroDataPageViewModel : ViewModelBase
    {
        public HeroDataPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInformationProvider currentUserInformationProvider
        )
            : base(containerProvider)
        {
            this.SeasonDataModels = currentUserInformationProvider.GetPersonalSeasonsAsync().Result;
            SelectedSeasonDataModel = SeasonDataModels.FirstOrDefault();
            this.CurrentUserInformationModel = currentUserInformationProvider
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public List<PersonalSeasonDataModel> SeasonDataModels { get; }

        private PersonalSeasonDataModel _selectedSeasonDataModel;
        public PersonalSeasonDataModel SelectedSeasonDataModel
        {
            get { return _selectedSeasonDataModel; }
            set
            {
                _selectedSeasonDataModel = value;
                RaisePropertyChanged();
            }
        }

        public UserInformationModel CurrentUserInformationModel { get; }
    }
}
