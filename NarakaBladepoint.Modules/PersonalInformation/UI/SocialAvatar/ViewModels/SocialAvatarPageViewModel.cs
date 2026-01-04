using System.Collections.ObjectModel;
using NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Models;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.ViewModels
{
    internal class SocialAvatarPageViewModel : ViewModelBase
    {
        public ObservableCollection<AvatarItemModel> AvatarItemModels { get; set; } = [];

        public SocialAvatarPageViewModel(IContainerExtension containerExtension)
            : base(containerExtension) { }
    }
}
