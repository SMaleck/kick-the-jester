using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class ParallaxLayer : AbstractBehaviour
    {
        public bool UseSpeed = true;
        public bool Repeat = true;

        // RANGE [0, 1]
        // <0: Super Speed!
        // 0 : Same speed as the Camera, i.e. normal speed
        // 1 : Synched with Camera, i.e. will never move out
        // >1: Move forward :D
        public float ParallaxSpeed = 0;
        
        private Transform[] spriteTransforms;
        private float spriteWidth;
        
        private int leftSpriteIndex;
        private int rightSpriteIndex;

        private Transform cameraTransform;
        private Vector2 lastCameraPos;


        private void Awake()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPos = cameraTransform.position;

            spriteWidth = gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

            // Create Left and Right Clones
            if (Repeat)
            {
                GameObject goSprite = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;

                CreateAdjecentClone(goSprite, spriteWidth);
                CreateAdjecentClone(goSprite, -spriteWidth);                
            }

            // Get Transforms of all sprites            
            spriteTransforms = GetSpriteTransforms().ToArray();

            leftSpriteIndex = 0;
            rightSpriteIndex = spriteTransforms.Count() - 1;
        }              


        private void LateUpdate()
        {
            // Prallaxing
            if (UseSpeed)
            {
                float deltaX = cameraTransform.position.x - lastCameraPos.x;
                transform.position += Vector3.right * (deltaX * ParallaxSpeed);
            }

            lastCameraPos = cameraTransform.position;

            // Scroll to the right, if Camera moved past the left-most tile
            if (Repeat)
            {
                if (cameraTransform.position.x >= spriteTransforms[leftSpriteIndex].position.x + spriteWidth)
                {
                    ScrollRight();
                }
            }           
        }


        // Creates a clone of the sprite-carrying gameobject with the specified offset
        private void CreateAdjecentClone(GameObject toClone, float OffsetX)
        {            
            GameObject goCopy = GameObject.Instantiate(toClone);
            goCopy.transform.parent = goTransform;

            goCopy.transform.position = new Vector3(goCopy.transform.position.x + OffsetX, goCopy.transform.position.y, goCopy.transform.position.z);
        }


        // Gets all transforms in Children, ordered by the X-position
        private IEnumerable<Transform> GetSpriteTransforms()
        {            
            List<Transform> transforms = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                transforms.Add(transform.GetChild(i));
            }

            return transforms.OrderBy(e => e.transform.position.x);
        }


        // Moves the left-most sprite to the right end
        private void ScrollRight()
        {            
            spriteTransforms[leftSpriteIndex].position = Vector3.right * (spriteTransforms[rightSpriteIndex].position.x + spriteWidth);

            rightSpriteIndex = leftSpriteIndex;
            leftSpriteIndex++;

            if(leftSpriteIndex >= spriteTransforms.Length)
            {
                leftSpriteIndex = 0;
            }            
        }
    }
}
