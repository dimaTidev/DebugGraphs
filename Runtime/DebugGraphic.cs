using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(150)]
public class DebugGraphic : MonoBehaviour
{
    [SerializeField] string prefixValue = "value:";
    [SerializeField] LineRenderer linRenderer;
    [SerializeField] UILineRenderer ui_linRenderer;
    [SerializeField] int frameCount = 240;
    Vector2[] positions = new Vector2[1];

    [SerializeField] Text
        text_value,
        text_minValue,
        text_maxValue;

    [SerializeField] float step = 0.1f;

    float maxValue, minValue;
    bool isInitializedValues;

    Vector3 tempPoint;

    private void Start() => positions = new Vector2[frameCount];

    public void AddPoint(float point)
    {
        tempPoint.x = 0;
        tempPoint.y = point;
        tempPoint.z = 0;

        if (!isInitializedValues)
        {
            maxValue = point;
            minValue = point;
            isInitializedValues = true;
        }
        else
        {
            maxValue = Mathf.Max(maxValue, point);
            minValue = Mathf.Min(minValue, point);
        }
    }

    void LateUpdate()
    {
       // AddPoint(Random.Range(0f, 1f));

        for (int i = positions.Length - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
            positions[i].x += step;
        }
           
        positions[0] = tempPoint;
        tempPoint = Vector2.zero;

        if (ui_linRenderer)
        {
          //  ui_linRenderer.positionCount = positions.Length;
            ui_linRenderer.SetPositions(positions);
        }

        if (text_value)
            text_value.text = prefixValue + "\n" + positions[0].y.ToString();
        if (text_maxValue)
            text_maxValue.text = "max:" + maxValue.ToString();
        if (text_minValue)
            text_minValue.text = "min:" + minValue.ToString();
    }

    public void Button_Remove() => DebugGraphicManager.Instance.RemoveDebugGraphic(this);

}
