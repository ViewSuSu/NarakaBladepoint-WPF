using Prism.Events;
using System.Collections.Generic;

namespace NarakaBladepoint.Framework.Core.Evens
{
    /// <summary>
    /// 提示消息事件
    /// </summary>
    public class TipMessageEvent : PubSubEvent<TipMessageWithHighlightArgs> { }

    /// <summary>
    /// 提示消息参数
    /// 包含消息内容和需要高亮的文本列表
    /// </summary>
    public class TipMessageWithHighlightArgs
    {
        /// <summary>
        /// 提示消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 需要高亮的文本列表（可空，不高亮则为null）
        /// </summary>
        public List<string> HighlightTexts { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TipMessageWithHighlightArgs()
        {
        }

        /// <summary>
        /// 仅消息的构造函数
        /// </summary>
        public TipMessageWithHighlightArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// 完整构造函数
        /// </summary>
        public TipMessageWithHighlightArgs(string message, List<string> highlightTexts)
        {
            Message = message;
            HighlightTexts = highlightTexts;
        }
    }
}


