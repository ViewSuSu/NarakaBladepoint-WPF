using System.Collections.ObjectModel;
using NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.Models;

namespace NarakaBladepoint.Modules.CommonFunction.UI.CustomMatch.ViewModels
{
    internal class CustomMatchPageViewModel : CanRemoveMainContentRegionViewModelBase
    {
        private readonly string[] _roomNamePrefixes = new[]
        {
            "00088",
            "雨里躲风起的房间",
            "96",
            "oiyeye的房间",
            "女大学生刀房",
            "豆豆有俯炒炒香的房间",
            "半飘飘的房间",
            "与郅再关关的房间",
            "实战007",
            "竞技场对抗",
            "高手集中营",
            "新手练习室",
            "排位冲分队",
            "匹配团队",
            "休闲游戏房",
        };

        private readonly string[] _modes = new[]
        {
            "无尽试炼",
            "排位赛",
            "竞技场",
            "团队战",
            "大逃杀",
        };
        private readonly string[] _statuses = new[] { "准备中", "游戏中" };
        private readonly ITipMessageService tipMessageService;
        private ObservableCollection<RoomItem> _roomList = [];
        private ObservableCollection<RoomItem> _allRooms = [];
        private Random _random = new Random();

        public ObservableCollection<RoomItem> RoomList
        {
            get { return _roomList; }
            set
            {
                _roomList = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand<string> _searchCommand;

        public DelegateCommand<string> SearchCommand =>
            _searchCommand ??= new DelegateCommand<string>(OnSearch);

        private DelegateCommand _returnCommand;

        public DelegateCommand ReturnCommand =>
            _returnCommand ??= new DelegateCommand(() =>
            {
                eventAggregator.GetEvent<RemoveMainContentRegionEvent>().Publish();
            });

        private DelegateCommand _refreshCommand;

        public DelegateCommand RefreshCommand =>
            _refreshCommand ??= new DelegateCommand(() =>
            {
                GenerateRandomRooms();
            });

        private DelegateCommand _createRoomCommand;

        public DelegateCommand CreateRoomCommand =>
            _createRoomCommand ??= new DelegateCommand(async () =>
            {
                await tipMessageService.ShowTipMessageAsync(
                    new TipMessageWithHighlightArgs("开发中...")
                );
            });

        public CustomMatchPageViewModel(ITipMessageService tipMessageService)
        {
            this.tipMessageService = tipMessageService;
            InitializeRoomData();
        }

        private void InitializeRoomData()
        {
            _allRooms = new ObservableCollection<RoomItem>
            {
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "00088",
                    Delay = "50ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "1",
                    TeamSlots = "1/9(0/0)",
                    IsLocked = true,
                },
                new RoomItem
                {
                    Status = "游戏中",
                    RoomName = "雨里躲风起的房间",
                    Delay = "48ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "3",
                    TeamSlots = "3/6(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "96",
                    Delay = "50ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "3",
                    TeamSlots = "2/6(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "oiyeye的房间",
                    Delay = "55ms",
                    Mode = "排位赛",
                    PlayersPerTeam = "3",
                    TeamSlots = "1/60(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "女大学生刀房",
                    Delay = "22ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "1",
                    TeamSlots = "1/2(0/0)",
                    IsLocked = true,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "豆豆有俯炒炒香的房间",
                    Delay = "55ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "3",
                    TeamSlots = "2/6(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "半飘飘的房间",
                    Delay = "48ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "1",
                    TeamSlots = "1/2(0/0)",
                    IsLocked = true,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "与郅再关关的房间",
                    Delay = "55ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "1",
                    TeamSlots = "2/2(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "实战007",
                    Delay = "22ms",
                    Mode = "无尽试炼",
                    PlayersPerTeam = "1",
                    TeamSlots = "2/2(0/0)",
                    IsLocked = false,
                },
                new RoomItem
                {
                    Status = "准备中",
                    RoomName = "竞技场对抗",
                    Delay = "45ms",
                    Mode = "竞技场",
                    PlayersPerTeam = "2",
                    TeamSlots = "1/4(0/0)",
                    IsLocked = true,
                },
            };

            RoomList = new ObservableCollection<RoomItem>(_allRooms);
        }

        private void GenerateRandomRooms()
        {
            var randomRooms = new ObservableCollection<RoomItem>();
            int roomCount = _random.Next(8, 15);

            for (int i = 0; i < roomCount; i++)
            {
                var roomName = _roomNamePrefixes[_random.Next(_roomNamePrefixes.Length)];
                var delay = _random.Next(20, 80);
                var mode = _modes[_random.Next(_modes.Length)];
                var playersPerTeam = _random.Next(1, 4);
                var currentPlayers = _random.Next(1, playersPerTeam + 1);
                var totalSlots = playersPerTeam * (_random.Next(2, 4));
                var isLocked = _random.Next(0, 2) == 0;

                randomRooms.Add(
                    new RoomItem
                    {
                        Status = _statuses[_random.Next(_statuses.Length)],
                        RoomName = roomName + (i > 0 ? $"_{i}" : ""),
                        Delay = $"{delay}ms",
                        Mode = mode,
                        PlayersPerTeam = playersPerTeam.ToString(),
                        TeamSlots = $"{currentPlayers}/{totalSlots}(0/0)",
                        IsLocked = isLocked,
                    }
                );
            }

            _allRooms = randomRooms;
            RoomList = new ObservableCollection<RoomItem>(_allRooms);
        }

        private void OnSearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                RoomList = new ObservableCollection<RoomItem>(_allRooms);
                return;
            }

            var filteredRooms = _allRooms
                .Where(room =>
                    room.RoomName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                    || room.Mode.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                )
                .ToList();

            RoomList = new ObservableCollection<RoomItem>(filteredRooms);
        }
    }
}
