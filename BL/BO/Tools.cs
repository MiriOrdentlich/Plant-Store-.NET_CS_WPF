﻿using System;
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
}
