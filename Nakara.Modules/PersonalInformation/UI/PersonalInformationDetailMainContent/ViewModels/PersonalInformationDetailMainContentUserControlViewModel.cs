using Nakara.Modules.PersonalInformation.Domain.Consts;
using Nakara.Modules.PersonalInformation.Domain.Events;
using Nakara.Modules.PersonalInformation.UI.HistoryMatchRecord.Views;
using Nakara.Modules.PersonalInformation.UI.PersonalInformationDetails.Views;

namespace Nakara.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels
{
    internal class PersonalInformationDetailMainContentUserControlViewModel : ViewModelBase
    {
        public PersonalInformationDetailMainContentUserControlViewModel(
            IContainerExtension containerExtension
        )
            : base(containerExtension)
        {
            ReturnCommand = new DelegateCommand(() =>
            {
                this.eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
            this.eventAggregator.GetEvent<LoadPersonalInformationDetailMainContentEvents>()
                .Subscribe(
                    (viewName) =>
                    {
                        this.regionManager.RequestNavigate(
                            PersonalInformationDetailMainContentRegionConsts.PersonalInformationMainContent,
                            viewName
                        );
                    },
                    ThreadOption.UIThread
                );
            this.eventAggregator.GetEvent<RemovePersonalInformationDetailMainContentEvents>()
                .Subscribe(
                    () =>
                    {
                        this.regionManager.Regions[
                                PersonalInformationDetailMainContentRegionConsts.PersonalInformationMainContent
                            ]
                            .RemoveAll();
                    },
                    ThreadOption.UIThread
                );
        }

        public DelegateCommand ReturnCommand { get; }
    }
}
