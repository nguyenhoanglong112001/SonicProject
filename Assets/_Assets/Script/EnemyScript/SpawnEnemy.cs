using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Pool;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float changeSpawn;
    [SerializeField] private SpawnMap getRoadindex;
    [SerializeField] private int roadIndex;
    [SerializeField] private GameObject lane;
    private LeanGameObjectPool enemyPool;
    private GameObject roadToCheck; 
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = GameObject.FindWithTag("EnemyPool").GetComponent<LeanGameObjectPool>();
        EnemySpawn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemySpawn()
    {
        int r = Random.Range(0, 100);
        {
            if (r > changeSpawn)
            {
                int a = Random.Range(0, enemyPrefab.Length);
                {
                    if(enemyPrefab[a].GetComponent<TypeEnemy>().type == EnemyType.MotoBug)
                    {
                        if(CanSpawnMotorBug())
                        {
                            enemyPool.Prefab = enemyPrefab[a];
                            enemyPool.Spawn(transform.position, enemyPrefab[a].transform.rotation);
                        }
                        else
                        {
                            while (enemyPrefab[a].GetComponent<TypeEnemy>().type == EnemyType.MotoBug)
                            {
                                a = Random.Range(0, enemyPrefab.Length);
                            }
                            enemyPool.Prefab = enemyPrefab[a];
                            enemyPool.Spawn(transform.position, enemyPrefab[a].transform.rotation);
                        }
                    }
                }
            }
        }
    }

    private bool CanSpawnMotorBug()
    {
        GameObject mapSpawn = lane.transform.parent.transform.parent.gameObject;
        getRoadindex = mapSpawn.GetComponent<SpawnMap>();
        roadIndex = Array.IndexOf(getRoadindex.RoadMspawn, lane);
        if(roadIndex > 0)
        {
            roadToCheck = getRoadindex.RoadMspawn[roadIndex - 1];
        }
        else if(roadIndex <= 0)
        {
            roadIndex = Array.IndexOf(getRoadindex.RoadRspawn, lane);
            if (roadIndex > 0)
            {
                roadToCheck = getRoadindex.RoadRspawn[roadIndex - 1];
            }
            else if (roadIndex <= 0)
            {
                roadIndex = Array.IndexOf(getRoadindex.RoadLspawn, lane);
                if (roadIndex > 0)
                {
                    roadToCheck = getRoadindex.RoadRspawn[roadIndex - 1];
                }
                else
                {
                    return true;
                }
            }    
        }
        if (roadToCheck.GetComponent<RoadType>().type != TypeRoad.Rail)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
