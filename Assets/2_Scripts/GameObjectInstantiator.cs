namespace Gameplay
{
    using UnityEngine;

    public class GameObjectInstantiator : MonoBehaviour
    {

        WaveData waveDataPrefab;
        SpawnData enemyData;

        WaveData wave;
        public WaveData SpawnWaveData(WaveData data)
        {
            waveDataPrefab = data;
            wave = Instantiate(waveDataPrefab, transform.position, transform.rotation);
            return wave;
        }

        public void GenerateSpawnData(GameObject enemyType, int amountToSpawn, float spawnInterval, float InitialSpawnDelay)
        {
            SpawnData temp = Instantiate(enemyData);
            temp.amountToSpawn = amountToSpawn;
            temp.enemyType = enemyType;
            temp.spawnInterval = spawnInterval;
            temp.initialSpawnDelay = InitialSpawnDelay;


            temp.transform.parent = wave.transform;
        }
    }
}