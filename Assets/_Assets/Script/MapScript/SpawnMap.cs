using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnMap : MonoBehaviour
{
    [SerializeField] private GameObject[] roadM;
    [SerializeField] private GameObject[] roadL;
    [SerializeField] private GameObject[] roadR;
    [SerializeField] private GameObject[] roadMspawn;
    [SerializeField] private GameObject[] roadLspawn;
    [SerializeField] private GameObject[] roadRspawn;
    [SerializeField] private Transform parentobj;
    [SerializeField] private Transform Mspawnpos;
    [SerializeField] private Transform Rspawnpos;
    [SerializeField] private Transform Lspawnpos;
    private int RoadLength = 8;
    private int a;

    public GameObject[] RoadMspawn { get => roadMspawn; set => roadMspawn = value; }
    public GameObject[] RoadLspawn { get => roadLspawn; set => roadLspawn = value; }
    public GameObject[] RoadRspawn { get => roadRspawn; set => roadRspawn = value; }

    // Start is called before the first frame update
    void Start()
    {
        roadMspawn = new GameObject[RoadLength];
        roadLspawn = new GameObject[RoadLength];
        roadRspawn = new GameObject[RoadLength];
        //Spawn();
        SpawnRoad(roadMspawn, roadM, Mspawnpos);
        SpawnRoad(roadLspawn, roadL, Lspawnpos);
        SpawnRoad(roadRspawn, roadR, Rspawnpos);
        CheckRailRoad(roadMspawn, roadLspawn, roadRspawn, roadL, roadR);
        CheckRailRoad(roadLspawn, roadMspawn, roadRspawn, roadM, roadR);
        CheckRailRoad(roadRspawn, roadMspawn, roadLspawn, roadM, roadL);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void Spawn()
    //{
    //    for (int i = 0; i < RoadLength; i++)
    //    {

    //        if(i == RoadLength -1)
    //        {
    //            SpawnRoad(RoadLspawn[i], roadL, roadLspawn[i - 1].transform);
    //            SpawnRoad(RoadMspawn[i], roadM, roadMspawn[i - 1].transform);
    //            SpawnRoad(RoadRspawn[i], roadR, roadRspawn[i - 1].transform);
    //        }

    //    }
    //}

    //private GameObject SpawnRoad(GameObject road, Transform pos)
    //{
    //    GameObject roadspawn = Instantiate(road, pos.position, Quaternion.Euler(-90, 0, 0));
    //    return roadspawn;
    //}

    //private void SpawnRoad(GameObject road, GameObject[] roadspawn, Transform pos)
    //{
    //    int r = Random.Range(0, 100);
    //    if (r >= 90)
    //    {
    //        road = SpawnRoad(roadspawn[1], pos);
    //    }
    //    else
    //    {
    //        road = SpawnRoad(roadspawn[0], pos);
    //    }
    //}
    private void SpawnRoad(GameObject[] road, GameObject[] roadspawn, Transform pos)
    {
        for (int i = 0; i < road.Length; i++)
        {
            int r = Random.Range(0, 100);
            if (i == road.Length - 1)
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
                    if (road[i - 1].GetComponent<RoadType>().type == TypeRoad.Rail)
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
                road[i] = Instantiate(roadspawn[a], pos.position, Quaternion.Euler(-90, 0, 0));
            }
            else if (road[i - 1].GetComponent<RoadType>().type == TypeRoad.Hillup)
            {
                road[i] = Instantiate(roadspawn[3], road[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0));
            }
            else
            {
                road[i] = Instantiate(roadspawn[a], road[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0));
            }
        }
    }

    private void CheckRailRoad(GameObject[] roadListcheck, GameObject[] listroad1, GameObject[] listroad2, GameObject[] road1, GameObject[] road2)
    {
        int a;
        int b;
        for (int i = 0; i < roadMspawn.Length; i++)
        {
            if (roadListcheck[i].GetComponent<RoadType>().type == TypeRoad.Rail)
            {
                a = Random.Range(4, 7);
                b = Random.Range(4, 7);
                if (listroad1[i].GetComponent<RoadType>().type == TypeRoad.Hillup ||
                listroad1[i].GetComponent<RoadType>().type == TypeRoad.HillDown ||
                listroad2[i].GetComponent<RoadType>().type == TypeRoad.Hillup ||
                listroad2[i].GetComponent<RoadType>().type == TypeRoad.HillDown)
                {
                    a = Random.Range(0, 2);
                    Vector3 pos1 = roadListcheck[i].transform.position;
                    Destroy(roadListcheck[i]);
                    roadListcheck[i] = Instantiate(road1[a], pos1, Quaternion.Euler(-90, 0, 0));
                }
                else
                {
                    if (listroad1[i].GetComponent<RoadType>().type != TypeRoad.Rail)
                    {
                        Vector3 pos1 = listroad1[i].transform.position;
                        Destroy(listroad1[i]);
                        listroad1[i] = Instantiate(road1[a], pos1, Quaternion.Euler(-90, 0, 0));
                    }
                    if (listroad2[i].GetComponent<RoadType>().type != TypeRoad.Rail)
                    {
                        Vector3 pos2 = listroad2[i].transform.position;
                        Destroy(listroad2[i]);
                        listroad2[i] = Instantiate(road2[b], pos2, Quaternion.Euler(-90, 0, 0));
                    }
                }
            }
        }
    }
}
