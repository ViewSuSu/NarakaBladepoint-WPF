namespace NarakaBladepoint.Modules.SocialTag.UI.Models
{
    internal class SocialTagOnlineModel : BindableBase
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

        public SocialTagOnlineData SocialTagOnlineData { get; }

        public SocialTagOnlineModel(SocialTagOnlineData socialTagOnlineData)
        {
            SocialTagOnlineData = socialTagOnlineData;
        }
    }
}
