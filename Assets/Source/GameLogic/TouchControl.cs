using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Source.GameLogic
{
    class TouchControl : IPointerDownHandler
    {
        

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer down: " + eventData.pointerId);
        }
    }
}
