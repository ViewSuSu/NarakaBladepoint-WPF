using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class PersonalInformationDetailsPageViewModel : ViewModelBase
    {
        public List<PersonalInformationDetailModel> PersonalSeasonDataModels { get; set; } = [];
        public UserInformationModel CurrentUserBasicInformationModel { get; }

        private PersonalInformationDetailModel _selectedItem;
        public PersonalInformationDetailModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        public PersonalInformationDetailsPageViewModel(
            IContainerExtension containerExtension,
            ICurrentUserInformationProvider currentUserBasicInformation
        )
            : base(containerExtension)
        {
            HeroTagCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                    .Publish(nameof(HeroTagPage));
            });
            PersonalSeasonDataModels = currentUserBasicInformation
                .GetPersonalSeasonsAsync()
                .Result.ConvertToList<PersonalInformationDetailModel>();
            this.CurrentUserBasicInformationModel = currentUserBasicInformation
                .GetCurrentUserInfoAsync()
                .Result;
            SelectedItem = PersonalSeasonDataModels.FirstOrDefault();
        }

        public DelegateCommand HeroTagCommand { get; set; }
    }
}
