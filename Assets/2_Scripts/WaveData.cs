namespace Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public class WaveData : MonoBehaviour
    {
        List<SpawnData> spawnData = new List<SpawnData>();

        float timeSinceStart = 0.0f;
        public bool spawning = false;

        public int totalAmountToSpawn;
        public int totalAmountRemaining;

        public int waveGoldBonus;

        private void Start()
        {
            if (spawnData.Count <= 0)
            {
                foreach (SpawnData data in GetComponentsInChildren<SpawnData>())
                {
                    spawnData.Add(data);
                }
            }

            foreach (SpawnData data in spawnData)
            {
                totalAmountToSpawn += data.getAmountToSpawn();
                totalAmountRemaining += data.getAmountToSpawn();

            }
        }

        private void Update()
        {
            if (spawning)
            {
                if (spawnData.Count > 0)
                {

                    timeSinceStart += Time.deltaTime;
                    List<SpawnData> dataToRemove = new List<SpawnData>();

                    foreach (SpawnData data in spawnData)
                    {
                        if (data.getInitialSpawnDelay() <= timeSinceStart)
                        {
                            data.StartSpawn();
                            dataToRemove.Add(data);
                        }
                    }

                    foreach (SpawnData data in dataToRemove)
                    {
                        spawnData.Remove(data);
                    }

                }
            }
        }

    }
}