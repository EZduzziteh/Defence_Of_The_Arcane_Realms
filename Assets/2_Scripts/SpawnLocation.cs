namespace Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SpawnLocation : MonoBehaviour
    {

        public List<Transform> spawnLocations = new List<Transform>();
        // Start is called before the first frame update
        void Start()
        {
            foreach (var point in GetComponentsInChildren<Transform>())
            {
                spawnLocations.Add(point);
            }
        }

        public Transform GetRandomSpawnLocation()
        {
            System.Random rand = new System.Random();
            int randomNum = rand.Next(0, spawnLocations.Count - 1);

            return spawnLocations[randomNum].transform;
        }
    }
}