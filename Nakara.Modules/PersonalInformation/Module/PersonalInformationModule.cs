using Nakara.Modules.PersonalInformation.UI.AvatarFrame.ViewModels;
using Nakara.Modules.PersonalInformation.UI.AvatarFrame.Views;
using Nakara.Modules.PersonalInformation.UI.CareerAchievements.ViewModels;
using Nakara.Modules.PersonalInformation.UI.CareerAchievements.Views;
using Nakara.Modules.PersonalInformation.UI.CreditScore.ViewModels;
using Nakara.Modules.PersonalInformation.UI.CreditScore.Views;
using Nakara.Modules.PersonalInformation.UI.HeroData.ViewModels;
using Nakara.Modules.PersonalInformation.UI.HeroData.Views;
using Nakara.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels;
using Nakara.Modules.PersonalInformation.UI.HistoryMatchRecord.Views;
using Nakara.Modules.PersonalInformation.UI.IllustratedCollection.ViewModels;
using Nakara.Modules.PersonalInformation.UI.IllustratedCollection.Views;
using Nakara.Modules.PersonalInformation.UI.PersonalInformation.ViewModels;
using Nakara.Modules.PersonalInformation.UI.PersonalInformation.Views;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using Nakara.Modules.PersonalInformation.UI.SeasonData.ViewModels;
using Nakara.Modules.PersonalInformation.UI.SeasonData.Views;
using Nakara.Modules.PersonalInformation.UI.SocialAvatar.ViewModels;
using Nakara.Modules.PersonalInformation.UI.SocialAvatar.Views;
using Nakara.Modules.PersonalInformation.UI.TippingRecord.ViewModels;
using Nakara.Modules.PersonalInformation.UI.TippingRecord.Views;

namespace Nakara.Modules.PersonalInformation.Module
{
    internal class PersonalInformationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<PersonalInformationUserControl>();
            containerRegistry.Register<PersonalInformationUserControlViewModel>();
            containerRegistry.RegisterForNavigation<
                PersonalInformationDetailMainContentUserControl,
                PersonalInformationDetailMainContentUserControlViewModel
            >();

            containerRegistry.RegisterForNavigation<HeroDataPage, HeroDataPageViewModel>();
            containerRegistry.RegisterForNavigation<HeroTagPage, HeroTagPageViewModel>();
            containerRegistry.RegisterForNavigation<
                HistoryMatchRecordPage,
                HistoryMatchRecordPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                PersonalInformationDetailsPage,
                PersonalInformationDetailsPageViewModel
            >();
            containerRegistry.RegisterForNavigation<AvatarFramePage, AvatarFramePageViewModel>();
            containerRegistry.RegisterForNavigation<
                CareerAchievementsPage,
                CareerAchievementsPageViewModel
            >();
            containerRegistry.RegisterForNavigation<
                IllustratedCollectionPage,
                IllustratedCollectionPageViewModel
            >();
            containerRegistry.RegisterForNavigation<CreditScorePage, CreditScorePageViewModel>();
            containerRegistry.RegisterForNavigation<SeasonDataPage, SeasonDataPageViewModel>();
            containerRegistry.RegisterForNavigation<SocialAvatarPage, SocialAvatarPageViewModel>();
            containerRegistry.RegisterForNavigation<
                TippingRecordPage,
                TippingRecordPageViewModel
            >();
        }
    }
}
