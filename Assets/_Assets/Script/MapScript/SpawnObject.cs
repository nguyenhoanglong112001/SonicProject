using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] spawnObjList;
    [SerializeField] private List<GameObject> spawnlist;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int numberofBlock;
    [SerializeField] private MeshRenderer meshref;
    [SerializeField] private float lane;
    private int a;
    [SerializeField] private Vector3 spawnPos;
    void Start()
    {
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
            if(r > 80)
            {
                a = Random.Range(0, spawnObjList.Length);
                GameObject block = Instantiate(spawnObjList[a], transform.position, spawnObjList[a].transform.rotation);
                spawnlist.Add(block);
                i++;
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
                if(r > 80)
                {
                    a = Random.Range(0, spawnObjList.Length);
                    GameObject block = Instantiate(spawnObjList[a], spawnPos, spawnObjList[a].transform.rotation);
                    spawnlist.Add(block);
                    i++;
                }
            }
        }
    }
}
