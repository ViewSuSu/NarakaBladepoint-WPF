using System.Collections.ObjectModel;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.ViewModels
{
    internal class SocialAvatarPageViewModel : ViewModelBase
    {
        public SocialAvatarPageViewModel(
            IContainerProvider containerProvider,
            IAvatarProvider avatarProvider
        )
            : base(containerProvider)
        {
            this.AvatarItemModels = avatarProvider.GetAvatarsAsync().Result;
        }

        private List<AvatarData> _avatarItemModels;

        public List<AvatarData> AvatarItemModels
        {
            get { return _avatarItemModels; }
            set
            {
                _avatarItemModels = value;
                RaisePropertyChanged();
            }
        }
    }
}
