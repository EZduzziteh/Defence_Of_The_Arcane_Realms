namespace Gameplay
{
    using Sirenix.OdinInspector;
    using UnityEngine;


    public class SpawnData : MonoBehaviour
    {





        [SerializeField]
        [AssetsOnly]
        public GameObject enemyType;
        public int amountToSpawn;
        public float spawnInterval;
        public float initialSpawnDelay;

        [SerializeField]
        EnemySpawner spawnerPrefab;
        EnemySpawner spawner;

        public bool spawning;


        public void StartSpawn()
        {
            Transform temp = FindObjectOfType<SpawnLocation>().GetRandomSpawnLocation();

            spawner = GameObject.Instantiate(spawnerPrefab, temp.position, temp.rotation);
            spawner.enemyToSpawn = enemyType;
            spawner.timeBetweenSpawns = spawnInterval;
            spawner.amountToSpawn = amountToSpawn;
            spawner.isSpawning = true;
            spawning = true;
        }

        public void Update()
        {
            if (spawning)
            {
                if (spawner.isSpawning == false)
                {
                    Destroy(this.gameObject);
                    spawning = false;
                }
            }
        }

        public float getInitialSpawnDelay()
        {
            return initialSpawnDelay;
        }
        public int getAmountToSpawn()
        {
            return amountToSpawn;
        }
        public float getSpawnInterval()
        {
            return spawnInterval;
        }

    }
}