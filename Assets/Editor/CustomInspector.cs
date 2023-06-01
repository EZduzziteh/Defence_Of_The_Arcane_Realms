using Gameplay;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameObjectInstantiator))]
public class CustomInspector : Editor
{

    string amountToSpawn = "1";
    string spawnInterval = "1";
    string spawnDelay = "0";
    WaveData wave = null;
    GameObject waveDataPrefab = null;
    GameObject enemyTypePrefab = null;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        GameObjectInstantiator instatiator = (GameObjectInstantiator)target;


        if (wave == null && waveDataPrefab != null)
        {
            if (GUILayout.Button("Create Wave"))
            {
                wave = instatiator.SpawnWaveData(waveDataPrefab.GetComponent<WaveData>());
            }
        }

        if (waveDataPrefab == null)
        {
            waveDataPrefab = EditorGUILayout.ObjectField("Select Wave Data Prefab: ", waveDataPrefab, typeof(GameObject), true) as GameObject;
            if (waveDataPrefab)
            {
                if (waveDataPrefab.GetComponent<WaveData>())
                {
                    Debug.Log("wave Data Selected");
                }
                else
                {
                    Debug.Log("Please Select a Prefab with WaveData Component");
                    waveDataPrefab = null;
                }
            }
        }

        /*
        GUILayout.BeginHorizontal("Spawn Data Box");
        enemyTypePrefab = EditorGUILayout.ObjectField("Enemy Type", enemyTypePrefab, typeof(GameObject), true) as GameObject;
        if (!enemyTypePrefab.GetComponent<Enemy_AI>())
        {
            enemyTypePrefab = null;
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("2");
        waveDataPrefab = EditorGUILayout.ObjectField("WaveDataPrefab", waveDataPrefab, typeof(GameObject), true) as GameObject;
        if (!waveDataPrefab.GetComponent<Enemy_AI>())
        {
            waveDataPrefab = null;
        }
        GUILayout.EndHorizontal();
        */


        if (wave != null)
        {



            GUILayout.BeginHorizontal();

            GUILayout.Label("Enemy Type");
            enemyTypePrefab = EditorGUILayout.ObjectField("Enemy Type: ", enemyTypePrefab, typeof(GameObject), true) as GameObject;
            if (enemyTypePrefab.GetComponent<Enemy_AI>())
            {
                Debug.Log("Set enemy Type");

            }
            else
            {
                Debug.Log("Select a prefab with an Enemy_AI component!");
                enemyTypePrefab = null;
            }


            GUILayout.Label("Amount to Spawn");
            amountToSpawn = GUILayout.TextField(amountToSpawn);

            GUILayout.Label("Spawn Interval");
            spawnInterval = GUILayout.TextField(spawnInterval);


            GUILayout.Label("Initial Spawn Delay");
            spawnDelay = GUILayout.TextField(spawnDelay);

            GUILayout.EndHorizontal();

            if (enemyTypePrefab != null)
            {

                if (GUILayout.Button("Create Spawn Data"))
                {
                    instatiator.GenerateSpawnData(enemyTypePrefab, int.Parse(amountToSpawn), int.Parse(spawnInterval), int.Parse(spawnDelay));
                }
            }
            else
            {

                GUILayout.Label("Select an Enemy Type!");
            }

        }
    }





}
