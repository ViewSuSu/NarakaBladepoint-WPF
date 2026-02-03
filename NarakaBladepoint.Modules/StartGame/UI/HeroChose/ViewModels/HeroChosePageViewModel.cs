using System.Windows.Media;
using NarakaBladepoint.Modules.StartGame.UI.HeroChose.Models;

namespace NarakaBladepoint.Modules.StartGame.UI.HeroChose.ViewModels
{
    internal class HeroChosePageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly IHeroInfoProvider heroInfomation;
        private readonly ICurrentUserInfoProvider currentUserInformationProvider;
        private readonly IConfiguration configuration;

        public List<HeroChoseModuleItemModel> HeroChoseModuleItemModels { get; private set; }

        private int firstHeroIndex = -1;
        private int secondHeroIndex = -1;
        private int thirdHeroIndex = -1;

        public int FirstHeroIndex
        {
            get { return firstHeroIndex; }
            set
            {
                firstHeroIndex = value;
                RaisePropertyChanged(nameof(FirstHeroAvatar));
                RaisePropertyChanged(nameof(IsCanSelcted));
                RaisePropertyChanged(nameof(FirstHeroIsNull));
            }
        }

        public int SecondHeroIndex
        {
            get { return secondHeroIndex; }
            set
            {
                secondHeroIndex = value;
                RaisePropertyChanged(nameof(SecondHeroAvatar));
                RaisePropertyChanged(nameof(IsCanSelcted));
                RaisePropertyChanged(nameof(SecondHeroIsNull));
            }
        }

        public int ThirdHeroIndex
        {
            get { return thirdHeroIndex; }
            set
            {
                thirdHeroIndex = value;
                RaisePropertyChanged(nameof(ThirdHeroAvatar));
                RaisePropertyChanged(nameof(IsCanSelcted));
                RaisePropertyChanged(nameof(ThirdHeroIsNull));
            }
        }

        public ImageSource FirstHeroAvatar =>
            FirstHeroIndex != -1
                ? this.heroInfomation.GetHeroAvatarModelByIdAsync(FirstHeroIndex).Result.Avatar
                : null;

        public ImageSource SecondHeroAvatar =>
            SecondHeroIndex != -1
                ? this.heroInfomation.GetHeroAvatarModelByIdAsync(SecondHeroIndex).Result.Avatar
                : null;

        public ImageSource ThirdHeroAvatar =>
            ThirdHeroIndex != -1
                ? this.heroInfomation.GetHeroAvatarModelByIdAsync(ThirdHeroIndex).Result.Avatar
                : null;

        public bool IsCanSelcted => FirstHeroIsNull || SecondHeroIsNull || ThirdHeroIsNull;

        public bool FirstHeroIsNull => FirstHeroIndex == -1;
        public bool SecondHeroIsNull => SecondHeroIndex == -1;
        public bool ThirdHeroIsNull => ThirdHeroIndex == -1;

        public HeroChosePageViewModel(
            IHeroInfoProvider heroInfomation,
            ICurrentUserInfoProvider currentUserInformationProvider,
            IConfiguration configuration
        )
        {
            this.heroInfomation = heroInfomation;
            this.currentUserInformationProvider = currentUserInformationProvider;
            this.configuration = configuration;
        }

        private void SelectedHero(HeroChoseModuleItemModel selectedModel)
        {
            var index = HeroChoseModuleItemModels.IndexOf(selectedModel);

            // Check if the hero is already selected
            if (index == FirstHeroIndex || index == SecondHeroIndex || index == ThirdHeroIndex)
            {
                return;
            }

            selectedModel.IsSelected = true;
            if (FirstHeroIndex == -1)
            {
                FirstHeroIndex = index;
            }
            else if (SecondHeroIndex == -1)
            {
                SecondHeroIndex = index;
            }
            else if (ThirdHeroIndex == -1)
            {
                ThirdHeroIndex = index;
            }
        }

        private void Clear()
        {
            RemoveFirstHeroCommand.Execute();
            RemoveSecondHeroCommand.Execute();
            RemoveThirdHeroCommand.Execute();
        }

        private DelegateCommand _removeFirstHeroCommand;

        public DelegateCommand RemoveFirstHeroCommand =>
            _removeFirstHeroCommand ??= new DelegateCommand(() =>
            {
                if (FirstHeroIndex != -1)
                    HeroChoseModuleItemModels[FirstHeroIndex].IsSelected = false;
                FirstHeroIndex = -1;
            });

        private DelegateCommand _removeSecondHeroCommand;

        public DelegateCommand RemoveSecondHeroCommand =>
            _removeSecondHeroCommand ??= new DelegateCommand(() =>
            {
                if (SecondHeroIndex != -1)
                    HeroChoseModuleItemModels[SecondHeroIndex].IsSelected = false;
                SecondHeroIndex = -1;
            });

        private DelegateCommand _removeThirdHeroCommand;

        public DelegateCommand RemoveThirdHeroCommand =>
            _removeThirdHeroCommand ??= new DelegateCommand(() =>
            {
                if (ThirdHeroIndex != -1)
                    HeroChoseModuleItemModels[ThirdHeroIndex].IsSelected = false;
                ThirdHeroIndex = -1;
            });

        private DelegateCommand _saveCommand;

        public DelegateCommand SaveCommand =>
            _saveCommand ??= new DelegateCommand(() =>
            {
                var reuslt = Save().Result;
                if (reuslt)
                {
                    ReturnCommand.Execute();
                }
                else
                {
                    throw new Exception("保存失败！");
                }
            });

        private DelegateCommand _clearCommand;

        public DelegateCommand ClearCommand => _clearCommand ??= new DelegateCommand(Clear);

        private DelegateCommand<HeroChoseModuleItemModel> _selectedHeroCommand;

        public DelegateCommand<HeroChoseModuleItemModel> SelectedHeroCommand =>
            _selectedHeroCommand ??= new DelegateCommand<HeroChoseModuleItemModel>(SelectedHero);

        protected override async void OnNavigatedToExecute(NavigationContext navigationContext)
        {
            List<HeroAvatarModel> heroModels = await heroInfomation.GetHeroAvatarModelsAsync();
            this.HeroChoseModuleItemModels = heroModels
                .Select(x => new HeroChoseModuleItemModel(x))
                .ToList();

            UserInformationData userModel =
                await currentUserInformationProvider.GetCurrentUserInfoAsync();
            FirstHeroIndex = userModel.FirstPickHeroIndex;
            SecondHeroIndex = userModel.SecondPickHeroIndex;
            ThirdHeroIndex = userModel.ThridPickHeroIndex;
            if (FirstHeroIndex != -1)
                HeroChoseModuleItemModels[FirstHeroIndex].IsSelected = true;
            if (SecondHeroIndex != -1)
                HeroChoseModuleItemModels[SecondHeroIndex].IsSelected = true;
            if (ThirdHeroIndex != -1)
                HeroChoseModuleItemModels[ThirdHeroIndex].IsSelected = true;
            RaisePropertyChanged(nameof(HeroChoseModuleItemModels));
        }

        private async Task<bool> Save()
        {
            UserInformationData currentUserModel =
                await currentUserInformationProvider.GetCurrentUserInfoAsync();
            currentUserModel.FirstPickHeroIndex = FirstHeroIndex;
            currentUserModel.SecondPickHeroIndex = SecondHeroIndex;
            currentUserModel.ThridPickHeroIndex = ThirdHeroIndex;
            return await configuration.SaveAsync(currentUserModel);
        }
    }
}
