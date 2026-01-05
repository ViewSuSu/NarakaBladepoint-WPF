using System.Collections.ObjectModel;
using NarakaBladepoint.Framework.Core.Extensions;
using NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Models;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.ViewModels
{
    internal class SocialAvatarPageViewModel : ViewModelBase
    {
        public List<AvatarItemModel> AvatarItemModels { get; set; } = [];

        public SocialAvatarPageViewModel(
            IContainerExtension containerExtension,
            IImageSourceProvider imageSourceProvider
        )
            : base(containerExtension)
        {
            AvatarItemModels = imageSourceProvider
                .GetCurrenUserAllAvatarImageSources()
                .Result.Select(x => new AvatarItemModel()
                {
                    Icon = x,
                    Name = x.GetFileName(),
                    Description = "",
                    IsLocked = false,
                })
                .ToList();
        }
    }
}
