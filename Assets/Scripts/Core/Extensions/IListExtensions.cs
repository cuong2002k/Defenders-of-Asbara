using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IListExtensions
{
    public static bool Next(this IList list, ref int index, bool wrap = false)
    {
        int count = list.Count;
        if(count == 0)
        {
            return false;
        }

        index++;
        if(index >= count)
        {
            if(wrap)
            {
                index = 0;
                return true;
            }
            index = count - 1;
            return false;
        }

        return true;
    }
}
