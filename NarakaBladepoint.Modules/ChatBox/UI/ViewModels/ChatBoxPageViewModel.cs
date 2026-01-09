using System.Collections.ObjectModel;
using NarakaBladepoint.Modules.ChatBox.UI.Models;

namespace NarakaBladepoint.Modules.ChatBox.UI.ViewModels
{
    internal class ChatBoxPageViewModel : ViewModelBase
    {
        public ObservableCollection<ChatMessageItem> ChatMessageItems { get; set; } = [];

        public ChatBoxPageViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }
    }
}
