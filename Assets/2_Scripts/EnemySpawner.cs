
namespace Gameplay
{
    using System;
    using UnityEngine;

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        public GameObject spawnPoint;
        public float timeBetweenSpawns = 3.0f;
        [SerializeField]
        public GameObject enemyToSpawn;
        float spawnTimer;


        int amountSpawned;
        public int amountToSpawn;

        public bool isSpawning = false;


        // Update is called once per frame
        void Update()
        {
            if (isSpawning)
            {
                if (amountSpawned < amountToSpawn)
                {
                    spawnTimer -= Time.deltaTime;

                    if (spawnTimer <= 0)
                    {
                        spawnTimer = timeBetweenSpawns;
                        SpawnEnemy();
                    }

                }
                else
                {
                    isSpawning = false;
                    
                    Destroy(this.gameObject);
                }
            }
        }

        public void SpawnEnemy()
        {
            //#TODO hook this up to the object pool
            GameObject temp = GameObject.Instantiate(enemyToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
            temp.transform.localScale = enemyToSpawn.transform.localScale;
            amountSpawned++;
        }

     

      
    }
}