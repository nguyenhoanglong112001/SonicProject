using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class SpawnMap : MonoBehaviour
{
    [SerializeField] private GameObject[] roadM;
    [SerializeField] private GameObject[] roadL;
    [SerializeField] private GameObject[] roadR;
    [SerializeField] private GameObject[] roadMspawn;
    [SerializeField] private GameObject[] roadLspawn;
    [SerializeField] private GameObject[] roadRspawn;
    [SerializeField] private Transform Mspawnpos;
    [SerializeField] private Transform Rspawnpos;
    [SerializeField] private Transform Lspawnpos;
    [SerializeField] private GameObject objParent;
    [SerializeField] private LeanGameObjectPool roadPool;
    private int RoadLength = 8;
    private int a;

    public GameObject[] RoadMspawn { get => roadMspawn; set => roadMspawn = value; }
    public GameObject[] RoadLspawn { get => roadLspawn; set => roadLspawn = value; }
    public GameObject[] RoadRspawn { get => roadRspawn; set => roadRspawn = value; }

    // Start is called before the first frame update
    void Start()
    {
        roadPool = GameObject.FindWithTag("RoadPool").GetComponent<LeanGameObjectPool>();
        roadMspawn = new GameObject[RoadLength];
        roadLspawn = new GameObject[RoadLength];
        roadRspawn = new GameObject[RoadLength];
        foreach(Transform child in objParent.transform)
        {
            Destroy(child.gameObject);
        }
        SpawnMidLane();
        SpawnSideLane(RoadLspawn, roadL, Lspawnpos);
        SpawnSideLane(RoadRspawn, roadR, Rspawnpos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnMidLane()
    {
        for (int i = 0; i < roadMspawn.Length; i++)
        {
            int r = Random.Range(0, 100);
            if (i == roadMspawn.Length - 1)
            {
                if (r >= 90)
                {
                    a = 1;
                }
                else
                {
                    a = 0;
                }
            }
            else if (i > 0)
            {
                if (r >= 97)
                {
                    if (roadMspawn[i - 1].GetComponent<RoadType>().type == TypeRoad.Rail)
                    {
                        a = 0;
                    }
                    else
                    {
                        a = Random.Range(4, 7);
                    }
                }
                else if (r >= 95)
                {
                    a = 2;
                }
                else if (r >= 90 && r < 95)
                {
                    a = 1;
                }
                else if (r < 90)
                {
                    a = 0;
                }
            }

            if (i == 0)
            {
                if (r >= 95)
                {
                    a = 2;
                }
                else if (r >= 90 && r < 95)
                {
                    a = 1;
                }
                else if (r < 90)
                {
                    a = 0;
                }
                roadPool.Prefab = roadM[a];
                roadMspawn[i] = roadPool.Spawn(Mspawnpos.position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
            else if (roadMspawn[i - 1].GetComponent<RoadType>().type == TypeRoad.Hillup)
            {
                roadPool.Prefab = roadM[3];
                roadMspawn[i] = roadPool.Spawn(roadMspawn[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
            else
            {
                roadPool.Prefab = roadM[a];
                roadMspawn[i] = roadPool.Spawn(roadMspawn[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
        }
    }

    private void SpawnSideLane(GameObject[] sideRoad, GameObject[] roadSpawn, Transform spawnPos)
    {
        for (int i = 0; i < sideRoad.Length; i++)
        {
            int r = Random.Range(0, 100);
            if (i == sideRoad.Length - 1)
            {
                if (r >= 90)
                {
                    a = 1;
                }
                else
                {
                    a = 0;
                }
                roadPool.Prefab = roadSpawn[a];
                sideRoad[i] = roadPool.Spawn(sideRoad[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
            else if (i > 0)
            {
                if(roadMspawn[i].GetComponent<RoadType>().type == TypeRoad.Rail)
                {
                    a = Random.Range(4, 7);
                }
                else if (roadMspawn[i+1].GetComponent<RoadType>().type == TypeRoad.Rail)
                {
                    a = Random.Range(0, 2);
                }
                else if (sideRoad[i-1].GetComponent<RoadType>().type == TypeRoad.Rail)
                {
                    a = 0;
                }
                else if (sideRoad[i - 1].GetComponent<RoadType>().type == TypeRoad.Hillup)
                {
                    a = 3;
                }
                else
                {
                    if (r >98)
                    {
                        if (sideRoad[i - 1].GetComponent<RoadType>().type == TypeRoad.Road)
                        {
                            a = 7;
                        }
                    }
                    else if (r >= 95)
                    {
                        a = 2;
                    }
                    else if (r >= 90 && r < 95)
                    {
                        a = 1;
                    }
                    else if (r < 90)
                    {
                        a = 0;
                    }
                }
                roadPool.Prefab = roadSpawn[a];
                sideRoad[i] = roadPool.Spawn(sideRoad[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
            if (i == 0)
            {
                if (r >= 95)
                {
                    a = 2;
                }
                else if (r >= 90 && r < 95)
                {
                    a = 1;
                }
                else
                {
                    a = 0;
                }
                roadPool.Prefab = roadSpawn[a];
                sideRoad[i] = roadPool.Spawn(spawnPos.position, Quaternion.Euler(-90, 0, 0), objParent.transform);
            }
        }
    }
}
