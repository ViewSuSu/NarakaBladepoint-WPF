using System.Windows.Media;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Models
{
    internal class SocialAvatarItemModel : BindableBase
    {
        public int Index { get; set; }

        public ImageSource ImageSource { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsHave { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        public SocialAvatarItemModel() { }

        public SocialAvatarItemModel(AvatarData avatarData)
        {
            Index = avatarData.Index;
            ImageSource = avatarData.ImageSource;
            Name = avatarData.Name;
            Description = avatarData.Description;
            IsHave = avatarData.IsHave;
        }
    }
}
