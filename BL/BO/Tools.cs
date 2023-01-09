using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BO;

static class Tools
{
    //implemention for tuples:
    public static string ToStringProperty<T>(this T t)
    {
        string str = "";
        foreach (PropertyInfo item in t!.GetType().GetProperties())
        {
            str += "\n" + item.Name + ": ";
            if (item.GetValue(t, null) is IEnumerable<object>)
            {
                IEnumerable<object> lst = (IEnumerable<object>)item.GetValue(obj: t, null)!;
                string s = String.Join(" ", lst);
                str += s;
            }
            else
                str += item.GetValue(t, null);
        }
        return str + '\n';
    }

    //this function is to help modify the output of enum values in comboBox:
    public static IEnumerable<string> GetEnumDescriptions<TEnum>() where TEnum : struct, Enum
    {
        var enumType = typeof(TEnum);

        IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();

        IEnumerable<string> descriptions = from enumValue in enumValues
                                           let fieldInfo = enumType.GetField(enumValue.ToString())
                                           let attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute
                                           select attribute?.Description ?? enumValue.ToString();

        return descriptions;
    }
}
