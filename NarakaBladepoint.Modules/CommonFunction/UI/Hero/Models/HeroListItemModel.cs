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

            _name = heroAvatarModel.Name;
            _voiceActor = heroAvatarModel.VoiceActor;
            _shortDescription = heroAvatarModel.ShortDescription;
            _description = heroAvatarModel.Description;
            _heroAccessibilityDifficulty = heroAvatarModel.HeroAccessibilityDifficulty;
            _survival = heroAvatarModel.Survival;
            _control = heroAvatarModel.Control;
            _mobility = heroAvatarModel.Mobility;
            _damage = heroAvatarModel.Damage;
            _support = heroAvatarModel.Support;
        }

        private readonly HeroAvatarModel _heroAvatarModel;

        public HeroAvatarModel HeroAvatarModel => _heroAvatarModel;

        public int Index => _heroAvatarModel.Index;

        public ImageSource Avatar => _heroAvatarModel.Avatar;

        public ImageSource ShowImage => _heroAvatarModel.ShowImage;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _voiceActor;
        public string VoiceActor
        {
            get => _voiceActor;
            set
            {
                _voiceActor = value;
                RaisePropertyChanged();
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get => _shortDescription;
            set
            {
                _shortDescription = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private double _heroAccessibilityDifficulty;
        public double HeroAccessibilityDifficulty
        {
            get => _heroAccessibilityDifficulty;
            set
            {
                _heroAccessibilityDifficulty = value;
                RaisePropertyChanged();
            }
        }

        private int _survival;
        public int Survival
        {
            get => _survival;
            set
            {
                _survival = value;
                RaisePropertyChanged();
            }
        }

        private int _control;
        public int Control
        {
            get => _control;
            set
            {
                _control = value;
                RaisePropertyChanged();
            }
        }

        private int _mobility;
        public int Mobility
        {
            get => _mobility;
            set
            {
                _mobility = value;
                RaisePropertyChanged();
            }
        }

        private int _damage;
        public int Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                RaisePropertyChanged();
            }
        }

        private int _support;
        public int Support
        {
            get => _support;
            set
            {
                _support = value;
                RaisePropertyChanged();
            }
        }
    }
}
