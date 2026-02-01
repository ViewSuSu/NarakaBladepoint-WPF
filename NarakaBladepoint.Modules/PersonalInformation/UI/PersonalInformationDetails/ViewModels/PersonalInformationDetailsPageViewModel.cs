using System.Collections.ObjectModel;
using System.Windows;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using NarakaBladepoint.Modules.SocialTag.Domain.Events;
using NarakaBladepoint.Modules.SocialTag.UI.Views;
using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class PersonalInformationDetailsPageViewModel : ViewModelBase
    {
        public List<PersonalInformationDetailModel> PersonalSeasonDataModels { get; set; } = [];
        public UserInformationData CurrentUserBasicInformationModel { get; private set; }

        private PersonalInformationDetailModel _selectedItem;
        private readonly ICurrentUserInfoProvider currentUserBasicInformation;
        private readonly IHeroInfoProvider heroInfomation;
        private readonly ITipMessageService tipMessageService;

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
            ICurrentUserInfoProvider currentUserBasicInformation,
            IHeroInfoProvider heroInfomation,
            ITipMessageService tipMessageService
        )
        {
            this.currentUserBasicInformation = currentUserBasicInformation;
            this.heroInfomation = heroInfomation;
            this.tipMessageService = tipMessageService;
            eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Subscribe(Init);
            eventAggregator.GetEvent<SaveNameSuccessEvent>().Subscribe(Init);
            Init();
        }

        private void Init()
        {
            PersonalSeasonDataModels = currentUserBasicInformation
                .GetPersonalSeasonsAsync()
                .Result.ConvertToList<PersonalInformationDetailModel>();
            this.CurrentUserBasicInformationModel = currentUserBasicInformation
                .GetCurrentUserInfoAsync()
                .Result;
            Name = CurrentUserBasicInformationModel.Name;
            IsHaveTags = CurrentUserBasicInformationModel.IsExsitAnyValidSocialTag;
            SelectedItem = PersonalSeasonDataModels.FirstOrDefault();
            SetHeroTags();
            eventAggregator.GetEvent<SaveHeroTagEvent>().Subscribe(SetHeroTags);
        }

        private bool _isHaveTags;

        public bool IsHaveTags
        {
            get { return _isHaveTags; }
            set
            {
                _isHaveTags = value;
                RaisePropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
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
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(HeroTagPage)));
            });

        private DelegateCommand _changeTagCommand;

        public DelegateCommand ChangeTagCommand =>
            _changeTagCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(SocialTagPage)));
            });

        private DelegateCommand _copyUserIdCommand;

        public DelegateCommand CopyUserIdCommand =>
            _copyUserIdCommand ??= new DelegateCommand(async () =>
            {
                try
                {
                    Clipboard.SetText(CurrentUserBasicInformationModel.Id.ToString());
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs("复制成功")
                    );
                }
                catch
                {
                    await tipMessageService.ShowTipMessageAsync(
                        new TipMessageWithHighlightArgs("复制失败")
                    );
                }
            });

        private DelegateCommand _editNameCommand;

        public DelegateCommand EditNameCommand =>
            _editNameCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(EditNamePage)));
            });

        private DelegateCommand _editGenderCommand;

        public DelegateCommand EditGenderCommand =>
            _editGenderCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(EditGenderPage)));
            });

        private DelegateCommand _choseTitleTagCommand;

        public DelegateCommand ChoseTitleTagCommand =>
            _choseTitleTagCommand ??= new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<LoadHomePageRegionEvent>()
                    .Publish(new NavigationArgs(nameof(TitleTagPage)));
            });
    }
}
