using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Framework.Core.Attrbuites;
using NarakaBladepoint.Framework.Core.Evens;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    /// <summary>
    /// 提示消息服务实现
    /// </summary>
    [Component(ComponentLifetime.Singleton)]
    internal class TipMessageService : ITipMessageService
    {
        private readonly IEventAggregator _eventAggregator;

        public TipMessageService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// 异步发送提示消息
        /// </summary>
        /// <param name="message">提示消息内容</param>
        public async Task ShowTipMessageAsync(string message)
        {
            await Task.Run(() =>
            {
                _eventAggregator.GetEvent<TipMessageEvent>().Publish(message);
            });
        }

        /// <summary>
        /// 同步发送提示消息
        /// </summary>
        /// <param name="message">提示消息内容</param>
        public void ShowTipMessage(string message)
        {
            _eventAggregator.GetEvent<TipMessageEvent>().Publish(message);
        }
    }
}
