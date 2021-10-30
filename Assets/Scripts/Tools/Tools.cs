using UnityEngine;
using System.Collections.Generic;
using System;
public class Tools
{
    public static string replaceFirst(string value, string oldStr, string newStr) {
        int idx = value.IndexOf(oldStr);
        if (idx == -1)
            return value;
        value = value.Remove(idx, oldStr.Length);
        return value.Insert(idx, newStr);
    }
}
