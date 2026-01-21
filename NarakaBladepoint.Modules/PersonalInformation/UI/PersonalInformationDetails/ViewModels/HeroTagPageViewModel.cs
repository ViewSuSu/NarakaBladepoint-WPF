using System.ComponentModel;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class HeroTagPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly IHeroInfoProvider heroInfomation;
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly IConfiguration configuration;

        private BindingList<HeroTagItemModel> _itemModels = [];

        public BindingList<HeroTagItemModel> ItemModels
        {
            get { return _itemModels; }
            set
            {
                _itemModels = value;
                RaisePropertyChanged();
            }
        }

        public int SelectedCount => ItemModels.Count(x => x.IsSelected);

        public static int TotalCount => 10;

        public bool IsOutRange => SelectedCount < TotalCount;

        public HeroTagPageViewModel(
            IHeroInfoProvider heroInfomation,
            ICurrentUserInfoProvider currentUserInformationProvider,
            IConfiguration configuration
        )
        {
            this.heroInfomation = heroInfomation;
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.configuration = configuration;
        }

        protected override void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            var userModel = currentUserInformationProvider.GetCurrentUserInfoAsync().Result;
            var allHeroTags = heroInfomation.GetHeroTagModelsAsync().Result;
            this.ItemModels = new BindingList<HeroTagItemModel>(
                allHeroTags
                    .Select(x => new HeroTagItemModel(x)
                    {
                        IsSelected = heroInfomation.GetHeroHeroTagIsSelectedAsync(x.Index).Result,
                    })
                    .ToList()
            );
            ItemModels.ListChanged += (o, e) =>
            {
                RaisePropertyChanged(nameof(SelectedCount));
                RaisePropertyChanged(nameof(IsOutRange));
            };
            RaisePropertyChanged(nameof(SelectedCount));
        }

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand =>
            _saveCommand ??= new DelegateCommand(async () =>
            {
                var userModel = await currentUserInformationProvider.GetCurrentUserInfoAsync();
                userModel.SelectedHeroTags = ItemModels
                    .Where(x => x.IsSelected)
                    ?.Select(x => ItemModels.IndexOf(x))
                    .ToArray();
                var saveResult = await configuration.Save(userModel);
                if (saveResult)
                {
                    eventAggregator.GetEvent<SaveHeroTagEvent>().Publish();
                }
            });
    }
}
