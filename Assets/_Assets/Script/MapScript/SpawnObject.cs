using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Pool;

public class SpawnObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] spawnObjList;
    [SerializeField] private List<GameObject> spawnlist;
    [SerializeField] private float spawnDistance;
    [SerializeField] private MeshRenderer meshref;
    [SerializeField] private float lane;
    [SerializeField]
    private int a;
    [SerializeField] private Vector3 spawnPos;
    private LeanGameObjectPool blockPool;
    void Start()
    {
        blockPool = GameObject.FindWithTag("BlockPool").GetComponent<LeanGameObjectPool>();
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
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
                blockPool.Prefab = spawnObjList[a];
                GameObject block = blockPool.Spawn(transform.position, spawnObjList[a].transform.rotation, gameObject.transform);
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
                if(r > 60)
                {
                    blockPool.Prefab = spawnObjList[a];
                    GameObject block = blockPool.Spawn(spawnPos, spawnObjList[a].transform.rotation, gameObject.transform);
                    spawnlist.Add(block);
                    i++;
                }
            }
        }
    }
}
