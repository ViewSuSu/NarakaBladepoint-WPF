using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using NarakaBladepoint.Framework.Core.Extensions;

namespace NarakaBladepoint.Framework.UI.Converters
{
    /// <summary>
    /// 将 ImageSource 转换为文件名的值转换器
    /// </summary>
    public class ImageSourceToFileNameConverter : IValueConverter
    {
        /// <summary>
        /// 将 ImageSource 转换为文件名（不带扩展名）
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ImageSource imageSource)
                return null;
            return imageSource.GetFileName();
        }

        /// <summary>
        /// 不支持反向转换
        /// </summary>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotSupportedException(
                "ImageSourceToFileNameConverter does not support backward conversion."
            );
        }
    }
}
