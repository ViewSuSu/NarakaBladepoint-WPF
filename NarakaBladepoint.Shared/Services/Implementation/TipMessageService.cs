using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NarakaBladepoint.Framework.Core.Attrbuites;
using NarakaBladepoint.Framework.Core.Evens;
using NarakaBladepoint.Shared.Services.Abstractions;

namespace NarakaBladepoint.Shared.Services.Implementation
{
    /// <summary>
    /// 提示消息服务实现
    /// 通过事件发布机制将消息和高亮文本传递到主窗口
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
        /// 异步显示提示消息
        /// </summary>
        /// <param name="args">包含消息内容和高亮文本的参数</param>
        public async Task ShowTipMessageAsync(TipMessageWithHighlightArgs args)
        {
            await Task.Run(() =>
            {
                // 发布提示消息事件，包含消息内容和高亮文本
                _eventAggregator.GetEvent<TipMessageEvent>().Publish(args);
            });
        }
    }
}

