using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NarakaBladepoint.Framework.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的 Description 特性描述
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>描述字符串，不存在则返回 null</returns>
        public static string? GetDescription(this Enum value)
        {
            if (value == null)
                return null;

            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
                return null;

            var field = type.GetField(name);
            if (field == null)
                return null;

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description;
        }
    }
}
