using NarakaBladepoint.Modules.PersonalInformation.Domain.Consts;
using NarakaBladepoint.Modules.PersonalInformation.Domain.Events;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetailMainContent.ViewModels
{
    internal class PersonalInformationDetailMainContentUserControlViewModel : ViewModelBase
    {
        public PersonalInformationDetailMainContentUserControlViewModel(
            IContainerProvider containerProvider
        )
            : base(containerProvider)
        {
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

        private DelegateCommand _returnCommand;

        public DelegateCommand ReturnCommand =>
            _returnCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveHomePageRegionEvent>().Publish();
            });
    }
}
