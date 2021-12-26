using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugGraphicManager : MonoBehaviour
{
    static DebugGraphicManager instance;

    Dictionary<string, DebugGraphic> list = new Dictionary<string, DebugGraphic>();

    public DebugGraphic prefabGraphicLine;
    public Transform content;

    public static DebugGraphicManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject prefab = Resources.Load<GameObject>("DebugGraphs/Canvas_DebugGraphs");
                if (prefab)
                {
                    GameObject go = Instantiate(prefab);// GameObject("DebugGraphicManager_Singleton");
                    instance = go.GetComponent<DebugGraphicManager>();
                }
                else
                {
                    Debug.LogError("Can't find `Canvas_DebugGraphs` at path `Resources/DebugGraphs/Canvas_DebugGraphs`");
                    GameObject go = new GameObject("DebugGraphicManager_Singleton");
                    instance = go.AddComponent<DebugGraphicManager>();
                }
            }
            return instance;
        }
    }

    private void Awake() => instance = this;

    public void SetDebugGraphic(string key, float value)
    {
        if (!prefabGraphicLine)
        {
            Debug.LogError("You must Set Prefab Graphic first");
            return;
        }
           

        if (!list.ContainsKey(key))
            list.Add(key, CreateGraphic());

        list[key].AddPoint(value);
    }

    public void RemoveDebugGraphic(DebugGraphic graphic)
    {
        if (!graphic)
            return;
        var key = list.FirstOrDefault(x => x.Value == graphic).Key;
        if (list.ContainsKey(key))
            list.Remove(key);
        Destroy(graphic.gameObject);
    }

    DebugGraphic CreateGraphic()
    {
        if (!prefabGraphicLine)
            return null;
        GameObject go = Instantiate(prefabGraphicLine.gameObject, content);
        return go.GetComponent<DebugGraphic>();
    }
}
