using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UILineRenderer : Graphic
{
	[SerializeField] Vector2[] points;

    float step;

    protected override void Start()
    {
        base.Start();

        CorrectPoints();
    }

    [SerializeField] float thickness = 0.2f;


    public void SetPositions(Vector2[] points) 
    {
        CorrectPoints();
        this.points = points;
        SetVerticesDirty();
    }

    void CorrectPoints()
    {
        step = GetPixelAdjustedRect().width / (points.Length - 1);

        for (int i = 0; i < points.Length; i++)
            points[i].x = step * i;
    }

    float MaxHeight
    {
        get
        {
            float returnValue = 1;
            for (int i = 0; i < points.Length; i++)
                returnValue = Mathf.Max(returnValue, points[i].y);
            return returnValue;
        }
    }

    float MinHeight
    {
        get
        {
            float returnValue = 1;
            for (int i = 0; i < points.Length; i++)
                returnValue = Mathf.Min(returnValue, points[i].y);
            return returnValue;
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        //  vh = vertexHelper;
        // base.OnPopulateMesh(vh);

        if (points == null || points.Length < 2)
            return;

        CorrectPoints();

        vh.Clear();

        //UIVertex vertex = UIVertex.simpleVert;
        Vector2 vertex;

        var rect = GetPixelAdjustedRect();

        float maxHeight = Mathf.Max(Mathf.Abs(MaxHeight), Mathf.Abs(MinHeight));
        float halfHeight = rect.height * 0.5f;
       // var rectWithOffset = new Vector4(rect.x, rect.y, rect.x + rect.width, rect.y + rect.height);

        Vector2 offset = new Vector2(rect.x, rect.y);

        int vertCount = points.Length * 2;


        Vector2 tempPos;

        for (int i = 0; i < points.Length; i++)
        {

            Vector2 rightVector;//rightVectorForward;

            Vector2 rightVectorForward = Vector2.zero;
            Vector2 rightVectorBackward = Vector2.zero;

            if (i < points.Length - 1)
            {
                Vector2 directionForward = (points[i + 1] - points[i]).normalized;
                rightVectorForward = Vector2.Perpendicular(directionForward).normalized;
            }

            if (i > 0)
            {
                Vector2 directionBackward = (points[i] - points[i - 1]).normalized;
                rightVectorBackward = Vector2.Perpendicular(directionBackward).normalized; 
            }

            if(i == 0)
                rightVector = rightVectorForward;
            else if(i == points.Length - 1)
                rightVector = rightVectorBackward;
            else
                rightVector = Vector2.Lerp(rightVectorForward, rightVectorBackward, 0.5f).normalized;

            tempPos = points[i];
            tempPos.y = (tempPos.y / maxHeight) * halfHeight + halfHeight;

            vertex = tempPos + offset + rightVector * thickness; //+ new Vector3(-thickness + rectWithOffset.x, thickness + rectWithOffset.y);
            vh.AddVert(vertex, color, Vector2.zero);
            vertex = tempPos + offset - rightVector * thickness;
            vh.AddVert(vertex, color, Vector2.zero);
            // vertex.position = points[i] + new Vector3(-thickness + rect.x, thickness + rect.y);
        }

        int vert;
        for (int i = 0; i < points.Length - 1; i++)
        {
            vert = i * 2;

            vh.AddTriangle(vert, vert + 1, vert + 3);

           // if (i != 0)
                vh.AddTriangle(vert, vert + 3, vert + 2);
           // else
           //     vh.AddTriangle(vert + 1, vert + 3, vert + 2);
            
                

           // vh.AddTriangle(vert + 3, vert + 1, vert);
           // vh.AddTriangle(vert + 2, vert + 3, vert);
        }


        /*  int vertCount = points.Length * 4;

          for (int i = 0; i < points.Length; i++)
          {
              vertex.position = points[i] + new Vector3(-thickness, -thickness);
              vertex.position.x += v.x;
              vertex.position.y += v.y;
              vh.AddVert(vertex);

              vertex.position = points[i] + new Vector3(-thickness, thickness);
              vertex.position.x += v.x;
              vertex.position.y += v.y;
              vh.AddVert(vertex);

              vertex.position = points[i] + new Vector3(thickness, thickness);
              vertex.position.x += v.x;
              vertex.position.y += v.y;
              vh.AddVert(vertex);

              vertex.position = points[i] + new Vector3(thickness, -thickness);
              vertex.position.x += v.x;
              vertex.position.y += v.y;
              vh.AddVert(vertex);
          }

           int vert;
           for (int i = 0; i < points.Length; i++)
           {
              vert = i * 4;
              vh.AddTriangle(vert, vert + 1, vert + 2);
              vh.AddTriangle(vert + 2, vert + 3, vert);

              vh.AddTriangle(vert, vert + 1, vert + 2);
              vh.AddTriangle(vert + 2, vert + 3, vert);
          }*/

        //  var r = GetPixelAdjustedRect();
        //  var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
        //
        //  Color32 color32 = color;
        //  vh.Clear();
        //  vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(0f, 0f));
        //  vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(0f, 1f));
        //  vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1f, 1f));
        //  vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1f, 0f));
        //
        //  vh.AddTriangle(0, 1, 2);
        //  vh.AddTriangle(2, 3, 0);



        //base.OnPopulateMesh(vh);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        CorrectPoints();
    }
#endif
}