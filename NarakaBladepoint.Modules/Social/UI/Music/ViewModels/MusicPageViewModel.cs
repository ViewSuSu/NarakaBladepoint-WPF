using System.Collections.ObjectModel;
using NarakaBladepoint.Modules.Social.UI.Music.Models;

namespace NarakaBladepoint.Modules.Social.UI.Music.ViewModels
{
    internal class MusicPageViewModel : CanRemoveHomePageRegionViewModelBase
    {
        private int _selectedMusicIndex = 1;
        public int SelectedMusicIndex
        {
            get { return _selectedMusicIndex; }
            set
            {
                _selectedMusicIndex = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedPlayModeIndex = 0;
        public int SelectedPlayModeIndex
        {
            get { return _selectedPlayModeIndex; }
            set
            {
                _selectedPlayModeIndex = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedMusicTitle = "大厅音乐曲库";
        public string SelectedMusicTitle
        {
            get { return _selectedMusicTitle; }
            set
            {
                _selectedMusicTitle = value;
                RaisePropertyChanged();
            }
        }

        private string _musicCategoryOne = "大厅音乐曲库";
        public string MusicCategoryOne
        {
            get { return _musicCategoryOne; }
            set
            {
                _musicCategoryOne = value;
                RaisePropertyChanged();
            }
        }

        private string _musicCategoryTwo = "我喜欢的音乐";
        public string MusicCategoryTwo
        {
            get { return _musicCategoryTwo; }
            set
            {
                _musicCategoryTwo = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _playModeList = [];
        public ObservableCollection<string> PlayModeList
        {
            get { return _playModeList; }
            set
            {
                _playModeList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MusicItem> _musicList = [];
        public ObservableCollection<MusicItem> MusicList
        {
            get { return _musicList; }
            set
            {
                _musicList = value;
                RaisePropertyChanged();
                UpdateMusicCountAndEmptyState();
            }
        }

        private string _musicCountText = "上限 0/20";
        public string MusicCountText
        {
            get { return _musicCountText; }
            set
            {
                _musicCountText = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEmptyState = true;
        public bool IsEmptyState
        {
            get { return _isEmptyState; }
            set
            {
                _isEmptyState = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand<string> _selectMusicCommand;
        public DelegateCommand<string> SelectMusicCommand =>
            _selectMusicCommand ??= new DelegateCommand<string>(OnSelectMusic);

        public MusicPageViewModel()
        {
            InitializePlayModeList();
            InitializeMusicData();
        }

        private void InitializePlayModeList()
        {
            PlayModeList = new ObservableCollection<string> { "列表循环", "单曲循环", "随机播放" };
        }

        private void InitializeMusicData()
        {
            MusicList = new ObservableCollection<MusicItem>
            {
                new MusicItem
                {
                    Index = "1",
                    MusicName = "穿云箭（第19赛季大厅音乐）",
                    Artist = "Moe/24工作室/雷火音频",
                    IsLiked = false,
                    Duration = "2:32",
                },
                new MusicItem
                {
                    Index = "2",
                    MusicName = "剑提琶张（永劫无间职业联赛主题曲）",
                    Artist = "谭维维/GAI周延",
                    IsLiked = false,
                    Duration = "3:33",
                },
                new MusicItem
                {
                    Index = "3",
                    MusicName = "破茧（COUNTER TO GLORY）（2025永劫无间世界冠军赛主题曲）",
                    Artist = "希林娜依·高/永劫无间",
                    IsLiked = false,
                    Duration = "3:21",
                },
                new MusicItem
                {
                    Index = "4",
                    MusicName = "战不凡（2024永劫无间世界冠军赛主题曲）",
                    Artist = "阿达娃/永劫无间",
                    IsLiked = false,
                    Duration = "3:55",
                },
                new MusicItem
                {
                    Index = "5",
                    MusicName = "以我为姓（2023永劫无间世界冠军赛主题曲）",
                    Artist = "阿达娃",
                    IsLiked = false,
                    Duration = "3:33",
                },
                new MusicItem
                {
                    Index = "6",
                    MusicName = "无间渡",
                    Artist = "胡彦斌/南派三叔",
                    IsLiked = false,
                    Duration = "4:01",
                },
                new MusicItem
                {
                    Index = "7",
                    MusicName = "侠风起（第18赛季大厅音乐）",
                    Artist = "九亭/24工作室/雷火音频",
                    IsLiked = false,
                    Duration = "2:50",
                },
                new MusicItem
                {
                    Index = "8",
                    MusicName = "Dethrone",
                    Artist = "胡彦斌/南派三叔",
                    IsLiked = false,
                    Duration = "4:01",
                },
            };
            IsEmptyState = false;
            UpdateMusicCountText();
        }

        private void OnSelectMusic(string index)
        {
            if (int.TryParse(index, out int musicIndex))
            {
                SelectedMusicIndex = musicIndex;
                if (musicIndex == 1)
                {
                    SelectedMusicTitle = "大厅音乐曲库";
                    IsEmptyState = false;
                    UpdateMusicCountText();
                }
                else if (musicIndex == 2)
                {
                    SelectedMusicTitle = "我喜欢的音乐";
                    IsEmptyState = true;
                    UpdateMusicCountText();
                }
            }
        }

        private void UpdateMusicCountAndEmptyState()
        {
            UpdateMusicCountText();
            if (SelectedMusicIndex == 2)
            {
                IsEmptyState = MusicList.Count == 0;
            }
        }

        private void UpdateMusicCountText()
        {
            if (SelectedMusicIndex == 1)
            {
                MusicCountText = $"上限 {MusicList.Count}/20";
            }
            else if (SelectedMusicIndex == 2)
            {
                MusicCountText = "上限 0/20";
            }
        }
    }
}
