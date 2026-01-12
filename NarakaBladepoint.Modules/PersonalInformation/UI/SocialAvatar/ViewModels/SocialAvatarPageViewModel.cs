using System;
using System.Collections.Generic;
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
            
            // 初始化5个随机顺序的AvatarData列表
            InitializeRandomAvatarLists();
        }

        private List<AvatarData> _avatarItemModels;

        public List<AvatarData> AvatarItemModels
        {
            get { return _avatarItemModels; }
            set
            {
                _avatarItemModels = value;
                RaisePropertyChanged();
                
                // 当原始数据变化时，重新初始化随机列表
                InitializeRandomAvatarLists();
            }
        }
        
        // 全部头像列表（第一个Tab）
        private List<AvatarData> _allAvatarModels;
        public List<AvatarData> AllAvatarModels
        {
            get { return _allAvatarModels; }
            set
            {
                _allAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 英雄头像列表（第二个Tab）
        private List<AvatarData> _heroAvatarModels;
        public List<AvatarData> HeroAvatarModels
        {
            get { return _heroAvatarModels; }
            set
            {
                _heroAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 武器头像列表（第三个Tab）
        private List<AvatarData> _weaponAvatarModels;
        public List<AvatarData> WeaponAvatarModels
        {
            get { return _weaponAvatarModels; }
            set
            {
                _weaponAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 赛季奖励头像列表（第四个Tab）
        private List<AvatarData> _seasonRewardAvatarModels;
        public List<AvatarData> SeasonRewardAvatarModels
        {
            get { return _seasonRewardAvatarModels; }
            set
            {
                _seasonRewardAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 隐族秘宝头像列表（第五个Tab）
        private List<AvatarData> _hiddenTreasureAvatarModels;
        public List<AvatarData> HiddenTreasureAvatarModels
        {
            get { return _hiddenTreasureAvatarModels; }
            set
            {
                _hiddenTreasureAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 限时活动头像列表（第六个Tab）
        private List<AvatarData> _limitedEventAvatarModels;
        public List<AvatarData> LimitedEventAvatarModels
        {
            get { return _limitedEventAvatarModels; }
            set
            {
                _limitedEventAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 其他头像列表（第七个Tab）
        private List<AvatarData> _otherAvatarModels;
        public List<AvatarData> OtherAvatarModels
        {
            get { return _otherAvatarModels; }
            set
            {
                _otherAvatarModels = value;
                RaisePropertyChanged();
            }
        }
        
        // 初始化随机顺序和随机数量的头像列表
        private void InitializeRandomAvatarLists()
        {
            if (AvatarItemModels == null)
            {
                return;
            }
            
            // 使用随机数生成器
            var random = new Random();
            
            // 为每个Tab创建一个随机顺序和随机数量的副本
            AllAvatarModels = GetRandomizedList(AvatarItemModels, random, 100); // 全部Tab显示最多数量
            HeroAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 20, 50); // 英雄Tab：20-50个
            WeaponAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 15, 40); // 武器Tab：15-40个
            SeasonRewardAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 10, 30); // 赛季奖励Tab：10-30个
            HiddenTreasureAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 10, 25); // 隐族秘宝Tab：10-25个
            LimitedEventAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 5, 20); // 限时活动Tab：5-20个
            OtherAvatarModels = GetRandomizedListWithRandomCount(AvatarItemModels, random, 10, 35); // 其他Tab：10-35个
        }
        
        // 获取随机顺序的完整列表副本
        private List<AvatarData> GetRandomizedList(List<AvatarData> originalList, Random random, int maxCount)
        {
            // 创建原始列表的副本
            var listCopy = new List<AvatarData>(originalList);
            
            // 打乱列表顺序
            for (int i = listCopy.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (listCopy[i], listCopy[j]) = (listCopy[j], listCopy[i]);
            }
            
            // 如果列表数量超过最大值，截取前maxCount个
            if (listCopy.Count > maxCount)
            {
                listCopy = listCopy.Take(maxCount).ToList();
            }
            
            return listCopy;
        }
        
        // 获取随机顺序和随机数量的列表副本
        private List<AvatarData> GetRandomizedListWithRandomCount(List<AvatarData> originalList, Random random, int minCount, int maxCount)
        {
            // 创建原始列表的副本
            var listCopy = new List<AvatarData>(originalList);
            
            // 打乱列表顺序
            for (int i = listCopy.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (listCopy[i], listCopy[j]) = (listCopy[j], listCopy[i]);
            }
            
            // 生成随机数量
            int count = random.Next(minCount, maxCount + 1);
            
            // 截取前count个元素
            return listCopy.Take(count).ToList();
        }
    }
}
