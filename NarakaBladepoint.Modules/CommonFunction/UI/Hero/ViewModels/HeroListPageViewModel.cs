using NarakaBladepoint.Modules.CommonFunction.Domain.Bases;
using NarakaBladepoint.Modules.CommonFunction.UI.Hero.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Hero.ViewModels
{
    internal class HeroListPageViewModel : CommonFunctionPageViewModelBase
    {
        private readonly IHeroInfoProvider heroInfoProvider;

        public List<HeroListItemModel> HeroAvatarModels { get; }

        private HeroListItemModel _selectedHeroAvatarModel;

        public HeroListItemModel SelectedHeroAvatarModel
        {
            get { return _selectedHeroAvatarModel; }
            set
            {
                _selectedHeroAvatarModel = value;
                RaisePropertyChanged();
            }
        }

        public HeroListPageViewModel(IHeroInfoProvider heroInfoProvider)
        {
            this.heroInfoProvider = heroInfoProvider;
            this.HeroAvatarModels = heroInfoProvider
                .GetHeroAvatarModelsAsync()
                .Result.Select(x => new HeroListItemModel(x))
                .ToList();
            SelectedHeroAvatarModel = HeroAvatarModels.FirstOrDefault();
        }

        private DelegateCommand<HeroListItemModel> selectedCommand;

        public DelegateCommand<HeroListItemModel> SelectedCommand =>
            selectedCommand
            ?? new DelegateCommand<HeroListItemModel>(item =>
            {
                SelectedHeroAvatarModel = item;
            });
    }
}
