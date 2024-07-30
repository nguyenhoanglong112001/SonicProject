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
    private int a;
    // Start is called before the first frame update
    void Start()
    {
        roadMspawn = new GameObject[8];
        roadLspawn = new GameObject[8];
        roadRspawn = new GameObject[8];
        SpawnRoad(roadMspawn, roadM, Mspawnpos);
        SpawnRoad(roadLspawn, roadL, Lspawnpos);
        SpawnRoad(roadRspawn, roadR, Rspawnpos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRoad(GameObject[] road, GameObject[] roadspawn,Transform pos)
    {
        for (int i = 0; i < road.Length; i++)
        {
            Debug.Log(road[i]);
            int r = Random.Range(0, 100);
            if (0 < r && r < 90)
            {
                a = 0;
            }
            else if (r >= 90)
            {
                a = 1;
            }
            if (i == 0)
            {
                road[i] = Instantiate(roadspawn[a], pos.position, Quaternion.Euler(-90, 0, 0));
            }
            else
            {
                road[i] = Instantiate(roadspawn[a], road[i - 1].transform.GetChild(0).position, Quaternion.Euler(-90, 0, 0));
            }
        }
    }
}
