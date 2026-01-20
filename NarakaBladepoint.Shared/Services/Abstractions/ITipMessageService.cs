using NarakaBladepoint.Framework.Core.Evens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Services.Abstractions
{
    /// <summary>
    /// 提示消息服务接口
    ///
    /// 用途：在应用的主窗口顶部显示临时提示消息，用于反馈用户操作结果（如成功、失败、警告等）
    ///
    /// 特点：
    /// - 消息会自动动画从下往上滑出并显示
    /// - 显示0.5秒后开始淡出，整个动画周期为1.5秒
    /// - 支持异步调用，不阻塞UI线程
    /// - 如果消息未显示完成就有新消息，新消息会重新开始动画
    /// - 支持对消息中的特定文本进行高亮显示
    /// </summary>
    public interface ITipMessageService
    {
        /// <summary>
        /// 异步显示提示消息
        /// </summary>
        /// <param name="args">
        /// 提示消息参数，包含消息内容和需要高亮的文本列表
        ///
        /// 示例用途：
        /// - new TipMessageWithHighlightArgs("已复制") - 用户操作成功反馈
        /// - new TipMessageWithHighlightArgs("获得 500 金币", new List&lt;string&gt; { "500" }) - 高亮关键数字
        /// - new TipMessageWithHighlightArgs("操作失败，请重试") - 操作失败提示
        /// - new TipMessageWithHighlightArgs("获得奖励", new List&lt;string&gt; { "获得", "奖励" }) - 高亮多个关键词
        /// </param>
        /// <returns>异步任务，等待消息显示完成</returns>
        /// <example>
        /// <code>
        /// // 简单消息，不高亮
        /// await tipMessageService.ShowTipMessageAsync(new TipMessageWithHighlightArgs("操作成功"));
        /// 
        /// // 带高亮的消息
        /// await tipMessageService.ShowTipMessageAsync(new TipMessageWithHighlightArgs(
        ///     "恭喜获得 500 金币",
        ///     new List&lt;string&gt; { "500" }
        /// ));
        /// </code>
        /// </example>
        Task ShowTipMessageAsync(TipMessageWithHighlightArgs args);
    }
}
