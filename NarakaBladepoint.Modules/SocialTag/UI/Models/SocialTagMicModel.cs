namespace NarakaBladepoint.Modules.SocialTag.UI.Models
{
    internal class SocialTagMicModel : BindableBase
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

        public SocialTagMicData SocialTagMicData { get; }

        public SocialTagMicModel(SocialTagMicData socialTagMicData)
        {
            SocialTagMicData = socialTagMicData;
        }
    }
}
