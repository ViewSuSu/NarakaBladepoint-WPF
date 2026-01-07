using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class PersonalInformationDetailsPageViewModel : ViewModelBase
    {
        public List<PersonalInformationDetailModel> PersonalSeasonDataModels { get; set; } = [];
        public UserInformationData CurrentUserBasicInformationModel { get; }

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
            IContainerProvider containerProvider,
            ICurrentUserInformationProvider currentUserBasicInformation
        )
            : base(containerProvider)
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
