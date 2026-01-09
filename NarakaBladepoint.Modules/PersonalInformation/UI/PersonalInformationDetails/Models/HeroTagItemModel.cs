using NarakaBladepoint.Shared.Services.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.PersonalInformationDetails.Models
{
    internal class HeroTagItemModel : BindableBase
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public HeroTagItemModel(HeroTagModel heroTagModel)
        {
            HeroTagModel = heroTagModel;
        }

        public HeroTagModel HeroTagModel { get; }
    }
}
