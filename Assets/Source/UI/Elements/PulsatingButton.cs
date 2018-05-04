using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Elements
{
    [Serializable]
    public class PulsatingButton : Button
    {
        [Header("Pulse Config")]
        [SerializeField] float pulseSpeed = 1f;
        [SerializeField] float minScale = 1f;
        [SerializeField] float maxScale = 1.3f;

        private int pulseDirection = 1;


        private void Update()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            UpdatePulseDirection(currentScale);

            float pulseStep = (pulseSpeed * Time.deltaTime) * pulseDirection;
            float nextScale = currentScale.x + pulseStep;

            gameObject.transform.localScale = new Vector3(nextScale, nextScale, 1);
        }


        private void UpdatePulseDirection(Vector3 currentScale)
        {
            if (currentScale.x < maxScale && currentScale.x > minScale)
            {
                return;
            }

            if (currentScale.x >= maxScale)
            {
                pulseDirection = -1;
            }
            else if (currentScale.x <= minScale)
            {
                pulseDirection = 1;
            }
        }
    }
}
