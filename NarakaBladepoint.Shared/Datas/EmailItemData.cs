using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Shared.Datas
{
    public enum EmailItemType
    {
        [Display(Name = "全部")]
        All = 0,

        [Display(Name = "通知")]
        Notice = 1,

        [Display(Name = "问卷")]
        Questionnaire = 2,

        [Display(Name = "推广")]
        Promotion = 3,

        [Display(Name = "活动")]
        Activity = 4,
    }

    public class EmailItemData
    {
        public int Index { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateTime { get; set; }

        public EmailItemType Type { get; set; }

        public int RemainingDays { get; set; }
    }
}
