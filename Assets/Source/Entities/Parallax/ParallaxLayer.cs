using System.Collections.Generic;
using System.Linq;
using Assets.Source.Entities.GenericComponents;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Parallax
{        
    public class ParallaxLayer : AbstractComponent<Parallaxer>
    {
        private readonly ParallaxLayerConfig config;
        private readonly Transform cameraTransform;

        private Vector2 lastCameraPos;
        private List<GameObject> Tiles;

        private float spriteWidth;
        private int leftTileIndex;
        private int rightTileIndex;

        private Vector3 leftTilePosition
        {
            get { return Tiles[leftTileIndex].transform.position; }
            set { Tiles[leftTileIndex].transform.position = value; }
        }

        private Vector3 rightTilePosition
        {
            get { return Tiles[rightTileIndex].transform.position; }
            set { Tiles[rightTileIndex].transform.position = value; }
        }


        public ParallaxLayer(Parallaxer owner, ParallaxLayerConfig config, Transform cameraTransform) 
            : base(owner)
        {
            this.config = config;
            this.cameraTransform = cameraTransform;

            lastCameraPos = cameraTransform.position;

            CreateTiles();

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(owner);
        }


        // Creates the Parallax Tiles
        private void CreateTiles()
        {
            Tiles = new List<GameObject>() { CreateTile() };

            // Get Sprite Width
            SpriteRenderer sprite = Tiles.First().GetComponent<SpriteRenderer>();
            this.spriteWidth = sprite.bounds.size.x;

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

            go.name = "PrxLayer_" + spriteRenderer.sprite.name;
            return go;
        }


        protected void OnLateUpdate()
        {
            // Prallaxing
            MoveTiles();
            lastCameraPos = cameraTransform.position;

            // Switch tiles for infinite background
            SwitchTiles();            
        }


        // Moves all tiles to follow the camera according to the parallax speed
        private void MoveTiles()
        {
            if (!config.UseSpeed) { return; }

            foreach (GameObject go in Tiles)
            {
                float deltaX = cameraTransform.position.x - lastCameraPos.x;
                go.transform.position += Vector3.right * (deltaX * config.Speed);
            }
        }

        
        // Switches the Tiles so that the Camera always sees the middle one, i.e. inifnite scrolling
        private void SwitchTiles()
        {
            if (!config.Repeat) { return; }

            if (CameraEnteredLeftTile())
            {
                ScrollLeft();
            }
            else if (CameraEnteredRightTile())
            {
                ScrollRight();
            }
        }


        // Checks if the camera entered the left tile
        private bool CameraEnteredLeftTile()
        {
            return cameraTransform.position.x <= leftTilePosition.x + spriteWidth / 2;
        }


        // Checks if the camera entered the right tile
        private bool CameraEnteredRightTile()
        {
            return cameraTransform.position.x >= rightTilePosition.x - spriteWidth / 2;
        }


        // Moves the left-most sprite to the right end
        private void ScrollRight()
        {
            Vector3 nextPos = new Vector3(rightTilePosition.x + spriteWidth, rightTilePosition.y, rightTilePosition.z);

            leftTilePosition = nextPos;

            rightTileIndex = leftTileIndex;
            leftTileIndex++;

            if (leftTileIndex >= Tiles.Count)
            {
                leftTileIndex = 0;
            }
        }


        // Moves the right-most sprite to the left end
        private void ScrollLeft()
        {
            Vector3 nextPos = new Vector3(leftTilePosition.x - spriteWidth, leftTilePosition.y, leftTilePosition.z);

            rightTilePosition = nextPos;

            leftTileIndex = rightTileIndex;
            rightTileIndex--;

            if (rightTileIndex < 0)
            {
                rightTileIndex = Tiles.Count -1;
            }
        }

    }    
}
