using System.Collections.ObjectModel;
using NarakaBladepoint.Modules.ChatBox.UI.Models;

namespace NarakaBladepoint.Modules.ChatBox.UI.ViewModels
{
    internal class ChatBoxUserControlViewModel : ViewModelBase
    {
        public ObservableCollection<ChatMessageItem> ChatMessageItems { get; set; } = [];

        public ChatBoxUserControlViewModel(IContainerProvider containerProvider)
            : base(containerProvider) { }
    }
}
