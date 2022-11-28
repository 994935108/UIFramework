using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDebugTool 
{
    public static void Log(object msg)
    {
        Debug.Log(string.Format("wuxh~~~{0}~~~", msg));

    }

    public static void LogError(object msg)
    {
        Debug.LogError(string.Format("wuxh~~~{0}~~~", msg));

    }
}
