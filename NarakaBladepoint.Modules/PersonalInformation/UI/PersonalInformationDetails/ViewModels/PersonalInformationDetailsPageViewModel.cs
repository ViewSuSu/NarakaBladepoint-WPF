using System.Collections.ObjectModel;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class PersonalInformationDetailsPageViewModel : ViewModelBase
    {
        public List<PersonalInformationDetailModel> PersonalSeasonDataModels { get; set; } = [];
        public UserInformationData CurrentUserBasicInformationModel { get; }

        private PersonalInformationDetailModel _selectedItem;
        private readonly ICurrentUserInfoProvider currentUserBasicInformation;
        private readonly IHeroInfoProvider heroInfomation;

        public PersonalInformationDetailModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<HeroTagModel> selectedHeroTagModels;

        public ObservableCollection<HeroTagModel> SelectedHeroTagModels
        {
            get { return selectedHeroTagModels; }
            set
            {
                selectedHeroTagModels = value;
                RaisePropertyChanged();
            }
        }

        public PersonalInformationDetailsPageViewModel(
            IContainerProvider containerProvider,
            ICurrentUserInfoProvider currentUserBasicInformation,
            IHeroInfoProvider heroInfomation
        )
            : base(containerProvider)
        {
            this.currentUserBasicInformation = currentUserBasicInformation;
            this.heroInfomation = heroInfomation;
            PersonalSeasonDataModels = currentUserBasicInformation
                .GetPersonalSeasonsAsync()
                .Result.ConvertToList<PersonalInformationDetailModel>();
            this.CurrentUserBasicInformationModel = currentUserBasicInformation
                .GetCurrentUserInfoAsync()
                .Result;
            SelectedItem = PersonalSeasonDataModels.FirstOrDefault();
            SetHeroTags();
            eventAggregator.GetEvent<SaveHeroTagEvent>().Subscribe(SetHeroTags);
        }

        private async void SetHeroTags()
        {
            var selectedTagItems = await heroInfomation.GetSelectedHeroTagModelsAsync();
            this.SelectedHeroTagModels = new ObservableCollection<HeroTagModel>(selectedTagItems);
            while (SelectedHeroTagModels.Count < 10)
            {
                SelectedHeroTagModels.Add(new HeroTagModel());
            }
        }

        private DelegateCommand _heroTagCommand;

        public DelegateCommand HeroTagCommand =>
            _heroTagCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                    .Publish(nameof(HeroTagPage));
            });
    }
}
