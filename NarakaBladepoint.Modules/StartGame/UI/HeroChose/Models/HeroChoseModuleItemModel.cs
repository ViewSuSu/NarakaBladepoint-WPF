namespace NarakaBladepoint.Modules.StartGame.UI.HeroChose.Models
{
    internal class HeroChoseModuleItemModel : BindableBase
    {
        internal const string IsSelectedProperty = nameof(IsSelected);

        private bool _isSelected;

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 英雄头像信息
        /// </summary>
        public HeroAvatarModel HeroAvatarModel { get; }

        public HeroChoseModuleItemModel(HeroAvatarModel heroAvatarModel)
        {
            HeroAvatarModel = heroAvatarModel;
        }
    }
}
