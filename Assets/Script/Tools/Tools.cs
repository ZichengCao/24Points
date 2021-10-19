using UnityEngine;
using System.Collections.Generic;

public class Tools
{
    public static List<int> getNoRepeatList(int length)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < length; i++) list.Add(i);

        for (int i = 0; i < length; i++)
        {
            int T1 = Random.Range(0, length);
            int T2 = Random.Range(0, length);
            Swap(list, T1, T2);
        }
        return list;
    }
    public static void Swap<T>(List<T> list, int x, int y)
    {
        T temp = list[x];
        list[x] = list[y];
        list[y] = temp;
    }

    public static string replaceFirst(string value, string oldStr, string newStr) {
        int idx = value.IndexOf(oldStr);
        if (idx == -1)
            return value;
        value = value.Remove(idx, oldStr.Length);
        return value.Insert(idx, newStr);
    }
}
