using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{

    [SerializeField]
    Vector3 spawnPosition;

    [SerializeField]
    EnemySpawner spawnerPrefab;
    ObjectPool<EnemySpawner> spawnerPool;
    [SerializeField]
    Enemy_AI orcWarriorPrefab;
    ObjectPool<Enemy_AI> orcWarriorPool;


    // Start is called before the first frame update
    void Start()
    {
        spawnerPool = new ObjectPool<EnemySpawner>(() => //on create
        {
            return Instantiate(spawnerPrefab);
        }, spawner => //on get
        {
            spawner.gameObject.SetActive(true);
        }, spawner =>//on release
        {
            spawner.gameObject.SetActive(false);
        }, spawner => // on kill
        {
            Destroy(spawner.gameObject);
        }, false, 10, 20);


        orcWarriorPool = new ObjectPool<Enemy_AI>(() => //on create
        {
            return Instantiate(orcWarriorPrefab);
        }, spawner => //on get
        {
            spawner.gameObject.SetActive(true);
        }, spawner =>//on release
        {
            spawner.gameObject.SetActive(false);
        }, spawner => // on kill
        {
            Destroy(spawner.gameObject);
        }, false, 10, 20);


    }

    public EnemySpawner GetEnemySpawner()
    {
        return spawnerPool.Get();
    }

    public void ReleaseenemySpawner(EnemySpawner enemySpawner)
    {
        spawnerPool.Release(enemySpawner);
    }

    public Enemy_AI GetOrcWarrior()
    {
        return orcWarriorPool.Get();
    }
    public void ReleaseOrcWarrior(Enemy_AI enemy)
    {
        orcWarriorPool.Release(enemy);
    }

}
