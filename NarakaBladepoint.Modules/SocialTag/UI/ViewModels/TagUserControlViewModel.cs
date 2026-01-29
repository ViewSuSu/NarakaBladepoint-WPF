using System.ComponentModel;
using NarakaBladepoint.Modules.SocialTag.Domain.Events;
using NarakaBladepoint.Modules.SocialTag.UI.Models;

namespace NarakaBladepoint.Modules.SocialTag.UI.ViewModels
{
    internal class TagUserControlViewModel : ViewModelBase
    {
        private readonly ICurrentUserInfoProvider currentUserInfoProvider;
        private readonly ISocialTagProvider socialTagProvider;

        public string SelectedSocialTagOnlineModelContent =>
            SocialTagOnlineModels.FirstOrDefault(x => x.IsSelected)?.SocialTagOnlineData?.Text;

        public bool IsSettingMic
        {
            get
            {
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                if (currentModel.SelectedSocialTagMic.HasValue)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsSettingOnline
        {
            get
            {
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                if (currentModel.SelectedSocialTagOnline.HasValue)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsHaveMicAndSelected
        {
            get
            {
                var currentModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
                if (
                    currentModel.SelectedSocialTagMic.HasValue
                    && currentModel.SelectedSocialTagMic == 0
                )
                {
                    return true;
                }
                return false;
            }
        }

        private IEnumerable<SocialTagModel> _selectedSocialTagModels = [];

        public IEnumerable<SocialTagModel> SelectedSocialTagModels
        {
            get { return _selectedSocialTagModels; }
            set
            {
                _selectedSocialTagModels = value;
                RaisePropertyChanged();
            }
        }

        public TagUserControlViewModel(
            ICurrentUserInfoProvider currentUserInfoProvider,
            ISocialTagProvider socialTagProvider
        )
        {
            this.currentUserInfoProvider = currentUserInfoProvider;
            this.socialTagProvider = socialTagProvider;

            this.eventAggregator.GetEvent<NoticeSocialTagChangeEvent>().Subscribe(Init);
            Init();
        }

        private void Init()
        {
            var currentUserModel = this.currentUserInfoProvider.GetCurrentUserInfoAsync().Result;
            SelectedSocialTagModels = currentUserModel.SelectedSocialTags?.Select(
                x => new SocialTagModel(socialTagProvider.GetSocialTagByIndex(x).Result)
            );
            this.SocialTagOnlineModels = new BindingList<SocialTagOnlineModel>(
                socialTagProvider
                    .GetSocialTagOnlineDatas()
                    .Result.Select(x => new SocialTagOnlineModel(x)
                    {
                        IsSelected =
                            currentUserModel.SelectedSocialTagOnline.HasValue
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
                            currentUserModel.SelectedSocialTagOnline.HasValue
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
            )
            {
                RaisepropertyChangedExecute();
            }
        }

        private void RaisepropertyChangedExecute()
        {
            RaisePropertyChanged(nameof(IsSettingMic));
            RaisePropertyChanged(nameof(IsSettingOnline));
            RaisePropertyChanged(nameof(IsHaveMicAndSelected));
            RaisePropertyChanged(nameof(SelectedSocialTagOnlineModelContent));
            RaisePropertyChanged(nameof(SelectedSocialTagModels));
        }

        private BindingList<SocialTagMicModel> _socialTagMicModels;

        public BindingList<SocialTagMicModel> SocialTagMicModels
        {
            get { return _socialTagMicModels; }
            set
            {
                _socialTagMicModels = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<SocialTagOnlineModel> _socialTagOnlineModels;

        public BindingList<SocialTagOnlineModel> SocialTagOnlineModels
        {
            get { return _socialTagOnlineModels; }
            set
            {
                _socialTagOnlineModels = value;
                RaisePropertyChanged();
            }
        }
    }
}
