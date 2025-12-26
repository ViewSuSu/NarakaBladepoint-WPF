using Nakara.Modules.PersonalInformation.UI.MatchRecord.ViewModels;
using Nakara.Modules.PersonalInformation.UI.MatchRecord.Views;

namespace Nakara.Modules.PersonalInformation.Module
{
    internal class MatchRecordModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<
                MatchRecordUserControl,
                MatchRecordUserControlViewModel
            >();
        }
    }
}
