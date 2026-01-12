using NarakaBladepoint.Modules.PersonalInformation.UI.AvatarFrame.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.AvatarFrame.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.CareerAchievements.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.CareerAchievements.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.CreditScore.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.CreditScore.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.HeroData.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecord.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecordDetail.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.HistoryMatchRecordDetail.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.IllustratedCollection.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.IllustratedCollection.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformation.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.SeasonData.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.SeasonData.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Views;
using NarakaBladepoint.Modules.PersonalInformation.UI.TippingRecord.ViewModels;
using NarakaBladepoint.Modules.PersonalInformation.UI.TippingRecord.Views;

namespace NarakaBladepoint.Modules.PersonalInformation.Module
{
    internal class PersonalInformationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<PersonalInformationPage>();
            containerRegistry.Register<PersonalInformationPageViewModel>();
            containerRegistry.RegisterForNavigation<
                PersonalInformationDetailMainContentPage,
                PersonalInformationDetailMainContentPageViewModel
            >();

            containerRegistry.RegisterForNavigation<HeroDataPage, HeroDataPageViewModel>();
            containerRegistry.RegisterForNavigation<HeroTagPage, HeroTagPageViewModel>();
            containerRegistry.RegisterForNavigation<
                HistoryMatchRecordDetailPage,
                HistoryMatchRecordDetailPageViewModel
            >();
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
