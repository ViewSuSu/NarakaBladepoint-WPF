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
    /// </summary>
    public interface ITipMessageService
    {
        /// <summary>
        /// 异步显示提示消息
        /// </summary>
        /// <param name="message">
        /// 提示消息内容
        ///
        /// 示例用途：
        /// - "已复制" - 用户操作成功反馈
        /// - "复制失败" - 操作失败提示
        /// - "已打招呼！" - 操作完成确认
        /// - "加载中..." - 加载状态提示
        /// </param>
        /// <returns>异步任务，等待消息显示完成</returns>
        /// <example>
        /// <code>
        /// await tipMessageService.ShowTipMessageAsync("操作成功");
        /// </code>
        /// </example>
        Task ShowTipMessageAsync(string message);
    }
}
