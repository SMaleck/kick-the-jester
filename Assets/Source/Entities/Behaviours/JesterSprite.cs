using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Entities.Behaviours
{
    public class JesterSprite : AbstractBehaviour
    {                
        public float RotationSpeed = 5f;

        private bool isRotating = false;
        private float minRotationSpeed = 1f;
        private float maxRotationSpeed = 100f;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SetRotation();
        }


        public void Update()
        {
            if (isRotating)
            {
                gameObject.transform.Rotate(rotationDirection * RotationSpeed * Time.deltaTime);
            }
        }


        private void SetRotation()
        {
            isRotating = true;
            RotationSpeed++;

            RotationSpeed = Mathf.Clamp(RotationSpeed, minRotationSpeed, maxRotationSpeed);
        }


        public void Stop()
        {
            isRotating = false;
        }
    }
}
