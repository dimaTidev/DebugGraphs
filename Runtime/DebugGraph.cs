#define USE_DEBUG_GRAPHS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugGraph
{
    public static void Log(string key, float value) 
    {
#if USE_DEBUG_GRAPHS || DEVELOPMENT_BUILD
        DebugGraphicManager.Instance.SetDebugGraphic(key, value);
#endif
    }

    public static void Log(string key, Vector2 value)
    {
        Log(key, value.x);
        Log(key, value.y);
    }

    public static void Log(string key, Vector3 value)
    {
        Log(key, value.x);
        Log(key, value.y);
        Log(key, value.z);
    }
}
