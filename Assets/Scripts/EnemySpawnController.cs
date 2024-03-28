using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnController : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab;
        public int maxSpawnCount = 5;
    }

    public List<EnemySpawnData> spawnDataList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        foreach(var spawnData in spawnDataList)
        {
            for (int i = 0; i < spawnData.maxSpawnCount; ++i)
            {
                Vector3 spawnPointPos;
                if(FindRandomPoint(Vector3.zero, 50f, out spawnPointPos))
                Instantiate(spawnData.enemyPrefab, spawnPointPos, spawnData.enemyPrefab.transform.rotation);
            }
        }
    }

    bool FindRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    public void TriggerSpawn()
    {
        SpawnEnemies();
    }
}
