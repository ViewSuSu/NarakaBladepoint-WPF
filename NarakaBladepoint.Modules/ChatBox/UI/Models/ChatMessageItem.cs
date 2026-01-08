namespace NarakaBladepoint.Modules.ChatBox.UI.Models
{
    internal class ChatMessageItem : BindableBase
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发送人名称
        /// </summary>
        public string Sender { get; set; }
    }
}
