using Assets.Source.App;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class ParallaxLayer : BaseEntity
    {
        public float ParallaxSpeed = 1f;
        
        private Transform[] spriteTransforms;
        private float spriteWidth;
        
        private int leftSpriteIndex;
        private int rightSpriteIndex;

        private Transform cameraTransform;
        private Vector2 lastCameraPos;


        public void Awake()
        {
            cameraTransform = Camera.main.transform;
            spriteWidth = gameObject.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

            // Create Left and Right Clones
            GameObject goSprite = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;

            CreateAdjecentClone(goSprite, spriteWidth);
            CreateAdjecentClone(goSprite , -spriteWidth);

            // Get Transforms of all sprites            
            spriteTransforms = GetSpriteTransforms().ToArray();

            leftSpriteIndex = 0;
            rightSpriteIndex = spriteTransforms.Count() - 1;
        }              


        public void Update()
        {
            float deltaX = lastCameraPos.x - cameraTransform.position.x;
            transform.position += Vector3.right * (deltaX * ParallaxSpeed);

            lastCameraPos = cameraTransform.position;

            if (cameraTransform.position.x >= spriteTransforms[leftSpriteIndex].position.x + spriteWidth)
            {
                ScrollRight();
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
