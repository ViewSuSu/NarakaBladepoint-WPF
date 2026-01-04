using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Models
{
    internal class AvatarItemModel
    {
        public string Name { get; set; }
        public ImageSource Icon { get; set; }
        public string Description { get; set; }

        public bool IsLocked { get; set; }
    }
}
