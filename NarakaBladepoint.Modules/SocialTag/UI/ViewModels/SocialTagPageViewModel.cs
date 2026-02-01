using System.ComponentModel;
using NarakaBladepoint.Modules.SocialTag.Domain.Events;
using NarakaBladepoint.Modules.SocialTag.UI.Models;

namespace NarakaBladepoint.Modules.SocialTag.UI.ViewModels
{
    internal class SocialTagPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly ISocialTagProvider socialTagProvider;
        private readonly IConfiguration configuration;

        /// <summary>
        /// 当前选中的标签数量
        /// </summary>
        public int SelectedCount =>
            FightTags
                .Concat(SocialTags)
                .Concat(PersonalityTags)
                .Concat(ModeTags)
                .Concat(LanguageTags)
                .Count(x => x.IsSelected);

        public int MaxCount { get; set; } = 4;

        public int[] SelectedTagIndexArray =>
            FightTags
                .Concat(SocialTags)
                .Concat(PersonalityTags)
                .Concat(ModeTags)
                .Concat(LanguageTags)
                .Where(x => x.IsSelected)
                .Select(x => x.SocialTagData.Index)
                .ToArray();

        public IEnumerable<SocialTagModel> SelectedSocialTagModels =>
            FightTags
                .Concat(SocialTags)
                .Concat(PersonalityTags)
                .Concat(ModeTags)
                .Concat(LanguageTags)
                .Where(x => x.IsSelected);

        public SocialTagPageViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider,
            ISocialTagProvider socialTagProvider,
            IConfiguration configuration
        )
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.socialTagProvider = socialTagProvider;
            this.configuration = configuration;

            // 初始化当前用户信息
            this.CurrentUserModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;

            // 1. 战斗类标签（替换为BindingList）
            FightTags = new BindingList<SocialTagModel>(
                this.socialTagProvider.GetSocialTags(SocialTagType.Fight)
                    .Result.Select(x => new SocialTagModel(x)
                    {
                        IsSelected = socialTagProvider
                            .GetSocialTagIsSelectedByIndex(x.Index)
                            .Result,
                    })
                    .ToList()
            );

            // 2. 社交类标签（替换为BindingList）
            SocialTags = new BindingList<SocialTagModel>(
                this.socialTagProvider.GetSocialTags(SocialTagType.Social)
                    .Result.Select(x => new SocialTagModel(x)
                    {
                        IsSelected = socialTagProvider
                            .GetSocialTagIsSelectedByIndex(x.Index)
                            .Result,
                    })
                    .ToList()
            );

            // 3. 个性类标签（替换为BindingList）
            PersonalityTags = new BindingList<SocialTagModel>(
                this.socialTagProvider.GetSocialTags(SocialTagType.Personality)
                    .Result.Select(x => new SocialTagModel(x)
                    {
                        IsSelected = socialTagProvider
                            .GetSocialTagIsSelectedByIndex(x.Index)
                            .Result,
                    })
                    .ToList()
            );

            // 4. 模式类标签（替换为BindingList）
            ModeTags = new BindingList<SocialTagModel>(
                this.socialTagProvider.GetSocialTags(SocialTagType.Mode)
                    .Result.Select(x => new SocialTagModel(x)
                    {
                        IsSelected = socialTagProvider
                            .GetSocialTagIsSelectedByIndex(x.Index)
                            .Result,
                    })
                    .ToList()
            );

            // 5. 语言类标签（替换为BindingList）
            LanguageTags = new BindingList<SocialTagModel>(
                this.socialTagProvider.GetSocialTags(SocialTagType.Language)
                    .Result.Select(x => new SocialTagModel(x)
                    {
                        IsSelected = socialTagProvider
                            .GetSocialTagIsSelectedByIndex(x.Index)
                            .Result,
                    })
                    .ToList()
            );

            // 订阅所有BindingList的ListChanged事件
            FightTags.ListChanged += OnTagListChanged;
            SocialTags.ListChanged += OnTagListChanged;
            PersonalityTags.ListChanged += OnTagListChanged;
            ModeTags.ListChanged += OnTagListChanged;
            LanguageTags.ListChanged += OnTagListChanged;

            MouseLeftButtonDown = new DelegateCommand<SocialTagModel>(tag =>
            {
                tag.IsSelected = !tag.IsSelected;
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                currentModel.SelectedSocialTags = SelectedTagIndexArray;
                this.configuration.SaveAsync(currentModel);
                eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Publish();
            });

            CheckOnlineCommand = new DelegateCommand<SocialTagOnlineModel>(data =>
            {
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                if (data.IsSelected)
                {
                    foreach (var item in SocialTagOnlineModels)
                    {
                        if (item != data)
                        {
                            item.IsSelected = false;
                        }
                    }
                    currentModel.SelectedSocialTagOnline = data.SocialTagOnlineData.Index;
                }
                else
                {
                    currentModel.SelectedSocialTagOnline = null;
                }
                configuration.SaveAsync(currentModel);
                eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Publish();
            });

            CheckSocialTagMicCommand = new DelegateCommand<SocialTagMicModel>(data =>
            {
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                if (data.IsSelected)
                {
                    foreach (var item in SocialTagMicModels)
                    {
                        if (item != data)
                        {
                            item.IsSelected = false;
                        }
                    }
                    currentModel.SelectedSocialTagMic = data.SocialTagMicData.Index;
                }
                else
                {
                    currentModel.SelectedSocialTagMic = null;
                }
                configuration.SaveAsync(currentModel);
                eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Publish();
            });

            this.SocialTagOnlineModels = new BindingList<SocialTagOnlineModel>(
                socialTagProvider
                    .GetSocialTagOnlineDatas()
                    .Result.Select(x => new SocialTagOnlineModel(x)
                    {
                        IsSelected =
                            CurrentUserModel.SelectedSocialTagOnline.HasValue
                            && socialTagProvider
                                .GetSocialTagOnlineDataIsSelectedByIndex(x.Index)
                                .Result,
                    })
                    .ToList()
            );
            this.SocialTagMicModels = new BindingList<SocialTagMicModel>(
                socialTagProvider
                    .GetSocialTagMicDatas()
                    .Result.Select(x => new SocialTagMicModel(x)
                    {
                        IsSelected =
                            CurrentUserModel.SelectedSocialTagOnline.HasValue
                            && socialTagProvider
                                .GetSocialTagMicDataIsSelectedByIndex(x.Index)
                                .Result,
                    })
                    .ToList()
            );
            SocialTagOnlineModels.ListChanged += OnTagListChanged;
            SocialTagMicModels.ListChanged += OnTagListChanged;
            RaisepropertyChangedExecute();
        }

        /// <summary>
        /// BindingList变更事件处理方法
        /// </summary>
        /// <param name="sender">触发事件的BindingList</param>
        /// <param name="e">事件参数</param>
        private void OnTagListChanged(object sender, ListChangedEventArgs e)
        {
            if (
                e.PropertyDescriptor != null
                && e.PropertyDescriptor.Name == SocialTagModel.IsSelectedPropertyName
                && e.ListChangedType == ListChangedType.ItemChanged
            )
            {
                RaisepropertyChangedExecute();
            }
        }

        private void RaisepropertyChangedExecute()
        {
            RaisePropertyChanged(nameof(SelectedCount));
            RaisePropertyChanged(nameof(SelectedSocialTagModels));
            eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Publish();
        }

        // 当前用户信息（原有）
        public UserInformationData CurrentUserModel { get; private set; }

        // 战斗类标签列表（改为BindingList）
        public BindingList<SocialTagModel> FightTags { get; }

        // 社交类标签列表（改为BindingList）
        public BindingList<SocialTagModel> SocialTags { get; }

        // 个性类标签列表（改为BindingList）
        public BindingList<SocialTagModel> PersonalityTags { get; }

        // 模式类标签列表（改为BindingList）
        public BindingList<SocialTagModel> ModeTags { get; }

        // 语言类标签列表（改为BindingList）
        public BindingList<SocialTagModel> LanguageTags { get; }

        public DelegateCommand<SocialTagModel> MouseLeftButtonDown { get; set; }

        public DelegateCommand<SocialTagOnlineModel> CheckOnlineCommand { get; set; }
        public DelegateCommand<SocialTagMicModel> CheckSocialTagMicCommand { get; set; }

        public BindingList<SocialTagOnlineModel> SocialTagOnlineModels { get; }
        public BindingList<SocialTagMicModel> SocialTagMicModels { get; }
    }
}
