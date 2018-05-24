using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{        
    public class ParallaxLayer : AbstractComponent<Parallaxer>
    {
        private readonly ParallaxLayerConfig config;

        private GameObject spriteContainer;
        private List<GameObject> Columns;

        private Transform[] spriteTransforms;
        private float spriteWidth;

        private int leftSpriteIndex;
        private int rightSpriteIndex;

        private Transform cameraTransform;
        private Vector2 lastCameraPos;


        public ParallaxLayer(Parallaxer owner, ParallaxLayerConfig config, Transform cameraTransform)
            : base (owner, false)
        {
            this.config = config;
            this.cameraTransform = cameraTransform;

            lastCameraPos = cameraTransform.position;

            CreateLayerContainer();
        }


        private void CreateLayerContainer()
        {
            spriteContainer = new GameObject();

            SpriteRenderer spriteRenderer = spriteContainer.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;
            spriteRenderer.sortingLayerName = config.SortingLayer;
            spriteRenderer.sortingOrder = config.SortingOrder;
        }


        

        // Moves the left-most sprite to the right end
        private void ScrollRight()
        {
            Vector3 nextPos = new Vector3(spriteTransforms[rightSpriteIndex].position.x + spriteWidth, spriteTransforms[rightSpriteIndex].position.y, spriteTransforms[rightSpriteIndex].position.z);
            spriteTransforms[leftSpriteIndex].position = nextPos;

            rightSpriteIndex = leftSpriteIndex;
            leftSpriteIndex++;

            if (leftSpriteIndex >= spriteTransforms.Length)
            {
                leftSpriteIndex = 0;
            }
        }
    }    
}
