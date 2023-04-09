using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WellBites.MVVM.Converters
{
    public class EnumToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !value.GetType().IsEnum)
                return null;

            var enumValues = new List<object>();
            foreach (var enumValue in Enum.GetValues(value.GetType()))
            {
                var description = enumValue.ToString();
                var fieldInfo = value.GetType().GetField(enumValue.ToString());
                var attributes = fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false) as System.ComponentModel.DescriptionAttribute[];

                if (attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }

                enumValues.Add(new { Value = enumValue, Description = description });
            }

            return enumValues;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
