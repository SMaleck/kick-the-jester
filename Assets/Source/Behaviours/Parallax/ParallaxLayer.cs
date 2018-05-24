using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{        
    public class ParallaxLayer : AbstractComponent<Parallaxer>
    {
        private readonly ParallaxLayerConfig config;
        private readonly Transform cameraTransform;

        private Vector2 lastCameraPos;
        private List<GameObject> Tiles;
               
        private int leftTileIndex;
        private int rightTileIndex;
        
        
        public ParallaxLayer(Parallaxer owner, ParallaxLayerConfig config, Transform cameraTransform)
            : base (owner, false)
        {
            this.config = config;
            this.cameraTransform = cameraTransform;

            lastCameraPos = cameraTransform.position;

            CreateTiles();
        }


        // Creates the Parallax Tiles
        private void CreateTiles()
        {
            Tiles = new List<GameObject>() { CreateTile() };

            SpriteRenderer sprite = Tiles.First().GetComponent<SpriteRenderer>();
            float spriteWidth = sprite.bounds.size.x;

            // If this layer should repeat, create 2 adjecent tiles
            if (config.Repeat)
            {
                Tiles.Add(CreateTile(spriteWidth));
                Tiles.Add(CreateTile(-spriteWidth));

                rightTileIndex = 1;
                leftTileIndex = 2;
            }            
        }


        private GameObject CreateTile(float offsetX = 0)
        {
            GameObject go = new GameObject();
            go.transform.position = new Vector3(config.Position.x + offsetX, config.Position.y, config.Position.z);
            go.transform.localScale = config.Scale;


            // Setup Sprite Renderer
            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.Sprite;
            spriteRenderer.sortingLayerName = config.SortingLayer;
            spriteRenderer.sortingOrder = config.SortingOrder;

            return go;
        }


        protected override void LateUpdate()
        {
            // Prallaxing
            MoveTiles();
            lastCameraPos = cameraTransform.position;

            // Switch tiles if camera moved pass
            //SwitchTiles();            
        }


        // Moves all tiles to follow the camera according to the parallax speed
        private void MoveTiles()
        {
            if (config.UseSpeed)
            {
                foreach(GameObject go in Tiles)
                {
                    float deltaX = cameraTransform.position.x - lastCameraPos.x;
                    go.transform.position += Vector3.right * (deltaX * config.Speed);
                }
            }
        }

        /*
        private void SwitchTiles()
        {
            if (config.Repeat)
            {
                if (cameraTransform.position.x >= spriteTransforms[leftSpriteIndex].position.x + spriteWidth)
                {
                    ScrollRight();
                }
            }
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
        */
    }    
}
