using System.Collections.Concurrent;

namespace NarakaBladepoint.Modules.ChatBox.Domain
{
    public class ChatQueue : IDisposable
    {
        private BlockingCollection<Action> _tasks = new BlockingCollection<Action>();

        public event Action<string> MessageProcessed;

        public ChatQueue()
        {
            Task.Factory.StartNew(Consume, TaskCreationOptions.LongRunning);
        }

        public void AddMessage(string message, string sender = "系统")
        {
            _tasks.TryAdd(() =>
            {
                var processedMsg = $"[{DateTime.Now:HH:mm:ss}] {sender}: {message}";

                MessageProcessed?.Invoke(processedMsg);
            });
        }

        private void Consume()
        {
            foreach (Action action in _tasks.GetConsumingEnumerable())
            {
                action();
            }
        }

        public void Dispose()
        {
            _tasks.CompleteAdding();
        }
    }
}
