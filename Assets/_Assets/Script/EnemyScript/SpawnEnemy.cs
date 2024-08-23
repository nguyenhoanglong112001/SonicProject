using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float changeSpawn;
    [SerializeField] private SpawnMap getRoadindex;
    [SerializeField] private int roadIndex;
    [SerializeField] private GameObject lane;
    private GameObject roadToCheck; 
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }    
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
                            Instantiate(enemyPrefab[a], transform.position, enemyPrefab[a].transform.rotation, gameObject.transform);
                        }
                        else
                        {
                            while (enemyPrefab[a].GetComponent<TypeEnemy>().type == EnemyType.MotoBug)
                            {
                                a = Random.Range(0, enemyPrefab.Length);
                            }
                            Instantiate(enemyPrefab[a], transform.position, enemyPrefab[a].transform.rotation, gameObject.transform);
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
