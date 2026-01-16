using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NarakaBladepoint.Modules.CommonFunction.UI.Hero.Models
{
    internal class HeroListItemModel : BindableBase
    {
        public HeroListItemModel(HeroAvatarModel heroAvatarModel)
        {
            _heroAvatarModel =
                heroAvatarModel ?? throw new ArgumentNullException(nameof(heroAvatarModel));
        }

        private readonly HeroAvatarModel _heroAvatarModel;

        public HeroAvatarModel HeroAvatarModel => _heroAvatarModel;

        public int Index => _heroAvatarModel.Index;

        public ImageSource Avatar => _heroAvatarModel.Avatar;

        public ImageSource ShowImage => _heroAvatarModel.ShowImage;

        public string Name
        {
            get => _heroAvatarModel.Name;
            set
            {
                if (_heroAvatarModel.Name == value)
                {
                    return;
                }

                _heroAvatarModel.Name = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(Index));
                RaisePropertyChanged(nameof(Avatar));
                RaisePropertyChanged(nameof(ShowImage));
            }
        }

        public string VoiceActor
        {
            get => _heroAvatarModel.VoiceActor;
            set
            {
                if (_heroAvatarModel.VoiceActor == value)
                {
                    return;
                }

                _heroAvatarModel.VoiceActor = value;
                RaisePropertyChanged();
            }
        }

        public string ShortDescription
        {
            get => _heroAvatarModel.ShortDescription;
            set
            {
                if (_heroAvatarModel.ShortDescription == value)
                {
                    return;
                }

                _heroAvatarModel.ShortDescription = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _heroAvatarModel.Description;
            set
            {
                if (_heroAvatarModel.Description == value)
                {
                    return;
                }

                _heroAvatarModel.Description = value;
                RaisePropertyChanged();
            }
        }

        public double HeroAccessibilityDifficulty
        {
            get => _heroAvatarModel.HeroAccessibilityDifficulty;
            set
            {
                if (_heroAvatarModel.HeroAccessibilityDifficulty == value)
                {
                    return;
                }

                _heroAvatarModel.HeroAccessibilityDifficulty = value;
                RaisePropertyChanged();
            }
        }

        public int Survival
        {
            get => _heroAvatarModel.Survival;
            set
            {
                if (_heroAvatarModel.Survival == value)
                {
                    return;
                }

                _heroAvatarModel.Survival = value;
                RaisePropertyChanged();
            }
        }

        public int Control
        {
            get => _heroAvatarModel.Control;
            set
            {
                if (_heroAvatarModel.Control == value)
                {
                    return;
                }

                _heroAvatarModel.Control = value;
                RaisePropertyChanged();
            }
        }

        public int Mobility
        {
            get => _heroAvatarModel.Mobility;
            set
            {
                if (_heroAvatarModel.Mobility == value)
                {
                    return;
                }

                _heroAvatarModel.Mobility = value;
                RaisePropertyChanged();
            }
        }

        public int Damage
        {
            get => _heroAvatarModel.Damage;
            set
            {
                if (_heroAvatarModel.Damage == value)
                {
                    return;
                }

                _heroAvatarModel.Damage = value;
                RaisePropertyChanged();
            }
        }

        public int Support
        {
            get => _heroAvatarModel.Support;
            set
            {
                if (_heroAvatarModel.Support == value)
                {
                    return;
                }

                _heroAvatarModel.Support = value;
                RaisePropertyChanged();
            }
        }
    }
}
