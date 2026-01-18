using System;
using System.Collections.Generic;
using NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.Models;
using NarakaBladepoint.Shared.Datas;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Modules.PersonalInformation.UI.SocialAvatar.ViewModels
{
    internal class SocialAvatarPageViewModel : ViewModelBase
    {
        private static readonly Random _random = new();

        public SocialAvatarPageViewModel(
            IContainerProvider containerProvider,
            IAvatarProvider avatarProvider
        )
            : base(containerProvider)
        {
            var avatarDatas = avatarProvider.GetAvatarsAsync().Result;
            InitializeAvatarLists(avatarDatas);
        }

        // 全部头像列表（第一个Tab）
        private List<SocialAvatarItemModel> _allAvatarModels;
        public List<SocialAvatarItemModel> AllAvatarModels
        {
            get { return _allAvatarModels; }
            set
            {
                _allAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 英雄头像列表（第二个Tab）
        private List<SocialAvatarItemModel> _heroAvatarModels;
        public List<SocialAvatarItemModel> HeroAvatarModels
        {
            get { return _heroAvatarModels; }
            set
            {
                _heroAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 武器头像列表（第三个Tab）
        private List<SocialAvatarItemModel> _weaponAvatarModels;
        public List<SocialAvatarItemModel> WeaponAvatarModels
        {
            get { return _weaponAvatarModels; }
            set
            {
                _weaponAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 赛季奖励头像列表（第四个Tab）
        private List<SocialAvatarItemModel> _seasonRewardAvatarModels;
        public List<SocialAvatarItemModel> SeasonRewardAvatarModels
        {
            get { return _seasonRewardAvatarModels; }
            set
            {
                _seasonRewardAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 隐族秘宝头像列表（第五个Tab）
        private List<SocialAvatarItemModel> _hiddenTreasureAvatarModels;
        public List<SocialAvatarItemModel> HiddenTreasureAvatarModels
        {
            get { return _hiddenTreasureAvatarModels; }
            set
            {
                _hiddenTreasureAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 限时活动头像列表（第六个Tab）
        private List<SocialAvatarItemModel> _limitedEventAvatarModels;
        public List<SocialAvatarItemModel> LimitedEventAvatarModels
        {
            get { return _limitedEventAvatarModels; }
            set
            {
                _limitedEventAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        // 其他头像列表（第七个Tab）
        private List<SocialAvatarItemModel> _otherAvatarModels;
        public List<SocialAvatarItemModel> OtherAvatarModels
        {
            get { return _otherAvatarModels; }
            set
            {
                _otherAvatarModels = value;
                RaisePropertyChanged();
            }
        }

        private SocialAvatarItemModel _selectedAvatar;
        public SocialAvatarItemModel SelectedAvatar
        {
            get => _selectedAvatar;
            set
            {
                if (_selectedAvatar != null)
                    _selectedAvatar.IsSelected = false;
                SetProperty(ref _selectedAvatar, value);
                if (_selectedAvatar != null)
                    _selectedAvatar.IsSelected = true;
            }
        }

        private DelegateCommand<SocialAvatarItemModel> _selectAvatarCommand;
        public DelegateCommand<SocialAvatarItemModel> SelectAvatarCommand =>
            _selectAvatarCommand ??= new DelegateCommand<SocialAvatarItemModel>(avatar =>
            {
                SelectedAvatar = avatar;
            });

        // 初始化随机顺序和随机数量的头像列表
        private void InitializeAvatarLists(List<AvatarData> avatarDatas)
        {
            if (avatarDatas == null)
                return;

            // 为每个Tab创建一个随机顺序和随机数量的副本
            AllAvatarModels = GetRandomizedList(avatarDatas, 100);
            HeroAvatarModels = GetRandomizedList(avatarDatas, _random.Next(20, 51));
            WeaponAvatarModels = GetRandomizedList(avatarDatas, _random.Next(15, 41));
            SeasonRewardAvatarModels = GetRandomizedList(avatarDatas, _random.Next(10, 31));
            HiddenTreasureAvatarModels = GetRandomizedList(avatarDatas, _random.Next(10, 26));
            LimitedEventAvatarModels = GetRandomizedList(avatarDatas, _random.Next(5, 21));
            OtherAvatarModels = GetRandomizedList(avatarDatas, _random.Next(10, 36));

            // 默认选中第一个
            if (AllAvatarModels?.Count > 0)
                SelectedAvatar = AllAvatarModels[0];
        }

        // 获取随机顺序的列表副本
        private static List<SocialAvatarItemModel> GetRandomizedList(List<AvatarData> originalList, int maxCount)
        {
            var shuffled = originalList
                .OrderBy(_ => _random.Next())
                .Take(maxCount)
                .Select(x => new SocialAvatarItemModel(x))
                .ToList();

            return shuffled;
        }
    }
}
