using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Modules.EventCenter.Domain.EventParameters
{
    /// <summary>
    /// 导航到活动中心的参数
    /// </summary>
    internal class NavigatedToEventCenterParameter : NavigationParameters
    {
        /// <summary>
        /// 是否选中限时活动
        /// </summary>
        public bool IsSelectedLimitedEvent { get; }

        public NavigatedToEventCenterParameter(bool IsSelectedLimitedEvent = false)
        {
            this.IsSelectedLimitedEvent = IsSelectedLimitedEvent;
            AddParametersToNavigationParameters();
        }

        /// <summary>
        /// 通过反射将所有公共属性添加到导航参数字典中
        /// </summary>
        private void AddParametersToNavigationParameters()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

            foreach (var property in properties)
            {
                try
                {
                    var value = property.GetValue(this, null);
                    this.Add(property.Name, value);
                }
                catch (Exception ex)
                {
                    // 如果无法获取属性值，跳过该属性
                    System.Diagnostics.Debug.WriteLine(
                        $"无法添加属性 {property.Name} 到导航参数: {ex.Message}"
                    );
                }
            }
        }
    }
}
