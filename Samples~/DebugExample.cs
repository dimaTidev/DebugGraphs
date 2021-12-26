using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugExample : MonoBehaviour
{
   
    void Update()
    {
        DebugGraph.Log("MouseY", Input.mouseScrollDelta.y);
    }
}
