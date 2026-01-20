using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 提示消息服务接口
    /// </summary>
    public interface ITipMessageService
    {
        /// <summary>
        /// 异步发送提示消息
        /// </summary>
        /// <param name="message">提示消息内容</param>
        Task ShowTipMessageAsync(string message);

        /// <summary>
        /// 同步发送提示消息
        /// </summary>
        /// <param name="message">提示消息内容</param>
        void ShowTipMessage(string message);
    }
}
