using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class ObstacleGenerator : BaseEntity
    {
        public GameObject[] ObstaclePool;

        private System.Random random;
        private float offsetX;

        private float timeSinceLastSpawn = 0f;
        private float spawnIntervalSeconds = 1f;

        public void Start()
        {
            offsetX = goTransform.position.x - App.Cache.jester.goTransform.position.x;
            random = new System.Random();
        }


        public void Update()
        {
            // Keep distance from Jester
            goTransform.position = new Vector3(App.Cache.jester.goTransform.position.x + offsetX, goTransform.position.y);

            // Check time elapsed
            timeSinceLastSpawn += Time.deltaTime;

            if(timeSinceLastSpawn >= spawnIntervalSeconds)
            {
                SpawnObstacle();
                timeSinceLastSpawn = 0;
            }
        }


        private void SpawnObstacle()
        {
            int index = random.Next(0, ObstaclePool.Length);

            GameObject go = Instantiate(ObstaclePool[index]);
            go.transform.position = new Vector2(goTransform.position.x, goTransform.position.y);
        }
    }
}
