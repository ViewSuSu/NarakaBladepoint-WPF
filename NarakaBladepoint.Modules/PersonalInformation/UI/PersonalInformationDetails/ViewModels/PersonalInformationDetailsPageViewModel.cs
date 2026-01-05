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
        private readonly ICurrentUserBasicInformation currentUserBasicInformation;
        public List<PersonalInformationDetailModel> PersonalSeasonDataModels { get; set; } = [];
        public UserInformationModel CurrentUserBasicInformationModel { get; }
        public PersonalInformationDetailModel SelectedItem { get; set; }

        public PersonalInformationDetailsPageViewModel(
            IContainerExtension containerExtension,
            ICurrentUserBasicInformation currentUserBasicInformation
        )
            : base(containerExtension)
        {
            this.currentUserBasicInformation = currentUserBasicInformation;
            HeroTagCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                    .Publish(nameof(HeroTagPage));
            });
            PersonalSeasonDataModels = currentUserBasicInformation
                .GetPersonalSeasonsAsync()
                .Result.ConvertToList<PersonalSeasonDataModel, PersonalInformationDetailModel>();
            this.CurrentUserBasicInformationModel = currentUserBasicInformation
                .GetCurrentUserInfoAsync()
                .Result;
        }

        public DelegateCommand HeroTagCommand { get; set; }
    }
}
