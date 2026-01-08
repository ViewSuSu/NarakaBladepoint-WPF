using System.ComponentModel;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels
{
    internal class HeroTagPageViewModel : ViewModelBase
    {
        private readonly IHeroInfomation heroInfomation;
        private readonly ICurrentUserInformationProvider currentUserInformationProvider;
        private readonly IConfiguration configuration;

        public BindingList<HeroTagItemModel> ItemModels { get; }

        public int SelectedCount => ItemModels.Count(x => x.IsSelected);

        public int TotalCount => 10;

        public bool IsOutRange => SelectedCount < TotalCount;

        public HeroTagPageViewModel(
            IContainerProvider containerProvider,
            IHeroInfomation heroInfomation,
            ICurrentUserInformationProvider currentUserInformationProvider,
            IConfiguration configuration
        )
            : base(containerProvider)
        {
            this.heroInfomation = heroInfomation;
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.configuration = configuration;

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

            EscCommand = new DelegateCommand(() =>
            {
                eventAggregator
                    .GetEvent<RemovePersonalInformationDetailMainContentEvents>()
                    .Publish();
            });
            SaveCommand = new DelegateCommand(async () =>
            {
                var userModel = await currentUserInformationProvider.GetCurrentUserInfoAsync();
                userModel.SelectedHeroTags = ItemModels
                    .Where(x => x.IsSelected)
                    ?.Select(x => ItemModels.IndexOf(x))
                    .ToArray();
                var saveResult = await configuration.Save(userModel);
                if (saveResult)
                {
                    this.eventAggregator.GetEvent<SaveHeroTagEvent>().Publish();
                }
            });
        }

        public DelegateCommand EscCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
    }
}
