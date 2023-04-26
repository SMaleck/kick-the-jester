using UnityEngine;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    public class HorizontalGradient : BaseMeshEffect
    {
        [SerializeField] private Color leftColor = Color.white;
        [SerializeField] private Color rightColor = Color.black;

        public override void ModifyMesh(VertexHelper vertexHelper)
        {
            if (!this.IsActive())
                return;

            List<UIVertex> list = new List<UIVertex>();
            vertexHelper.GetUIVertexStream(list);

            ModifyVertices(list);

            vertexHelper.Clear();
            vertexHelper.AddUIVertexTriangleStream(list);
        }

        public void ModifyVertices(List<UIVertex> vertexList)
        {
            if (!this.IsActive())
                return;

            int count = vertexList.Count;
            float leftX = vertexList[0].position.x;
            float rightX = vertexList[0].position.x;

            for (int i = 1; i < count; i++)
            {
                float x = vertexList[i].position.x;

                if (x > rightX)
                {
                    rightX = x;
                } else if (x < leftX)
                {
                    leftX = x;
                }
            }
            
            float uiElementWidth = rightX - leftX;

            for (int i = 0; i < count; i++)
            {
                UIVertex uiVertex = vertexList[i];
                uiVertex.color = uiVertex.color * Color.Lerp(leftColor, rightColor, (uiVertex.position.x - leftX) / uiElementWidth);
                vertexList[i] = uiVertex;
            }
        }
    }
}
