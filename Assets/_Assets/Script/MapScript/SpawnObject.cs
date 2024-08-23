using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] spawnObjList;
    [SerializeField] private List<GameObject> spawnlist;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int numberofBlock;
    [SerializeField] private MeshRenderer meshref;
    [SerializeField] private float lane;
    [SerializeField]
    private int a;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private SpawnMap getRoadindex;
    [SerializeField] private int roadIndex;
    [SerializeField] private GameObject laneObject;
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject road = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.transform.parent.gameObject;
        getRoadindex = road.GetComponent<SpawnMap>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        Bounds bounds = meshref.bounds;
        int i = 0;
        int r = Random.Range(0, 100);
        if (i == 0)
        {
            if(r > 60)
            {
                a = Random.Range(0, spawnObjList.Length);
                if(lane == 0)
                {
                    if (a == spawnObjList.Length - 1)
                    {
                        if(CanSpawnTriple())
                        {
                            GameObject block = Instantiate(spawnObjList[a], transform.position, spawnObjList[a].transform.rotation, gameObject.transform);
                            spawnlist.Add(block);
                            i++;
                        }
                    }
                }
                else
                {
                    GameObject block = Instantiate(spawnObjList[a], transform.position, spawnObjList[a].transform.rotation, gameObject.transform);
                    spawnlist.Add(block);
                    i++;
                }
            }
            spawnPos = transform.position;
        }
        else
        {
            while (spawnlist[i-1].transform.position.z + spawnDistance < bounds.max.z)
            {
                float spawnZ = spawnPos.z + spawnDistance;
                spawnPos = new Vector3(lane, 0, spawnZ);
                r = Random.Range(0, 100);
                if(r > 60)
                {
                    a = Random.Range(0, spawnObjList.Length);
                    if (lane == 0)
                    {
                        if (a == spawnObjList.Length - 1)
                        {
                            if(CanSpawnTriple())
                            {
                                GameObject block = Instantiate(spawnObjList[a], transform.position, spawnObjList[a].transform.rotation, gameObject.transform);
                                spawnlist.Add(block);
                                i++;
                            }
                        }
                    }
                    else
                    {
                        GameObject block = Instantiate(spawnObjList[a], spawnPos, spawnObjList[a].transform.rotation);
                        spawnlist.Add(block);
                        i++;
                    }
                }
            }
        }
    }

    private bool CanSpawnTriple()
    {
        roadIndex = Array.IndexOf(getRoadindex.RoadMspawn, laneObject);
        if (getRoadindex.RoadMspawn[roadIndex].GetComponent<RoadType>().type != TypeRoad.Hillup ||
            getRoadindex.RoadMspawn[roadIndex].GetComponent<RoadType>().type != TypeRoad.HillDown ||
            getRoadindex.RoadMspawn[roadIndex].GetComponent<RoadType>().type != TypeRoad.Hillup ||
            getRoadindex.RoadMspawn[roadIndex].GetComponent<RoadType>().type != TypeRoad.HillDown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
