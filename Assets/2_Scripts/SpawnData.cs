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

        EnemyObjectPool pool;

        private void Start()
        {
            pool = FindObjectOfType<EnemyObjectPool>();
        }


        public void StartSpawn()
        {
            Transform temp = FindObjectOfType<SpawnLocation>().GetRandomSpawnLocation();


            //#TODO pull item from item pool
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

                    //#TODO return the item to the item pool
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